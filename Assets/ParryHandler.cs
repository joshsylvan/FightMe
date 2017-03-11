using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryHandler : MonoBehaviour {

	public Animator anim;

	// Use this for initialization
	void Start () {
		if (GetComponent<Animator> () == null) {
			transform.parent.gameObject.GetComponent<Animator> ();
		} else {
			anim = GetComponent<Animator> ();
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "PlayerWeapon" || col.gameObject.tag == "PlayerShield") {
			//if (anim.GetCurrentAnimatorStateInfo (0).IsName ("anim_WarriorSlash")) {
			Debug.Log("Parry");
				anim.SetTrigger ("Parry");
			//}
		}
	}
}
