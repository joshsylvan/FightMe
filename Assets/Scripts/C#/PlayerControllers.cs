using UnityEngine;
using System.Collections;
using Valve.VR;

public class PlayerControllers : MonoBehaviour {

	public GameObject valveLeftHand, valveRightHand;
	SteamVR_TrackedObject rightTrackedObject, leftTrackedObject;
	SteamVR_Controller.Device rightController, leftController;

	void Awake(){
		rightTrackedObject = valveRightHand.GetComponent<SteamVR_TrackedObject> ();
		leftTrackedObject = valveLeftHand.GetComponent<SteamVR_TrackedObject> ();
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (rightTrackedObject.isActiveAndEnabled) {
			rightController = SteamVR_Controller.Input ((int)rightTrackedObject.index);
		} else {
			rightController = null;
		}
		if (leftTrackedObject.isActiveAndEnabled) {
			leftController = SteamVR_Controller.Input ((int)leftTrackedObject.index);
		} else {
			leftController = null;
		}
	}

	public SteamVR_Controller.Device GetRightController(){
		return rightController;
	}

	public SteamVR_Controller.Device GetLeftController(){
		return leftController;
	}

	public GameObject GetRightControllerGameObject(){
		return valveRightHand;
	}

	public GameObject GetLeftControllerGameObject(){
		return valveLeftHand;
	}

	public bool AreBothControllersConnected(){
		return rightController != null && leftController != null;
	}

	public bool IsRightControllerConnected(){
		return rightController != null;
	}

	public bool IsLeftControllerConnected(){
		return leftController != null;
	}

}
