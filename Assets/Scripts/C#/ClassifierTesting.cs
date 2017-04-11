using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassifierTesting : MonoBehaviour {

	int testIndex = 20;

	GestureRecognizer gr;
	GestureLoader gl;

	List<Gesture> gestures;
	Gesture[] testGestures;
	Gesture trainingGesture;

	public TrainedAISword sword;

	// Use this for initialization
	void Start () {
		this.gameObject.AddComponent<GestureLoader> ();
		gl = this.gameObject.GetComponent<GestureLoader> ();
		gl.Init ();
		gestures = gl.GetClassifiedGestures ();
		Debug.Log ("Length: " + gestures.Count);

		gr = new GestureRecognizer ();

		List<Gesture> classifiedG = gr.ClassifyGestures (gestures, new List<Gesture> (), 0.5f, 0.05f, 2);
//		for (int i = 0; i < classifiedG.Count; i++) {
//			DrawGesture (classifiedG, i);
//		}
		Debug.Log ("Length: " + classifiedG.Count);
		sword.CreateAnimationClipsFromGestures (classifiedG);
		sword.CylcleAnimations ();
		//draw og gesture
//		DrawGesture(gestures, 5);

//		for (int i = 0; i < gestures.Count; i++) {
//			List<Point> tGesture = new List<Point> (gestures[i].GetPoints());
//			gestures [i].SetPoints ( gr.Resample( tGesture, 20) );
//		} 

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

	public void DrawGesture(List<Gesture> gestures, int i){

		GameObject node = Resources.Load ("SwordNode") as GameObject;
		GameObject gesture = new GameObject("GGG");
		float mult = 1;

		for (int j = 0; j < gestures [i].GetPoints ().Length; j++) {
			GameObject temp = Instantiate (node);
			temp.transform.rotation = gestures [i].GetRotations () [j];
			temp.transform.SetParent (gesture.transform);
			temp.transform.localPosition = new Vector3 (
				gestures [i].GetPoints ()[j].getX()*mult, 
				gestures [i].GetPoints ()[j].getY()*mult, 
				gestures [i].GetPoints ()[j].getZ()*mult
			);

		}

	}

	public void DrawGesture(Gesture gesture, string name){

		GameObject node = Resources.Load ("SwordNode") as GameObject;
		GameObject ge = new GameObject(name);
		float mult = 1;
		for (int j = 0; j < gesture.GetPoints ().Length; j++) {
			GameObject temp = Instantiate (node);
			temp.transform.rotation = gesture.GetRotations () [j];
			temp.transform.SetParent (ge.transform);
			temp.transform.localPosition = new Vector3 (
				gesture.GetPoints ()[j].getX()*mult, 
				gesture.GetPoints ()[j].getY()*mult, 
				gesture.GetPoints ()[j].getZ()*mult
			);
		}

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
