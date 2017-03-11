using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

public class NewtonGestureRecorder : MonoBehaviour {

	// VR Hands
	NVRHand leftHand, rightHand;
	NVRHead head;
	bool inVR = true;
	NVRPlayer VRPlayer;

	// Data variables
	List<Point> rightControllerPoints, leftControllerPoints;
	List<Quaternion> rightControllerRotations, leftControllerRotations;

	// Data recordings
	int gestureIDRight, gestureIDLeft;
	List<Gesture> unclassifiedGesturesRight, unclassifiedGesturesLeft;
	List<Gesture> classifiedGesturesRight, classifiedGesturesLeft;

	void Awake(){
		
	}

	// Use this for initialization
	void Start () {

		if (inVR) {
			VRPlayer = GameObject.Find ("NVRPlayer").GetComponent<NVRPlayer> ();
			head = VRPlayer.Head;
			leftHand = VRPlayer.LeftHand;
			rightHand = VRPlayer.RightHand;
		}

		// Data Variables for controllers
		rightControllerPoints = new List<Point> ();
		leftControllerPoints = new List<Point> ();
		rightControllerRotations = new List<Quaternion> ();
		leftControllerRotations = new List<Quaternion> ();
		// Data Recordings
		unclassifiedGesturesRight = new List<Gesture> ();
		unclassifiedGesturesLeft = new List<Gesture> ();
		classifiedGesturesRight = new List<Gesture> ();
		classifiedGesturesLeft = new List<Gesture> ();
		// Getsure ID
		gestureIDRight = 0;
		gestureIDLeft = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (inVR) {
			if (leftHand.CurrentHandState != HandState.Uninitialized) {
				UpdateController (leftHand);
			}
			if (rightHand.CurrentHandState != HandState.Uninitialized) {
				UpdateController (rightHand);
			}

			if (Input.GetKeyUp (KeyCode.Return)) {
				OutputClassifiedGestureCSV ();
			}
			if (Input.GetKeyUp (KeyCode.U)) {
				OutputUnclassifiedGestureCSV();
			}

		}
	}

	void UpdateController(NVRHand hand){
		if (hand.Inputs [NVRButtons.Grip].IsPressed) {
			hand.TriggerHapticPulse (500, NVRButtons.Touchpad);
			Debug.Log ("Recording");
			if (hand.Equals(leftHand)) {
				AddPoint (leftHand, head);
			} else if (hand.Equals(rightHand)) {
				AddPoint (rightHand, head);
			}
		}

		if (hand.Inputs [NVRButtons.Grip].PressUp) {
			if(hand.Equals(leftHand)) {
				Point[] gesturePosition = NormalizeListPosition (leftControllerPoints, 20);
				Quaternion[] gestureRotation = NormalizeListRotation (leftControllerRotations, 20);
				leftControllerPoints = new List<Point> ();
				leftControllerRotations = new List<Quaternion> ();
				if (gesturePosition != null) {
					Gesture tGesture = new Gesture ( "" + gestureIDLeft++, gesturePosition, gestureRotation );
					unclassifiedGesturesLeft.Add (tGesture);
				}
			} else if (hand.Equals(rightHand)){
				Point[] gesturePosition = NormalizeListPosition (rightControllerPoints, 20);
				Quaternion[] gestureRotation = NormalizeListRotation (rightControllerRotations, 20);
				rightControllerPoints = new List<Point> ();
				rightControllerRotations = new List<Quaternion> ();
				if (gesturePosition != null) {
					Gesture tGesture = new Gesture ( "" + gestureIDRight++, gesturePosition, gestureRotation );
					unclassifiedGesturesRight.Add (tGesture);
				}
			}
		}
	}

	void AddPoint(NVRHand hand, NVRHead head){
		if(hand.Equals(leftHand)) {
			leftControllerRotations.Add (hand.gameObject.transform.rotation);
			Matrix4x4 pointMatrix = head.gameObject.transform.worldToLocalMatrix * hand.gameObject.transform.localToWorldMatrix;
			leftControllerPoints.Add(new Point(
				pointMatrix.GetPosition().x,
				pointMatrix.GetPosition().y,
				pointMatrix.GetPosition().z
			));
		} else if (hand.Equals(rightHand)){
			rightControllerRotations.Add (hand.gameObject.transform.rotation);
			Matrix4x4 pointMatrix = head.gameObject.transform.worldToLocalMatrix * hand.gameObject.transform.localToWorldMatrix;
			rightControllerPoints.Add(new Point(
				pointMatrix.GetPosition().x,
				pointMatrix.GetPosition().y,
				pointMatrix.GetPosition().z
			));
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

	void OutputClassifiedGestureCSV(){
		FileOutput f = new FileOutput ();
		Debug.Log ("OUTPUT CLASSIFY");
		// Left
		GestureClassifier classifierL = new GestureClassifier();
		classifiedGesturesLeft = classifierL.Classify (unclassifiedGesturesLeft, classifiedGesturesLeft, 0.5f, 200);
		f.GestureOutput (classifiedGesturesLeft, "leftHandC");
		unclassifiedGesturesLeft = new List<Gesture>();
		// Right
		GestureClassifier classifierR = new GestureClassifier();
		classifiedGesturesRight = classifierR.Classify (unclassifiedGesturesRight, classifiedGesturesRight, 0.5f, 200);
		f.GestureOutput (classifiedGesturesRight, "rightHandC");
		unclassifiedGesturesRight = new List<Gesture>();
	}

	void OutputUnclassifiedGestureCSV(){
		FileOutput f = new FileOutput ();
		Debug.Log ("OUTPUT UNCLASSIFY");
		// Left
		f.GestureOutput (unclassifiedGesturesLeft, "leftHandU");
		// Right
		f.GestureOutput (unclassifiedGesturesRight, "rightHandU");
	}

}
