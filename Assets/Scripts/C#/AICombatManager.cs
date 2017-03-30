using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AICombatManager : MonoBehaviour {

	public int health = 3;
	bool enableRagdoll = false;
	bool dropWeapons = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			//weapons
			if (dropWeapons) {
				if (GetComponent<NaiveAI_Warrior> () != null) {
					transform.GetChild (0).gameObject.GetComponent<Rigidbody> ().isKinematic = false;
					transform.GetChild (0).transform.SetParent (null);
					transform.GetChild (0).gameObject.GetComponent<Rigidbody> ().isKinematic = false;
					transform.GetChild (0).transform.SetParent (null);
					Destroy (GetComponent<NaiveAI_Warrior> ());
					Destroy (GetComponent<Animator> ());
					Destroy (GetComponent<NavMeshAgent> ());
				} else if (GetComponent<NaiveAI_Runner> () != null){
					Destroy (transform.gameObject.GetComponent<Animator>());
					transform.GetChild (0).gameObject.GetComponent<Rigidbody> ().isKinematic = false;
					transform.GetChild (0).transform.SetParent (null);
					Destroy (GetComponent<NaiveAI_Runner> ());
					Destroy (GetComponent<NavMeshAgent> ());
				}

				dropWeapons = false;
			}
			if (enableRagdoll) {
				//body
				GetComponent<CapsuleCollider> ().isTrigger = false;
				GetComponent<Rigidbody> ().useGravity = true;
				GetComponent<Rigidbody> ().AddForce (Vector3.back * 2);
				Destroy (this);
			}
		}
	}


	void OnTriggerEnter(Collider col){
		if (col.tag == "PlayerWeapon") {
			health--;
			if (health <= 0) {
				dropWeapons = true;
			}
		}
	}

	void OnTriggerExit(){
		if (health <= 0) {
			enableRagdoll = true;
		}
	}

}
