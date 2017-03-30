using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;
using UnityEngine;

public class GestureClassifier {

	List<Pair> combosToChange;

	public GestureClassifier(){
		this.combosToChange = new List<Pair>();
	}

	public List<Gesture> Classify(List<Gesture> unclassifiedGestures, List<Gesture> classifiedGestures, float minRatio, float maxDistance){
		List<Gesture> gestures = classifiedGestures;
		for (int i = 0; i < unclassifiedGestures.Count; i++) {
			if (gestures.Count >= 1) {
				int result = NaiveRecognizer (unclassifiedGestures [i].GetPoints(), gestures, minRatio, maxDistance);
				if (result == -1) {
					//Debug.Log ("Add new Gesture.");
					gestures.Add (unclassifiedGestures [i]);

				} else {
                    // normalize array
                    //Debug.Log("Adjust Gesture " + unclassifiedGestures[i].GetName() + " and " + classifiedGestures[result].GetName());
                    combosToChange.Add(new Pair(int.Parse(unclassifiedGestures[i].GetName()), int.Parse(classifiedGestures[result].GetName())));
					classifiedGestures [result] = NormalizeGesture (unclassifiedGestures[i], classifiedGestures[result]);
					// Maybe normalize rotations too
				}
			} else {
//				Debug.Log("EMPTY");
				gestures.Add (unclassifiedGestures [i]);
			}
		}
		return gestures;
	}

	Gesture NormalizeGesture(Gesture p1, Gesture gesture){
		Gesture newGesture = new Gesture(gesture.GetName());
		for(int i = 0; i < gesture.GetPoints().Length; i++){
			newGesture.AddPoint (new Point (
				(p1.GetPoints()[i].getX() + gesture.GetPoints()[i].getX())/2,
				(p1.GetPoints()[i].getY() + gesture.GetPoints()[i].getY())/2,
				(p1.GetPoints()[i].getZ() + gesture.GetPoints()[i].getZ())/2
			));
			newGesture.AddRotation (gesture.GetRotations() [i]);
		}
		return newGesture;
	}
		
	// Cluster gestures together, minRatio in the minimum ratio needed to modify a current gesture and max distance is the maximum distance before a new gesture is needed to be defined.
	public int NaiveRecognizer(Point[] points, List<Gesture> patterns, float minRatio, float maxDistance){

		//Ratio of points hit
		int bestRatioIndex = -1;
		float bestRatio = 0f;
		
		for (int k = 0; k < patterns.Count; k++) {
		
			int indexCount = 0;
			float pathDistance = 0f;
			for (int i = 0; i < points.Length; i++) { // for each pont
		
				int indexOfShotestDistance = -1;
				float curShortestDistance = 99999f;
		
				for (int j = 0; j < patterns [k].GetPoints ().Length; j++) { //compare to everypoint in pattern 
		
					float distanceHolder = PointDistance (points [i], patterns [k].GetPoints () [j]);
					if (distanceHolder < curShortestDistance) {
						curShortestDistance = distanceHolder;
						indexOfShotestDistance = j;
					}
		
				}
				pathDistance += curShortestDistance;
				if (!patterns [k].GetPoints () [indexOfShotestDistance].isCompared ()) {
					indexCount++;
					patterns [k].GetPoints () [indexOfShotestDistance].setCompared (true);
				}
		
			}
			float ratio = (1 / (float)patterns [k].GetPoints ().Length * (float)indexCount);
		
			if (ratio > bestRatio) {
				bestRatio = ratio;
				bestRatioIndex = k;
			}
		}

		//Distance of shortest path and its index
		int shortestDistanceIndex = -1;
		float shortestDistsance = Mathf.Infinity;

		for (int i = 0; i < patterns.Count; i++) {

			int indexCount = 0;
			float pathDistance = 0f;

			for (int j = 0; j < points.Length; j++) { //calculate total path distance assuming same number  of nodes
				pathDistance += PointDistance (points[j], patterns[i].GetPoints()[j]);
			}
			pathDistance /= points.Length;
			if (pathDistance <= shortestDistsance) {
				shortestDistsance = pathDistance;
				shortestDistanceIndex = i;
			}

		}
		//Debug.Log ("Best Path = " + shortestDistanceIndex + " Distance: " + shortestDistsance + " thresh " + maxDistance);
		//Debug.Log ("Best Ratio = " + bestRatioIndex + " Ratio: " + bestRatio + " thresh " + minRatio);
		ResetGestures (patterns);

		if (shortestDistsance <= maxDistance && bestRatio >= minRatio) {
			if (shortestDistanceIndex == bestRatioIndex) {
				return shortestDistanceIndex;
			} else {
				return bestRatioIndex;
			}
		} else {
			return -1;
		}
	}

	//c 21
	//a 26
	//a 29

	float PointDistance(Point p1, Point p2){
		return Mathf.Sqrt( Mathf.Pow(p2.getX () - p1.getX (), 2) + Mathf.Pow(p2.getY () - p1.getY (), 2) + Mathf.Pow(p2.getZ () - p1.getZ (), 2) );
	}

	void ResetGestures(List<Gesture> patterns){
		for (int i = 0; i < patterns.Count; i++) {
			for (int j = 0; j < patterns [i].GetPoints().Length; j++) {
				patterns [i].GetPoints() [j].setCompared (false);
			}
		}
	}

    public List<Pair> GetCombosToChange()
    {
        return combosToChange;
    }

}
