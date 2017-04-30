using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gesture Recognizer class used for gesture recognition and classification.
/// </summary>
public class GestureRecognizer {

	List<Pair> combosToChange = new List<Pair> (); // Combos to change after classification
	int numberOfPoints = 20; // number of points to resample too

	/// <summary>
	/// Classifies a list of unclassified gestures and merges them with the classified gestures list.
	/// </summary>
	/// <returns>The gestures.</returns>
	/// <param name="unclassifiedGestures">List of unclassified gestures.</param>
	/// <param name="classifiedGestures">List of classified gestures.</param>
	/// <param name="minRatio">Minimum Structural Similartiy Ratio.</param>
	/// <param name="maxPathDistance">Maximum path distance.</param>
	/// <param name="maxPointDistance">Maximum point distance.</param>
	public List<Gesture> ClassifyGestures(List<Gesture> unclassifiedGestures, List<Gesture> classifiedGestures, float minRatio, float maxPathDistance, float maxPointDistance){
		combosToChange = new List<Pair>();
		List<Gesture> gestures = classifiedGestures;

		for (int i = 0; i < unclassifiedGestures.Count; i++) {
			
			Gesture currentGesture = unclassifiedGestures [i];
			currentGesture.SetPositions ( Resample(currentGesture.GetPositionList(), 20, currentGesture.GetDuration()) );
			if (GestureLength (currentGesture.GetPositionList().ToArray()) < 0.5f) {
				continue;
			}
			if (gestures.Count >= 1) { // if there is more than one class then do classification
				int result = NaiveRecognizer(currentGesture.GetPositionList().ToArray(), gestures, minRatio, maxPathDistance, maxPointDistance ); // Get most likeley match
				if (result == -1) { // If there is no match add gesture as a new class
					gestures.Add (currentGesture);
				} else { // else mege the gestures together
					combosToChange.Add (new Pair (int.Parse (currentGesture.GetName ()), int.Parse (classifiedGestures [result].GetName ()))); // add combo pair
					gestures [result] = NormalizeGesture (currentGesture, gestures [result]); //add result
				}
			} else { // if there are no classes add gesture as first class
				gestures.Add(currentGesture);
			}
		}
		return gestures;
	}

	/// <summary>
	/// Resample a list of 3D points to a specific length.
	/// </summary>
	/// <param name="positions">List of points to resample.</param>
	/// <param name="n">number of points to be resampled too.</param>
	/// <param name="totalDuration">Total time duration of Gesture</param>
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

	/// <summary>
	/// Calculate the total length of a gesture.
	/// </summary>
	/// <returns>Length of a gesture</returns>
	/// <param name="positions">List of 3D points</param>
	float GestureLength(Vector3[] positions){
		float length = 0f;
		for (int i = 1; i < positions.Length; i++) {
			length += Vector3.Distance (positions[i], positions[i-1]);
		}
		return length;
	}

	/// <summary>
	/// Compares a list of points to a list of gestures and return which one is the most similar, given the result is within the parameters. 
	/// </summary>
	/// <returns>Most likely match of a gesture, if no match return -1.</returns>
	/// <param name="points">List og points to match with a gesture.</param>
	/// <param name="gestures">List of  pre made gestures to compare to.</param>.</param>
	/// <param name="minRatio">Minimum Structural Similartiy Ratio.</param>
	/// <param name="maxPathDistance">Maximum path distance.</param>
	/// <param name="maxPointDistance">Maximum point distance.</param>
	public int NaiveRecognizer(Vector3[] points, List<Gesture> gestures, float minRatio, float maxPathDistance, float maxPointDistance){
		int bestRatioIndex = -1;
		float bestRatio = 0f;

		int bestDistanceIndex = -1;
		float bestDistance = Mathf.Infinity;

		for (int i = 0; i < gestures.Count; i++) {
			float pathDistance = GestureDistance(points, gestures[i].GetPositionList().ToArray());
			if (pathDistance < bestDistance) {
				bestDistance = pathDistance;
				bestDistanceIndex = i;
			}

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
		}
		bestRatio = bestRatio / points.Length;
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
				return bestRatioIndex;
			} else {
				return -1;
			}
		}
	}

	/// <summary>
	/// Calculates the distance between two gestures.
	/// </summary>
	/// <returns></returns>
	/// <param name="p1">Gesture one's position points.</param>
	/// <param name="p2">Gesture two's position points.</param>
	float GestureDistance(Vector3[] p1, Vector3[] p2){
		float distance = 0f;
		for (int i = 0; i < p1.Length; i++) {
			distance += Vector3.Distance (p1 [i], p2 [i]);
		}
		return distance / p1.Length;
	}

	/// <summary>
	/// Merges two gestures toether.
	/// </summary>
	/// <returns>The result of two merged gestires.</returns>
	/// <param name="p1">Gesture 1</param>
	/// <param name="gesture">Gesture 2</param>
	Gesture NormalizeGesture(Gesture p1, Gesture gesture){
		Gesture newGesture = new Gesture(gesture.GetName());
		for(int i = 0; i < gesture.GetMatrixArray().Length; i++){
			Vector3 newPos = new Vector3(
				(p1.GetMatrixArray()[i].GetPosition().x + gesture.GetMatrixArray()[i].GetPosition().x)/2, 
				(p1.GetMatrixArray()[i].GetPosition().y + gesture.GetMatrixArray()[i].GetPosition().y)/2,
				(p1.GetMatrixArray()[i].GetPosition().z + gesture.GetMatrixArray()[i].GetPosition().z)/2
			);
			Vector3 newScale = new Vector3(
				(p1.GetMatrixArray()[i].GetScale().x + gesture.GetMatrixArray()[i].GetScale().x)/2, 
				(p1.GetMatrixArray()[i].GetScale().y + gesture.GetMatrixArray()[i].GetScale().y)/2,
				(p1.GetMatrixArray()[i].GetScale().z + gesture.GetMatrixArray()[i].GetScale().z)/2
			);

			newGesture.AddMatrix(Matrix4x4.TRS( newPos, gesture.GetMatrixArray () [i].GetRotation (), newScale));
			newGesture.AddTime ((p1.GetDeltaTimes () [i] + gesture.GetDeltaTimes () [i]) / 2);
		}
		return newGesture;
	}

	/// <summary>
	/// Gets the combos to change.
	/// </summary>
	/// <returns>The combos to change.</returns>
	public List<Pair> GetCombosToChange(){
		return combosToChange;
	}
}