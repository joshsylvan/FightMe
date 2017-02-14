using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPositions : MonoBehaviour {

	PlayerControllers playerControllers;
	GameObject[] hands;

	// Use this for initialization
	void Start () {
		playerControllers = GetComponent<PlayerControllers> ();
		hands = new GameObject[2];
		hands [0] = transform.GetChild (0).GetChild(0).gameObject;
		hands [1] = transform.GetChild (0).GetChild(1).gameObject;
	}
	
	// Update is called once per frame
	void Update () {

		hands[0].transform.position = playerControllers.GetLeftControllerGameObject ().transform.position;
		hands[0].transform.rotation = playerControllers.GetLeftControllerGameObject ().transform.rotation;
		hands[1].transform.position = playerControllers.GetRightControllerGameObject ().transform.position;
		hands[1].transform.rotation = playerControllers.GetRightControllerGameObject ().transform.rotation;

	}
}
