using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewtonVR;

public class NewtonGestureRecorderMatrix : MonoBehaviour {

	// VR Hands
	NVRHand leftHand, rightHand;
	NVRHead head;
	bool inVR = true;
	NVRPlayer VRPlayer;


	List<Matrix4x4> rightHandMatrix, leftHandMatrix;
	List<float> rightHandTimes, leftHandTimes;

	// Timers
	float leftHandDeltaTime = 0, rightHandDeltaTime = 0;

	// Data recordings
	int gestureIDRight, gestureIDLeft;
	List<Gesture> unclassifiedGesturesRight, unclassifiedGesturesLeft;
	List<Gesture> classifiedGesturesRight, classifiedGesturesLeft;

	// Combo
	ComboRecorder comboRecorder;

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

		//Matrix
		rightHandMatrix = new List<Matrix4x4>();
		leftHandMatrix = new List<Matrix4x4>();
		rightHandTimes = new List<float> ();
		leftHandTimes = new List<float> ();

		// Data Recordings
		unclassifiedGesturesRight = new List<Gesture> ();
		unclassifiedGesturesLeft = new List<Gesture> ();
		classifiedGesturesRight = new List<Gesture> ();
		classifiedGesturesLeft = new List<Gesture> ();
		// Gesture ID
		gestureIDRight = 0;
		gestureIDLeft = 0;
		// Combo
		comboRecorder = GetComponent<ComboRecorder>();
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
			//Debug.Log ("Recording");
			if (hand.Equals(leftHand)) {
				AddPoint (leftHand, head);
				leftHandDeltaTime += Time.deltaTime;
			} else if (hand.Equals(rightHand)) {
				AddPoint (rightHand, head);
				rightHandDeltaTime += Time.deltaTime;
			}
		}

		if (hand.Inputs [NVRButtons.Grip].PressUp) {
			if(hand.Equals(leftHand)) {
				leftHandDeltaTime = 0;
				if (leftHandMatrix.Count >= 20) {
					Gesture g = new Gesture ("" + gestureIDLeft++, ReduceResolution(leftHandMatrix, 20),  ReduceResolution(leftHandTimes, 20));
					unclassifiedGesturesLeft.Add (g);
					comboRecorder.AddGestureToCurrentComboLeft (int.Parse( g.GetName ()));
				}
				leftHandMatrix = new List<Matrix4x4> ();
				leftHandTimes = new List<float> ();
				/*
				//Point[] gesturePosition = NormalizeListPosition (leftControllerPoints, 20);
				//Quaternion[] gestureRotation = NormalizeListRotation (leftControllerRotations, 20);
				//leftControllerPoints = new List<Point> ();
				//leftControllerRotations = new List<Quaternion> ();
				if (gesturePosition != null) {
					Gesture tGesture = new Gesture ( "" + gestureIDLeft++, gesturePosition, gestureRotation );
					unclassifiedGesturesLeft.Add (tGesture);
					comboRecorder.AddGestureToCurrentComboLeft (int.Parse( tGesture.GetName ()));
				}
				*/
			} else if (hand.Equals(rightHand)){
				rightHandDeltaTime = 0;
				if (rightHandMatrix.Count >= 20) {
					Gesture g = new Gesture ("" + gestureIDRight++, ReduceResolution(rightHandMatrix, 20), ReduceResolution(rightHandTimes, 20));
					unclassifiedGesturesRight.Add (g);
					comboRecorder.AddGestureToCurrentComboRight (int.Parse( g.GetName ()));
				}
				rightHandMatrix = new List<Matrix4x4> ();
				rightHandTimes = new List<float> ();
				/*
				//Point[] gesturePosition = NormalizeListPosition (rightControllerPoints, 20);
				//Quaternion[] gestureRotation = NormalizeListRotation (rightControllerRotations, 20);
				//rightControllerPoints = new List<Point> ();
				//rightControllerRotations = new List<Quaternion> ();
				if (gesturePosition != null) {
					Gesture tGesture = new Gesture ( "" + gestureIDRight++, gesturePosition, gestureRotation );
					unclassifiedGesturesRight.Add (tGesture);
					comboRecorder.AddGestureToCurrentComboRight (int.Parse( tGesture.GetName ()));
				}
				*/
			}
		}
	}

	void AddPoint(NVRHand hand, NVRHead head){
		if(hand.Equals(leftHand)) {
			Matrix4x4 pointMatrix = head.gameObject.transform.worldToLocalMatrix * hand.gameObject.transform.localToWorldMatrix;
			leftHandMatrix.Add (pointMatrix);
			leftHandTimes.Add (leftHandDeltaTime);
		} else if (hand.Equals(rightHand)){
			Matrix4x4 pointMatrix = head.gameObject.transform.worldToLocalMatrix * hand.gameObject.transform.localToWorldMatrix;
			rightHandMatrix.Add (pointMatrix);
			rightHandTimes.Add (rightHandDeltaTime);
		}
	}

	void OutputClassifiedGestureCSV(){
		/*
		FileOutput f = new FileOutput ();
		Debug.Log ("OUTPUT CLASSIFY");
		// Left
		GestureClassifier classifierL = new GestureClassifier();
		classifiedGesturesLeft = classifierL.Classify (unclassifiedGesturesLeft, classifiedGesturesLeft, 0.5f, 200);
		f.GestureOutput (classifiedGesturesLeft, "leftHandC");
        Pair[] combosToChangeL = classifierL.GetCombosToChange().ToArray();
        comboRecorder.UpdateComboIDsLeft(combosToChangeL);
        unclassifiedGesturesLeft = new List<Gesture>();
		// Right
		GestureClassifier classifierR = new GestureClassifier();
		classifiedGesturesRight = classifierR.Classify (unclassifiedGesturesRight, classifiedGesturesRight, 0.5f, 200);
		f.GestureOutput (classifiedGesturesRight, "rightHandC");
        Pair[] combosToChangeR = classifierR.GetCombosToChange().ToArray();
        comboRecorder.UpdateComboIDsRight(combosToChangeR);
        unclassifiedGesturesLeft = new List<Gesture>();
        unclassifiedGesturesRight = new List<Gesture>();
        */
	}

	void OutputUnclassifiedGestureCSV(){
		FileOutput f = new FileOutput ();
		Debug.Log ("OUTPUT UNCLASSIFY");
		// Left
		f.GestureOutput (unclassifiedGesturesLeft, "leftHandU");
		// Right
		f.GestureOutput (unclassifiedGesturesRight, "rightHandU");
	}

	Matrix4x4[] ReduceResolution(List<Matrix4x4> l, int length){ // RETUNING NULL
		if (l.Count < length) {
			return null;
		} else {
			int toRemove = l.Count - length;
			if (toRemove == 0) {
				return l.ToArray();
			} else {
				float ratio =  ((float)l.Count / (float)toRemove);
				//Debug.Log ("Ratio: " + ratio + "  l.Count: " + l.Count + "  toRemove: " + toRemove);
				List<Matrix4x4> newList = new List<Matrix4x4> ();
				for (int i = 0; i < l.Count; i++) {
					if ((i + 1) % ratio >= 1) {
						newList.Add (l [i]);
					}
				}
				if (newList.Count > length) {
					newList.RemoveAt (20);
				}
				//Debug.Log (newList.Count);
				return newList.ToArray();
			}

		}
	}

	float[] ReduceResolution(List<float> l, int length){ // RETUNING NULL
		if (l.Count < length) {
			return null;
		} else {
			int toRemove = l.Count - length;
			if (toRemove == 0) {
				return l.ToArray();
			} else {
				float ratio =  ((float)l.Count / (float)toRemove);
				//Debug.Log ("Ratio: " + ratio + "  l.Count: " + l.Count + "  toRemove: " + toRemove);
				List<float> newList = new List<float> ();
				for (int i = 0; i < l.Count; i++) {
					if ((i + 1) % ratio >= 1) {
						newList.Add (l [i]);
					}
				}
				if (newList.Count > length) {
					newList.RemoveAt (20);
				}
				//Debug.Log (newList.Count);
				return newList.ToArray();
			}

		}
	}

}
