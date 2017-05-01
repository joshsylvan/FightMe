using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainedAIShield : MonoBehaviour {

	public GameObject playerRightHand;
	bool follow = true;

	// Use this for initialization
	void Start () {
		playerRightHand = GameObject.Find ("RightHand");
	}

	// Update is called once per frame
	void Update () {
		if (follow) {
			this.transform.position = new Vector3 (
				transform.position.x,
				0.5f + playerRightHand.transform.position.y / 4,
				transform.position.z
			);
		}
	}

	public void StopFollow(){
		follow = false;
	}

	public void StartFollow(){
		follow = true;
	}
}
