using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Recognizer  {

	List<Gesture> patterns;

	public Recognizer(){
		patterns = new List<Gesture> ();
		//Horizontal Stroke
//		patterns.Add ( new Gesture( "Horizontal Right", new Point[]{new Point(0, 0, 0), new Point(20, 0, 0), new Point(40, 0, 0), new Point(60, 0, 0), new Point(80, 0, 0), new Point(100, 0, 0), new Point(120, 0, 0), new Point(140, 0, 0), new Point(160, 0, 0), new Point(180, 0, 0), new Point(200, 0, 0)}));
//		patterns.Add ( new Gesture( "Horizontal Left", new Point[]{new Point(-200, 0, 0), new Point(-180, 0, 0), new Point(-160, 0, 0), new Point(-140, 0, 0), new Point(-120, 0, 0), new Point(-100, 0, 0), new Point(-80, 0, 0), new Point(-60, 0, 0), new Point(-40, 0, 0), new Point(-20, 0, 0), new Point(0, 0, 0)}));
//		//Vertical Stroke
//		patterns.Add ( new Gesture( "Vertical Up", new Point[]{new Point(0, 20, 0), new Point(0, 40, 0), new Point(0, 60, 0), new Point(0, 80, 0), new Point(0, 100, 0), new Point(0, 120, 0), new Point(0, 140, 0), new Point(0, 160, 0), new Point(0, 180, 0), new Point(0, 200, 0)}));
//		patterns.Add ( new Gesture( "Vertical Down", new Point[]{new Point(0, -20, 0), new Point(0, -40, 0), new Point(0, -60, 0), new Point(0, -80, 0), new Point(0, -100, 0), new Point(0, -120, 0), new Point(0, -140, 0), new Point(0, -160, 0), new Point(0, -180, 0), new Point(0, -200, 0)}));
//
//		//Diagonal
//		patterns.Add ( new Gesture( "Right Up", new Point[]{new Point(0, 0, 0), new Point(20, 20, 0), new Point(40, 40, 0), new Point(60, 60, 0), new Point(80, 80, 0), new Point(100, 100, 0), new Point(120, 120, 0), new Point(140, 140, 0), new Point(160, 160, 0), new Point(180, 180, 0)}));
//		patterns.Add ( new Gesture( "Left Up", new Point[]{new Point(0, 0, 0), new Point(-20, 20, 0), new Point(-40, 40, 0), new Point(-60, 60, 0), new Point(-80, 80, 0), new Point(-100, 100, 0), new Point(-120, 120, 0), new Point(-140, 140, 0), new Point(-160, 160, 0), new Point(-180, 180, 0)}));
//
//		//shapes
//		patterns.Add ( new Gesture( "Circle", new Point[]{ new Point(0, 0, 0), new Point(10, 30, 0), new Point(20, 50, 0), new Point(40, 60, 0), new Point(70, 70, 0), new Point(100, 60, 0), new Point(120, 50, 0), new Point(130, 30, 0), new Point(140, 0, 0), new Point(130, -30, 0), new Point(120, -50, 0), new Point(100, -60, 0), new Point(70, -70, 0), new Point(40, -60, 0), new Point(20, -50, 0), new Point(10, -30, 0) } ) );
//		patterns.Add ( new Gesture( "Square", new Point[]{ new Point(0, 0, 0), new Point(0, 40, 0), new Point(0, 60, 0), new Point(0, 80, 0),new Point(20, 80, 0), new Point(40, 80, 0),  new Point(60, 80, 0), new Point(80, 80, 0), new Point(80, 60, 0), new Point(80, 40, 0), new Point(80, 20, 0), new Point(80, 0, 0), new Point(60, 0, 0), new Point(40, 0, 0), new Point(20, 0, 0)  } ) );
		//		Debug.Log(PointDistance (new Point( -7, 4, 3 ), new Point(17, 6, 2.5f)));
	}



	public string NaiveRecognizer(Point[] points){

		int patternIndex = -1;
		float bestRatio = 0f;

		for (int k = 0; k < patterns.Count; k++) {

			int indexCount = 0;
			float pathDistance = 0f;
			for (int i = 0; i < points.Length; i++) { // for each pont

				int indexOfShotestDistance = -1;
				float curShortestDistance = 99999f;
	
				for (int j = 0; j < patterns [k].GetPoints().Length; j++) { //compare to everypoint in pattern 
				
					float distanceHolder = PointDistance (points [i], patterns [k].GetPoints() [j]);
					if (distanceHolder < curShortestDistance) {
						curShortestDistance = distanceHolder;
						indexOfShotestDistance = j;
					}

				}
				pathDistance += curShortestDistance;
				if (!patterns [k].GetPoints() [indexOfShotestDistance].isCompared ()) {
					indexCount++;
					patterns [k].GetPoints() [indexOfShotestDistance].setCompared (true);
				}

			}
			float ratio = (1 / (float)patterns [k].GetPoints().Length * (float)indexCount);

			if (ratio > bestRatio) {
				bestRatio = ratio;
				patternIndex = k;
			}
//			Debug.Log ("Path : " + pathDistance / points.Length + " Ratio : " + ratio);
		
		}
		ResetPatters ();
		return patterns[patternIndex].GetName();
	}
		

	float PointDistance(Point p1, Point p2){
		return Mathf.Sqrt( Mathf.Pow(p2.getX () - p1.getX (), 2) + Mathf.Pow(p2.getY () - p1.getY (), 2) + Mathf.Pow(p2.getZ () - p1.getZ (), 2) );
	}
	void ResetPatters(){
		for (int i = 0; i < patterns.Count; i++) {
			for (int j = 0; j < patterns [i].GetPoints().Length; j++) {
				patterns [i].GetPoints() [j].setCompared (false);
			}
		}
	}

}
