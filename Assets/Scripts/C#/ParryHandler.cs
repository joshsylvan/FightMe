using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryHandler : MonoBehaviour {

	public Animator anim;
	public NaiveAI_Warrior ai;
	public NaiveAI_Runner aiR;

	// Cooldown for stutter
	float ogStutterCooldown = 0.5f;
	float stutterCooldown = 0.5f;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (ai != null) {  //warrior
			if (ai.GetStutter ()) {
				stutterCooldown -= Time.deltaTime;
				if (stutterCooldown <= 0) {
					ai.EndStutter ();
					stutterCooldown = ogStutterCooldown;
				}
			}
		} else {   //runner
			if (anim != null) {
				if (aiR.GetStutter ()) {
					stutterCooldown -= Time.deltaTime;
					if (stutterCooldown <= 0) {
						aiR.EndStutter ();
						stutterCooldown = ogStutterCooldown;
					}
				}
			}
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "PlayerWeapon" || col.gameObject.tag == "PlayerShield") {
			//if (anim.GetCurrentAnimatorStateInfo (0).IsName ("anim_WarriorSlash")) {
			if (anim != null) {
				Debug.Log ("Parry");
				anim.SetTrigger ("Parry");
				if (ai != null) {
					ai.StartStutter ();
				} else {
					aiR.StartStutter ();
				}
			}
			//}
		}
	}
}
