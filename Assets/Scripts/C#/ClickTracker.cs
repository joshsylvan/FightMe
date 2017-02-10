using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Valve.VR;

public class ClickTracker : MonoBehaviour {


	List<Point> points, points2;
	float OGCountdown = 0;
	float countdown, countdown2;
	bool setAnchor = false, setAnchor2 = false;
	float OGX, OGX2;
	float OGY, OGY2;
	float OGZ, OGZ2;

	public GameObject rightHand, leftHand;
	SteamVR_TrackedObject rightTrackedObject, leftTrackedObject;
	SteamVR_Controller.Device rightController, leftController;
	bool inVR = true;
	public CoordianteOuput cOut; 
	public GameObject cameraEyes;

	void Awake(){
		rightTrackedObject = rightHand.GetComponent<SteamVR_TrackedObject> ();
		leftTrackedObject = leftHand.GetComponent<SteamVR_TrackedObject> ();
		//rightController = SteamVR_Controller.Input( (int) rightTrackedObject.index );
	}

	// Use this for initialization
	void Start () {
		points = new List<Point> ();
		points2 = new List<Point> ();
		countdown = OGCountdown;
		countdown2 = OGCountdown;
	}
		
	void FixedUpdate(){
		//rightController = SteamVR_Controller.Input ((int)rightTrackedObject.index);
		//leftController = SteamVR_Controller.Input ((int)leftTrackedObject.index);
	}

	// Update is called once per frame
	void Update () {
		
		if (!inVR) {
			if (Input.GetMouseButton (0)) {
				if (!setAnchor) {
					setAnchor = true;
					OGX = Input.mousePosition.x;
					OGY = Input.mousePosition.y;
					OGZ = Input.mousePosition.z;
				}
				countdown -= Time.deltaTime;
				if (countdown <= 0) {
					countdown = OGCountdown;
					AddPoint (OGX, OGY, OGZ);
					//PrintMouseCoordinates ();
				}
			} else if (Input.GetMouseButtonUp (0)) {
				// Do recognition
				Recognizer r = new Recognizer ();
				r.NaiveRecognizer (points.ToArray ());
				points = new List<Point> ();
				countdown = OGCountdown;
				setAnchor = false;
			}
		} else {
			if (rightTrackedObject.isActiveAndEnabled) {
				rightController = SteamVR_Controller.Input ((int)rightTrackedObject.index);
			
				if (rightController.GetPress (SteamVR_Controller.ButtonMask.Trigger)) {
					if (!setAnchor) {
						setAnchor = true;
						OGX = RotateAroundYAxis (rightTrackedObject.transform.position.x, rightTrackedObject.transform.position.z, "x") * 100;
						OGY = rightTrackedObject.transform.position.y * 100;
						OGZ = RotateAroundYAxis (rightTrackedObject.transform.position.x, rightTrackedObject.transform.position.z, "z") * 100;
					}
					countdown -= Time.deltaTime;
					if (countdown <= 0) {
						countdown = OGCountdown;
						AddPoint (OGX, OGY, OGZ);
						cOut.SetRight (points [points.Count - 1].getX () + "\n" + points [points.Count - 1].getY () + "\n" + points [points.Count - 1].getZ () + " ");
					}
				}
				if (rightController.GetPressUp (SteamVR_Controller.ButtonMask.Trigger)) {
					Recognizer r = new Recognizer ();
					cOut.SetRight (r.NaiveRecognizer (points.ToArray ()));
					points = new List<Point> ();
					countdown = OGCountdown;
					setAnchor = false;
				}
			}

			if (leftTrackedObject.isActiveAndEnabled) {
				leftController = SteamVR_Controller.Input ((int)leftTrackedObject.index);

				if (leftController.GetPress (SteamVR_Controller.ButtonMask.Trigger)) {
					if (!setAnchor2) {
						setAnchor2 = true;
						OGX2 = RotateAroundYAxis (leftTrackedObject.transform.position.x, leftTrackedObject.transform.position.z, "x") * 100;
						OGY2 = leftTrackedObject.transform.position.y * 100;
						OGZ2 = RotateAroundYAxis (leftTrackedObject.transform.position.x, leftTrackedObject.transform.position.z, "z") * 100;
					}
					countdown2 -= Time.deltaTime;
					if (countdown2 <= 0) {
						countdown2 = OGCountdown;
						AddPointLeft (OGX2, OGY2, OGZ2);
						cOut.SetLeft (points2 [points2.Count - 1].getX () + "\n" + points2 [points2.Count - 1].getY () + "\n" + points2 [points2.Count - 1].getZ () + " ");
					}
				}
				if (leftController.GetPressUp (SteamVR_Controller.ButtonMask.Trigger)) {
					Recognizer r = new Recognizer ();
					cOut.SetLeft (r.NaiveRecognizer (points2.ToArray ()));
					points2 = new List<Point> ();
					countdown2 = OGCountdown;  
					setAnchor2 = false;
				}
			}
		}
	}

	void AddPoint(float OGX, float OGY, float OGZ){
		if (!inVR) {
			points.Add (new Point (
				Input.mousePosition.x - OGX, 
				Input.mousePosition.y - OGY, 
				Input.mousePosition.z - OGZ)
			);
		} else {
			points.Add (new Point(
				RotateAroundYAxis( rightTrackedObject.transform.position.x, rightTrackedObject.transform.position.z, "x") * 100 - OGX, 
				rightTrackedObject.transform.position.y * 100 - OGY, 
				RotateAroundYAxis( rightTrackedObject.transform.position.x, rightTrackedObject.transform.position.z, "z") * 100 - OGZ)
			);
		}
	}
	void AddPointLeft(float OGX, float OGY, float OGZ){
		points2.Add (new Point(
			RotateAroundYAxis( leftTrackedObject.transform.position.x, leftTrackedObject.transform.position.z, "x") * 100 - OGX, 
			leftTrackedObject.transform.position.y * 100 - OGY, 
			RotateAroundYAxis( leftTrackedObject.transform.position.x, leftTrackedObject.transform.position.z, "z") * 100 - OGZ)
		);
	}

	float RotateAroundYAxis(float x, float z, string co){
		float yAngle = Mathf.Deg2Rad * cameraEyes.transform.localRotation.eulerAngles.y;
		switch (co) {
		case "x":
			return -(z * Mathf.Sin (yAngle) - x * Mathf.Cos (yAngle));
		case "z":
			return z * Mathf.Cos (yAngle) - x * Mathf.Sin (yAngle);
		default:
			Debug.Log ("No Such Axis to roatet in RotateAroundYAxis()");
			return 0;
		}
	}

	void PrintMouseCoordinates(){
		Debug.Log ((Input.mousePosition.x - OGX)+ " " + (Input.mousePosition.y -OGY) + " " + (Input.mousePosition.z-OGZ));
	}
}
