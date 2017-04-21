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

		float ratio = 0.825f, maxPathDistance = 0.3f, maxPointDistance = 10f;


//		List<Gesture> classifiedG = new List<Gesture>(); 

//		for (int i = 1; i <= 10; i++) {
//			gl.Init ();
//			gestures = gl.GetClassifiedGesturesM ();
//			gr = new GestureRecognizer ();
			List<Gesture> classifiedG = new List<Gesture> ();
			ratio = 0.8f; maxPathDistance = 0.3f; maxPointDistance = 0.35f;
			classifiedG = gr.ClassifyGestures (gestures, new List<Gesture> (), ratio, maxPathDistance, maxPointDistance);
//			Debug.Log ("Ratio : " + ratio + ", MaxPathDistance: " + maxPathDistance + ", MaxPointDistance: " + maxPointDistance + ", Classified Length: " + classifiedG.Count);
//		}


		for (int i = 0; i < classifiedG.Count; i++) {
			DrawGestureM (classifiedG[i], ""+i);
		}

		//Debug.Log ("Length: " + classifiedG.Count);
//		Debug.Log ("CLength: " + classifiedG.Count);
//		sword.CreateAnimationClipsFromGestures (classifiedG);
//		sword.CylcleAnimations ();
		//draw og gesture
//		DrawGesture(gestures, 5);


//		for (int i = 0; i < classifiedG.Count; i++) {
////			List<Point> tGesture = new List<Point> (gestures[i].GetPoints());
////			gestures [i].SetPoints ( gr.Resample( tGesture, 20) );
//			DrawGestureM(classifiedG[i], i+"");
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
		
	public void DrawGestureM(Gesture gesture, string name){

		GameObject node = Resources.Load ("GNode") as GameObject;
		GameObject ge = new GameObject(name);

		Color col = new Color (Random.Range (0, 256), Random.Range (0, 256), Random.Range (0, 256), 1);
		MeshRenderer mr = node.GetComponent<MeshRenderer> ();
		Material mat = new Material (Shader.Find ("Standard"));
		mat.color = col;
		mr.material = mat;

//		600 x 480

		float mult = 1;
		for (int j = 0; j < gesture.GetMatrixArray().Length; j++) {
			GameObject temp = Instantiate (node);
			temp.transform.SetParent (ge.transform);
			temp.transform.localPosition = gesture.GetMatrixArray () [j].GetPosition ();
			temp.transform.localRotation = gesture.GetMatrixArray () [j].GetRotation ();
		}

	}


}