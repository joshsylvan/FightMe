using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningTester : MonoBehaviour {

	GestureClassifier gc;
	List<Gesture> patterns;

	// Use this for initialization
	void Start () {
//		patterns = new List<Gesture> ();
//
//		patterns.Add ( new Gesture( "Horizontal Right", new Point[]{
//			new Point(0, 0, 0), 
//			new Point(20, 0, 0), 
//			new Point(40, 0, 0), 
//			new Point(60, 0, 0), 
//			new Point(80, 0, 0), 
//			new Point(100, 0, 0), 
//			new Point(120, 10, 0), 
//			new Point(140, 20, 0), 
//			new Point(160, 30, 0), 
//			new Point(180, 0, 0), 
//			new Point(200, 0, 0)
//		}));
//
//		patterns.Add ( new Gesture( "Horizontal Right", new Point[]{
//			new Point(0, 0, 0), 
//			new Point(24, 0, 0), 
//			new Point(44, 0, 0), 
//			new Point(64, 0, 0), 
//			new Point(84, 0, 0), 
//			new Point(104, 0, 0), 
//			new Point(124, 0, 0), 
//			new Point(144, 0, 0), 
//			new Point(164, 0, 0), 
//			new Point(184, 0, 0), 
//			new Point(204, 0, 0)
//		}));
//
//		patterns.Add ( new Gesture( "Horizontal Right", new Point[]{
//			new Point(0, 0, 0), 
//			new Point(28, 0, 0), 
//			new Point(48, 0, 0), 
//			new Point(68, 0, 0), 
//			new Point(88, 0, 0), 
//			new Point(108, 0, 0), 
//			new Point(128, 0, 0), 
//			new Point(148, 0, 0), 
//			new Point(168, 0, 0), 
//			new Point(188, 0, 0), 
//			new Point(208, 0, 0)
//		}));

		patterns = new List<Gesture> ();
		//Horizontal Stroke
//		patterns.Add ( new Gesture( "Horizontal Right", new Point[]{new Point(0, 0, 0), new Point(20, 0, 0), new Point(40, 0, 0), new Point(60, 0, 0), new Point(80, 0, 0), new Point(100, 0, 0), new Point(120, 0, 0), new Point(140, 0, 0), new Point(160, 0, 0), new Point(180, 0, 0), new Point(200, 0, 0)}));
//		patterns.Add ( new Gesture( "Horizontal Left", new Point[]{new Point(-200, 0, 0), new Point(-180, 0, 0), new Point(-160, 0, 0), new Point(-140, 0, 0), new Point(-120, 0, 0), new Point(-100, 0, 0), new Point(-80, 0, 0), new Point(-60, 0, 0), new Point(-40, 0, 0), new Point(-20, 0, 0), new Point(0, 0, 0)}));
		//Vertical Stroke
//		patterns.Add ( new Gesture( "Vertical Up", new Point[]{new Point(0, 1, 0), new Point(0, 2, 0), new Point(0, 3, 0), new Point(0, 4, 0), new Point(0, 100, 0), new Point(0, 120, 0), new Point(0, 140, 0), new Point(0, 160, 0), new Point(0, 180, 0), new Point(0, 200, 0)}));
//		patterns.Add ( new Gesture( "Vertical Down", new Point[]{new Point(0, -20, 0), new Point(0, -40, 0), new Point(0, -60, 0), new Point(0, -80, 0), new Point(0, -100, 0), new Point(0, -120, 0), new Point(0, -140, 0), new Point(0, -160, 0), new Point(0, -180, 0), new Point(0, -200, 0)}));

		//Diagonal
//		patterns.Add ( new Gesture( "Right Up", new Point[]{new Point(0, 0, 0), new Point(20, 20, 0), new Point(40, 40, 0), new Point(60, 60, 0), new Point(80, 80, 0), new Point(100, 100, 0), new Point(120, 120, 0), new Point(140, 140, 0), new Point(160, 160, 0), new Point(180, 180, 0)}));
//		patterns.Add ( new Gesture( "Left Up", new Point[]{new Point(0, 0, 0), new Point(-20, 20, 0), new Point(-40, 40, 0), new Point(-60, 60, 0), new Point(-80, 80, 0), new Point(-100, 100, 0), new Point(-120, 120, 0), new Point(-140, 140, 0), new Point(-160, 160, 0), new Point(-180, 180, 0)}));

		//shapes
//		patterns.Add ( new Gesture( "Circle", new Point[]{ new Point(0, 0, 0), new Point(10, 30, 0), new Point(20, 50, 0), new Point(40, 60, 0), new Point(70, 70, 0), new Point(100, 60, 0), new Point(120, 50, 0), new Point(130, 30, 0), new Point(140, 0, 0), new Point(130, -30, 0), new Point(120, -50, 0), new Point(100, -60, 0), new Point(70, -70, 0), new Point(40, -60, 0), new Point(20, -50, 0), new Point(10, -30, 0) } ) );
//		patterns.Add ( new Gesture( "Square", new Point[]{ new Point(0, 0, 0), new Point(0, 40, 0), new Point(0, 60, 0), new Point(0, 80, 0),new Point(20, 80, 0), new Point(40, 80, 0),  new Point(60, 80, 0), new Point(80, 80, 0), new Point(80, 60, 0), new Point(80, 40, 0), new Point(80, 20, 0), new Point(80, 0, 0), new Point(60, 0, 0), new Point(40, 0, 0), new Point(20, 0, 0)  } ) );
		//	

//		patterns.Add ( new Gesture( "Vertical Up", new Point[]{new Point(0, 20, 0), new Point(0, 40, 0), new Point(0, 60, 0), new Point(0, 80, 0), new Point(0, 100, 0), new Point(0, 120, 0), new Point(0, 140, 0), new Point(0, 160, 0), new Point(0, 180, 0), new Point(0, 200, 0)}));

//		patterns.Add ( new Gesture( "1", new Point[]{
//			new Point(0, 0, 0), 
//			new Point(1, 0, 0), 
//			new Point(2, 0, 0), 
//			new Point(3, 0, 0), 
//			new Point(4, 0, 0), 
//			new Point(5, 0, 0), 
//			new Point(6, 0, 0), 
//			new Point(7, 0, 0), 
//			new Point(8, 0, 0), 
//			new Point(9, 0, 0), 
//			new Point(10, 0, 0)
//		}));
		patterns.Add(new Gesture( "2", new Point[]{
			new Point(0, 0f, 0), 
			new Point(1, 1, 0), 
			new Point(2, 2f, 0), 
			new Point(3, 3, 0), 
			new Point(4, 4f, 0), 
			new Point(5, 5, 0), 
			new Point(6, 6f, 0), 
			new Point(7, 7, 0), 
			new Point(8, 8f, 0), 
			new Point(9, 9, 0), 
			new Point(10, 10f, 0)
		}));
		patterns.Add(new Gesture( "3", new Point[]{
			new Point(0, 1f, 0), 
			new Point(1, 2, 0), 
			new Point(2, 3f, 0), 
			new Point(3, 2, 0), 
			new Point(4, 1f, 0), 
			new Point(5, 2, 0), 
			new Point(6, 3f, 0), 
			new Point(7, 2, 0), 
			new Point(8, 1f, 0), 
			new Point(9, 2, 0), 
			new Point(10, 3f, 0)
		}));
				
		gc = new GestureClassifier ();
		List<Gesture> test = new List<Gesture> ();

		test.Add(new Gesture( "1", new Point[]{
			new Point(0, 2, 0), 
			new Point(1, 3, 0), 
			new Point(2, 4, 0), 
			new Point(3, 5, 0), 
			new Point(4, 6, 0), 
			new Point(5, 7, 0), 
			new Point(6, 8, 0), 
			new Point(7, 9, 0), 
			new Point(8, 10, 0), 
			new Point(9, 11, 0), 
			new Point(10, 12, 0)
		}));

//		test.Add(new Gesture( "2", new Point[]{
//			new Point(0, 0.5f, 0), 
//			new Point(-1, 1, 0), 
//			new Point(-2, 2.5f, 0), 
//			new Point(-3, 3, 0), 
//			new Point(-4, 4.5f, 0), 
//			new Point(-5, 5, 0), 
//			new Point(-6, 6.5f, 0), 
//			new Point(-7, 7, 0), 
//			new Point(-8, 8.5f, 0), 
//			new Point(-9, 9, 0), 
//			new Point(-10, 10.5f, 0)
//		}));


		List<Gesture> result = gc.Classify(test, patterns, 0.8f, 100f);
//		Debug.Log ( result.Count );
//		foreach (Gesture p in patterns) {
//			Debug.Log (p.getName());
//		}
//
		foreach (Gesture p in result) {
			DrawGesture (p);
		}
//		foreach (Gesture p in test) {
//			DrawGesture2 (p);
//		}

	}

	void DrawGesture(Gesture pat){
		GameObject path = new GameObject(pat.GetName());
		GameObject node = Resources.Load ("GNode") as GameObject;
		foreach (Point p in pat.GetPoints()) {
			GameObject temp = Instantiate (node);
			temp.transform.position = new Vector3 (
				p.getX(),
				p.getY(),
				p.getZ()
			);
			temp.transform.SetParent (path.transform);
		}
	}

	void DrawGesture2(Gesture pat){
		GameObject path = new GameObject(pat.GetName());
		GameObject node = Resources.Load ("GNode2") as GameObject;
		foreach (Point p in pat.GetPoints()) {
			GameObject temp = Instantiate (node);
			temp.transform.position = new Vector3 (
				p.getX(),
				p.getY(),
				p.getZ()
			);
			temp.transform.SetParent (path.transform);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
