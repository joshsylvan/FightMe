using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureRecorder : MonoBehaviour {

	List<Point> rightControllerPoints, leftControllerPoints;
	List<Quaternion> rightControllerRotations, leftControllerRotations;
	float ogCountown = 0;
	float countdownRight, countdownLeft;

	public GameObject rightHand, leftHand;
	SteamVR_TrackedObject rightTrackedObject, leftTrackedObject;
	SteamVR_Controller.Device rightController, leftController;
	bool inVR = true;
	public GameObject HMD;

	int gestureIDRight, gestureIDLeft;
	List<Gesture> unclassifiedGesturesRight, unclassifiedGesturesLeft;
	List<Gesture> classifiedGesturesRight, classifiedGesturesLeft;

	//ComboRecorder comboRecorder;

	int fileIndex = 0;
	string name = "Josh";

	void Awake(){
		if (inVR) {
			rightTrackedObject = rightHand.GetComponent<SteamVR_TrackedObject> ();
			leftTrackedObject = leftHand.GetComponent<SteamVR_TrackedObject> ();
		}
	}

	// Use this for initialization
	void Start () {
		rightControllerPoints = new List<Point> ();
		leftControllerPoints = new List<Point> ();
		rightControllerRotations = new List<Quaternion> ();
		leftControllerRotations = new List<Quaternion> ();
		countdownRight = ogCountown;
		countdownLeft = ogCountown;
		unclassifiedGesturesRight = new List<Gesture> ();
		unclassifiedGesturesLeft = new List<Gesture> ();
		classifiedGesturesRight = new List<Gesture> ();
		classifiedGesturesLeft = new List<Gesture> ();
		gestureIDRight = 0;
		gestureIDLeft = 0;
		//comboRecorder = GetComponent<ComboRecorder> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (inVR) {

			if (rightTrackedObject.isActiveAndEnabled) { // Check if the right controller is connected.
				RightControllerRecorder ();
			} else {
				Debug.Log ("Vive Right Controller Disconnected.");
			}

			if (leftTrackedObject.isActiveAndEnabled) { // Check if the left controller is connected.
				LeftControllerRecorder ();
			} else {
				Debug.Log ("Vive Left Controller Disconnnected.");
			}
				
		} else {
			Debug.Log ("CONNECT VIVE YOU FOOL.");
		}
	}

	void RightControllerRecorder(){
		rightController = SteamVR_Controller.Input ((int)rightTrackedObject.index);

		if (rightController.GetPress (SteamVR_Controller.ButtonMask.Grip)) {
			rightController.TriggerHapticPulse (1000, Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
			Debug.Log ("Recording points");
			countdownRight -= Time.deltaTime;
			if (countdownRight <= 0) {
				countdownRight = ogCountown;
				AddPoint (rightTrackedObject, HMD);
			}
		}
		if (rightController.GetPressUp (SteamVR_Controller.ButtonMask.Grip)) {
			// Narrow the gesture down to 10 points.
			Point[] gesturePosition = NormalizeListPosition (rightControllerPoints, 20);
			Quaternion[] gestureRotation = NormalizeListRotation (rightControllerRotations, 20);
			rightControllerPoints = new List<Point> ();
			rightControllerRotations = new List<Quaternion> ();
			if (gesturePosition != null) {
				for (int i = 0; i < gesturePosition.Length; i++) {
					
				}
				Gesture tGesture = new Gesture ( "" + gestureIDRight++, gesturePosition, gestureRotation );
				unclassifiedGesturesRight.Add (tGesture);
				//comboRecorder.AddGestureToCurrentCombo (tGesture);
			}
		}
		if (Input.GetKeyUp (KeyCode.Return)) {
			FileOutput f = new FileOutput ();
			Debug.Log ("CLASSIFY");
			GestureClassifier classifier = new GestureClassifier();
			classifiedGesturesRight = classifier.Classify (unclassifiedGesturesRight, classifiedGesturesRight, 0.5f, 200);
			
			f.GestureOutput (classifiedGesturesRight, "test");
		}
		if (Input.GetKeyUp (KeyCode.O)) {
			FileOutput f = new FileOutput ();
			f.GestureOutput (unclassifiedGesturesRight, name + "-" + fileIndex);
			unclassifiedGesturesRight = new List<Gesture> ();
			Debug.Log ("Output Unclassified Gestures");
		}

	}

	void LeftControllerRecorder(){
		leftController = SteamVR_Controller.Input ((int)leftTrackedObject.index);

		if (leftController.GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) {
			countdownLeft -= Time.deltaTime;
			if (countdownLeft <= 0) {
				countdownLeft = ogCountown;
				AddPoint (leftTrackedObject, HMD);
			}
		}
		if (leftController.GetPressUp (SteamVR_Controller.ButtonMask.Trigger)) {
			// Narrow the gesture down to 10 points.
			Point[] gesturePosition = NormalizeListPosition(leftControllerPoints, 10);
			Quaternion[] gestureRotation = NormalizeListRotation (leftControllerRotations, 10);
			leftControllerPoints = new List<Point> ();
			leftControllerRotations = new List<Quaternion> ();
			if (gesturePosition != null) {
				unclassifiedGesturesLeft.Add (new Gesture (
					"" + gestureIDLeft++,
					gesturePosition,
					gestureRotation
				));
			}
		}
	}

	Point[] NormalizeListPosition(List<Point> l, int length){ // RETUNING NULL
		if (l.Count < length) {
			return null;
		} else {
			int toRemove = l.Count - length;
			if (toRemove == 0) {
				return l.ToArray();
			} else {
				float ratio =  ((float)l.Count / (float)toRemove);
				Debug.Log ("Ratio: " + ratio + "  l.Count: " + l.Count + "  toRemove: " + toRemove);
				List<Point> newList = new List<Point> ();
				for (int i = 0; i < l.Count; i++) {
					if ((i + 1) % ratio >= 1) {
						newList.Add (l [i]);
					}
				}
				if (newList.Count > length) {
					newList.RemoveAt (10);
				}
				Debug.Log (newList.Count);
				return newList.ToArray();
			}

		}
	}

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

	void AddPoint(SteamVR_TrackedObject obj, GameObject HMD){ // TODO: ROTATE THE POINTS RELATIVE TO THE HEADSET POSITION.
		if (obj.Equals (rightTrackedObject)) { //right


			//rightControllerPoints.Add(new Point(
			//	obj.transform.position.x - HMD.transform.position.x,
			//	obj.transform.position.y - HMD.transform.position.y,
			//	obj.transform.position.z - HMD.transform.position.z
			//));
			rightControllerRotations.Add (obj.transform.rotation);

			//MATRIXMETHOD

			Matrix4x4 pointMatrix = HMD.transform.worldToLocalMatrix * obj.transform.localToWorldMatrix;
			rightControllerPoints.Add(new Point(
				pointMatrix.GetPosition().x,
				pointMatrix.GetPosition().y,
				pointMatrix.GetPosition().z
			));





		} else { //left
			leftControllerPoints.Add(new Point(
				obj.transform.position.x - HMD.transform.position.x,
				obj.transform.position.y - HMD.transform.position.y,
				obj.transform.position.z - HMD.transform.position.z
			));
			leftControllerRotations.Add (obj.transform.rotation);
		}
	}

	public List<Gesture> GetUnclassifiedGesturesRight(){
		return unclassifiedGesturesRight;
	}

	public List<Gesture> GetUnclassifiedGesturesLeft(){
		return unclassifiedGesturesLeft;
	}

	public void ResetUnclassifiedGestures(){
		unclassifiedGesturesRight = new List<Gesture> ();
		unclassifiedGesturesLeft = new List<Gesture> ();
	}
}
