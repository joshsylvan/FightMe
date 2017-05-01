using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// AI combat manager. Used to track the damage the player does to AI
/// </summary>
public class AICombatManager : MonoBehaviour {

	public int health = 3; // Default health value
	bool enableRagdoll = false; // Enables the body to go into ragdoll mode.
	public bool dropWeapons = false; // AI drops their weaopns

	// Update is called once per frame. Drops weapons when required and deletes componets from AI when dead.
	void Update () {
		if (health <= 0) {
			//weapons
			if (dropWeapons) {
				transform.SetParent (GameObject.Find("DeadAI").transform);
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
				} else if (GetComponent<TrainedAI> () != null){
					//sword
					Destroy (transform.GetChild (0).gameObject.transform.GetChild (0).GetComponent<Animation> ());
					Destroy (transform.GetChild (0).gameObject.transform.GetChild (0).GetComponent<TrainedAISword> ());
					transform.GetChild (0).gameObject.transform.GetChild (0).GetComponent<Rigidbody> ().isKinematic = false;
					transform.GetChild (0).SetParent (null);

					//shield
					Destroy (transform.GetChild (2).GetComponent<TrainedAIShield> ());
					transform.GetChild (2).GetComponent<Rigidbody> ().isKinematic = false;
					transform.GetChild (2).SetParent (null);

					//body
					GetComponent<Collider>().isTrigger = false;
					GetComponent<Rigidbody>().useGravity = true;
					Destroy (GetComponent<TrainedAI> ());
					Destroy (GetComponent<Animator> ());
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

	/// <summary>
	/// Raises the trigger enter event. If players weapon enters the body deal damage.
	/// </summary>
	/// <param name="col">Col.</param>
	void OnTriggerEnter(Collider col){
		if (col.tag == "PlayerWeapon") {
			health--;
			if (health <= 0) {
				dropWeapons = true;
			}
		}
	}

	/// <summary>
	/// Raises the trigger exit event. When any object leavse the trigger area and AI's health is below zero enable ragdoll mode.
	/// </summary>
	void OnTriggerExit(){
		if (health <= 0) {
			enableRagdoll = true;
		}
	}

}
