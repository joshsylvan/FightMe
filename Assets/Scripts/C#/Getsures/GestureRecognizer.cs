using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureRecognizer {

	List<Pair> combosToChange;
	int numberOfPoints = 20;

	public void Recognizer(List<Point> points){
		combosToChange = new List<Pair>(); 
//		Point[] newPoints = Resample (points.ToArray(), numberOfPoints);
	}

	public List<Gesture> ClassifyGestures(List<Gesture> unclassifiedGestures, List<Gesture> classifiedGestures, float minRatio, float maxPathDistance, float maxPointDistance){
		combosToChange = new List<Pair>();
		List<Gesture> gestures = classifiedGestures;
//		Debug.Log (gestures.Count + "  :  " + unclassifiedGestures.Count);
//		List<Gesture> sampledUnclassifiedGesture = Resample (unclassifiedGestures, numberOfPoints);

		for (int i = 0; i < unclassifiedGestures.Count; i++) {
			Gesture currentGesture = unclassifiedGestures [i];
			List<Point> lp = new List<Point>(currentGesture.GetPoints ());
			currentGesture.SetPoints ( Resample(lp, 20) );
			if (GestureLength (currentGesture.GetPoints()) < 0.5f) {
				continue;
			}
			if (gestures.Count >= 1) { // if theeres more than one class then do cla ification
				int result = NaiveRecognizer(currentGesture.GetPoints(), gestures, minRatio, maxPathDistance, maxPointDistance );
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

	public int NaiveRecognizer(Point[] points, List<Gesture> gestures, float minRatio, float maxPathDistance, float maxPointDistance){
		Debug.Log (gestures.Count);
		int bestRatioIndex = -1;
		float bestRatio = 0f;

		int bestDistanceIndex = -1;
		float bestDistance = Mathf.Infinity;

		for (int i = 0; i < gestures.Count; i++) {
			// Distance
			float pathDistance = GestureDistance(points, gestures[i].GetPoints());
			if (pathDistance < bestDistance) {
				bestDistance = pathDistance;
				bestDistanceIndex = i;
			}

			// Ratio
			int indexCount = 0;
			for (int j = 0; j < points.Length; j++) {
				if (Vector3.Distance (points [j].GetPositionVector (), gestures [i].GetPoints () [j].GetPositionVector ()) <= maxPointDistance) {
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

	public Point[] Resample(List<Point> points, int n){
		float I = GestureLength (points.ToArray ()) / (n - 1);
		float D = 0f;
		List<Point> newPoints = new List<Point>();
		newPoints.Add(points [0]);
		for (int i = 1; i < points.Count; i++) {
			float d = Vector3.Distance (points [i - 1].GetPositionVector(), points [i].GetPositionVector());
			if ((D + d) >= I) {
				float qx = points [i - 1].getX () + ((I - D) / d) * (points [i].getX () - points [i - 1].getX ());
				float qy = points [i - 1].getY () + ((I - D) / d) * (points [i].getY () - points [i - 1].getY ());
				float qz = points [i - 1].getZ () + ((I - D) / d) * (points [i].getZ () - points [i - 1].getZ ());
				Point q = new Point (qx, qy, qz, 0.25f);
				newPoints.Add( q );
				points.Insert (i, q);
				D = 0f;
			} else {
				D += d;
			}
		}
		if (newPoints.Count == n - 1) {
			newPoints.Add(new Point (newPoints[newPoints.Count-1].getX (), newPoints [newPoints.Count-1].getY (), newPoints [newPoints.Count-1].getZ (), 0.25f));
		}
		return newPoints.ToArray();
	}

	float GestureDistance(Point[] p1, Point[] p2){
		float distance = 0f;
		for (int i = 0; i < p1.Length; i++) {
			distance += Vector3.Distance (p1 [i].GetPositionVector (), p2 [i].GetPositionVector ());
		}
		return distance / p1.Length;
	}

	float GestureLength(Point[] points){
		float length = 0f;
		for (int i = 1; i < points.Length; i++) {
			length += Vector3.Distance (points [i].GetPositionVector (), points [i - 1].GetPositionVector ());
		}
		return length;
	}

	Gesture NormalizeGesture(Gesture p1, Gesture gesture){   // TODO Maybe normalize based of the time too.
		Gesture newGesture = new Gesture("NEW");
		for(int i = 0; i < gesture.GetPoints().Length; i++){
			newGesture.AddPoint (new Point (
				(p1.GetPoints()[i].getX() + gesture.GetPoints()[i].getX())/2,
				(p1.GetPoints()[i].getY() + gesture.GetPoints()[i].getY())/2,
				(p1.GetPoints()[i].getZ() + gesture.GetPoints()[i].getZ())/2,
				gesture.GetPoints()[i].GetDeltaTime()
			));
			newGesture.AddRotation (gesture.GetRotations() [i]);
		}
		return newGesture;
	}
}