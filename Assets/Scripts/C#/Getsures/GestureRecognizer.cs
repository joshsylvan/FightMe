using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureRecognizer {

	List<Pair> combosToChange = new List<Pair> ();
	int numberOfPoints = 20;




	public List<Gesture> ClassifyGestures(List<Gesture> unclassifiedGestures, List<Gesture> classifiedGestures, float minRatio, float maxPathDistance, float maxPointDistance){
		combosToChange = new List<Pair>();
		List<Gesture> gestures = classifiedGestures;

		for (int i = 0; i < unclassifiedGestures.Count; i++) {
			
			Gesture currentGesture = unclassifiedGestures [i];
			currentGesture.SetPositions ( Resample(currentGesture.GetPositionList(), 20, currentGesture.GetDuration()) );
			//currentGesture.SetRotations (NormalizeListRotation(lr, 20));
			if (GestureLength (currentGesture.GetPositionList().ToArray()) < 0.5f) {
				continue;
			}
			if (gestures.Count >= 1) { // if theeres more than one class then do cla ification
				int result = NaiveRecognizer(currentGesture.GetPositionList().ToArray(), gestures, minRatio, maxPathDistance, maxPointDistance );
				if (result == -1) {
					gestures.Add (currentGesture);
				} else {
//					combosToChange.Add (new Pair (int.Parse (currentGesture.GetName ()), int.Parse (classifiedGestures [result].GetName ())));
					gestures [result] = NormalizeGesture (currentGesture, gestures [result]);
				}
			} else { // if there are none createa new class
				gestures.Add(currentGesture);
			}
		}

		return gestures;
	}

	public Vector3[] Resample(List<Vector3> positions, int n, float totalDuration){
		float I = GestureLength (positions.ToArray ()) / (n - 1);
		float D = 0f;
		List<Vector3> newPositions = new List<Vector3>();
		newPositions.Add(positions [0]);
		float duration = totalDuration/n;
		for (int i = 1; i < positions.Count; i++) {
			float d = Vector3.Distance (positions [i - 1], positions [i]);
			if ((D + d) >= I) {
				float qx = positions [i - 1].x + ((I - D) / d) * (positions [i].x - positions [i - 1].x);
				float qy = positions [i - 1].y + ((I - D) / d) * (positions [i].y - positions [i - 1].y);
				float qz = positions [i - 1].z + ((I - D) / d) * (positions [i].z - positions [i - 1].z);
				Vector3 q = new Vector3 (qx, qy, qz);
				newPositions.Add( q );
				positions.Insert (i, q);
				D = 0f;
			} else {
				D += d;
			}
		}
		if (newPositions.Count == n - 1) {
			newPositions.Add(new Vector3( 
				newPositions[newPositions.Count-1].x,
				newPositions[newPositions.Count-1].y,
				newPositions[newPositions.Count-1].z
			));
		}
		return newPositions.ToArray();
	}

	float GestureLength(Vector3[] positions){
		float length = 0f;
		for (int i = 1; i < positions.Length; i++) {
			length += Vector3.Distance (positions[i], positions[i-1]);
		}
		return length;
	}

	public int NaiveRecognizer(Vector3[] points, List<Gesture> gestures, float minRatio, float maxPathDistance, float maxPointDistance){
		int bestRatioIndex = -1;
		float bestRatio = 0f;

		int bestDistanceIndex = -1;
		float bestDistance = Mathf.Infinity;

		for (int i = 0; i < gestures.Count; i++) {
			// Distance
			float pathDistance = GestureDistance(points, gestures[i].GetPositionList().ToArray());
			if (pathDistance < bestDistance) {
				bestDistance = pathDistance;
				bestDistanceIndex = i;
			}

			// Ratio
			int indexCount = 0;
			for (int j = 0; j < points.Length; j++) {
				if (Vector3.Distance (points [j], gestures [i].GetPositionList () [j]) <= maxPointDistance) {
					indexCount++;
				}
			}
			if (indexCount > bestRatio) {
				bestRatio = indexCount;
				bestRatioIndex = i;
			}
			//			Debug.Log (indexCount);
		}
		bestRatio = bestRatio / points.Length;

		//		Debug.Log ("Best Ratio index, " + bestRatioIndex + " : " + bestRatio); 
		//		Debug.Log ("Best Distance index, " + bestDistanceIndex + " : " + bestDistance);
		//		Debug.Log ("END");
		//
		if (bestRatioIndex == bestDistanceIndex) {
			if (bestRatio >= minRatio && bestDistance <= maxPathDistance) {
				return bestRatioIndex;
			} else {
				return -1;
			}
		} else {
			if (bestRatio >= minRatio && bestDistance > maxPathDistance) {
				return bestRatioIndex;
			} else if (bestRatio < minRatio && bestDistance <= maxPathDistance) {
				return bestDistanceIndex;
			} else if (bestRatio >= minRatio && bestDistance <= maxPathDistance) {
				return bestRatioIndex; // TODO Work out what to do here if index is different.
			} else {
				return -1;
			}
		}
	}

	float GestureDistance(Vector3[] p1, Vector3[] p2){
		float distance = 0f;
		for (int i = 0; i < p1.Length; i++) {
			distance += Vector3.Distance (p1 [i], p2 [i]);
		}
		Debug.Log (distance / p1.Length);
		return distance / p1.Length;
	}

	Gesture NormalizeGesture(Gesture p1, Gesture gesture){   // TODO Maybe normalize based of the time too.
		for(int i = 0; i < gesture.GetMatrixArray().Length; i++){
			gesture.GetMatrixArray () [i].SetColumn (3, new Vector4 (
				(p1.GetMatrixArray()[i].GetPosition().x + gesture.GetMatrixArray()[i].GetPosition().x)/2,
				(p1.GetMatrixArray()[i].GetPosition().y + gesture.GetMatrixArray()[i].GetPosition().y)/2,
				(p1.GetMatrixArray()[i].GetPosition().z + gesture.GetMatrixArray()[i].GetPosition().z)/2
			));
			gesture.SetTimeIndex (i, (p1.GetDeltaTimes () [i] + gesture.GetDeltaTimes () [i]) / 2);
		}
		return gesture;
	}

	/*  START REFACTOR HERE



	Quaternion[] NormalizeListRotation(List<Quaternion> l, int length){
		if (l.Count < length) {
			return null;
		} else {
			int toRemove = l.Count - length;
			if (toRemove == 0) {
				return l.ToArray();
			} else {
				float ratio =  ((float)l.Count / (float)toRemove);
				List<Quaternion> newList = new List<Quaternion> ();
				for (int i = 0; i < l.Count; i++) {
					if ((i + 1) % ratio >= 1) {
						newList.Add (l [i]);
					}
				}
				if (newList.Count > length) {
					newList.RemoveAt (10);
				}
				return newList.ToArray();
			}

		}
	}

 */ //WaitForEndOfFrame REFACTOR
}