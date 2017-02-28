using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class WeaponAttach : MonoBehaviour {

	public GameObject weapon;
	public Rigidbody attachPoint;

	SteamVR_TrackedObject trackedObject;
	FixedJoint joint;

	GameObject go;

	void Awake(){
		this.trackedObject = GetComponent<SteamVR_TrackedObject> ();
		go = GameObject.Instantiate(weapon);
		go.transform.position = attachPoint.transform.position;
		joint = go.AddComponent<FixedJoint>();
		joint.connectedBody = attachPoint;
	}


	void FixedUpdate(){
		
	}

	void Update(){
		go.transform.position = attachPoint.transform.position;
	}

}
