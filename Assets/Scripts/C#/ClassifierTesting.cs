using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassifierTesting : MonoBehaviour {

	int testIndex = 20;

	GestureRecognizer gr;
	GestureLoader gl;

	List<Gesture> gestures;

	public TrainedAISword sword;

	// Use this for initialization
	void Start () {
		this.gameObject.AddComponent<GestureLoader> ();
		gl = this.gameObject.GetComponent<GestureLoader> ();
		gl.Init ();
		gestures = gl.GetClassifiedGesturesM ();
		Debug.Log ("Length: " + gestures.Count);

		gr = new GestureRecognizer ();

		//List<Gesture> classifiedG = gr.ClassifyGestures (gestures, new List<Gesture> (), 0.8f, 0.3f, 0.3f);

		//for (int i = 0; i < classifiedG.Count; i++) {
		//	DrawGestureM (classifiedG[i], ""+i);
		//}

		//Debug.Log ("Length: " + classifiedG.Count);

		sword.CreateAnimationClipsFromGestures (gestures);
		sword.CylcleAnimations ();
		//draw og gesture
//		DrawGesture(gestures, 5);

		/*
		for (int i = 0; i < classifiedG.Count; i++) {
//			List<Point> tGesture = new List<Point> (gestures[i].GetPoints());
//			gestures [i].SetPoints ( gr.Resample( tGesture, 20) );
			Gesture g = classifiedG[i];
			g.SetPoints(gr.Resample(new List<Point> (g.GetPoints()), 20));
			DrawGesture(g, ""+i);
		} 
		*/

//		trainingGesture = gestures[testIndex];
//		gestures.RemoveAt (testIndex);
//		testGestures = gestures.ToArray();

//		Debug.Log (trainingGesture.GetPoints ().Length);

//		gr = new GestureRecognizer ();
//		int draw = gr.NaiveRecognizer(trainingGesture.GetPoints() , gestures, 0.1f, 0.25f, 0.25f);
//		Debug.Log (draw);
//		DrawGesture (gestures, draw);
//		DrawGesture (trainingGesture, "Training");

//		Gesture newGesture = NormalizeGesture (trainingGesture, gestures[draw]);
//		DrawGesture (newGesture, "TESTS");
	}

	// Update is called once per frame
	void Update () {
		
	}
		
	public void DrawGestureM(Gesture gesture, string name){

		GameObject node = Resources.Load ("SwordNode") as GameObject;
		GameObject ge = new GameObject(name);
		float mult = 1;
		for (int j = 0; j < gesture.GetMatrixArray().Length; j++) {
			GameObject temp = Instantiate (node);
			temp.transform.SetParent (ge.transform);
			temp.transform.localPosition = gesture.GetMatrixArray () [j].GetPosition ();
			temp.transform.localRotation = gesture.GetMatrixArray () [j].GetRotation ();
		}

	}


}
