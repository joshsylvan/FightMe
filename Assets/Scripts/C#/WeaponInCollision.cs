using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInCollision : MonoBehaviour {

	bool inContact = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col){
		inContact = true;
	}

	void OnCollisionStay(Collision col){
		inContact = true;
	}

	void OnCollisionExit(Collision col){
		inContact = false;
	}

	public bool isInContact(){
		return inContact;
	}
}
