using UnityEngine;
using System.Collections;

public class ObjectPickUp : MonoBehaviour {

	public PlayerControllers playerControllers;
	public GameObject leftHand, rightHand;
	GameObject currentLeftObject, currentRightObject;

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate(){
		if (playerControllers.AreBothControllersConnected ()) {
			UpdateHandPositions ();
			PickUpObject (playerControllers.GetLeftController (), leftHand);
			PickUpObject (playerControllers.GetRightController (), rightHand);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}

	void PickUpObject(SteamVR_Controller.Device device, GameObject hand){
		
		if (device.GetPressUp (SteamVR_Controller.ButtonMask.Touchpad)) {
			GameObject newGO = hand.GetComponent<HandPickUp> ().GetObjectInContactWith ();
			if (newGO != null && !newGO.GetComponent<PickUp>().IsHeld()) {
				Debug.Log ("PickUp!!");
			}

		}

	}

	void UpdateHandPositions(){
		leftHand.transform.position = playerControllers.GetLeftControllerGameObject().transform.position;
		leftHand.transform.rotation = playerControllers.GetLeftControllerGameObject ().transform.rotation;
		rightHand.transform.position = playerControllers.GetRightControllerGameObject().transform.position;
		rightHand.transform.rotation = playerControllers.GetRightControllerGameObject ().transform.rotation;
	}
}
