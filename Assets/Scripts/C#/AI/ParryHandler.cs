using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryHandler : MonoBehaviour {

	public Animator anim;
	public NaiveAI_Warrior ai;
	public NaiveAI_Runner aiR;

	// Cooldown for stutter
	float ogStutterCooldown = 0.5f; // Parry Cooldown
	float stutterCooldown = 0.5f; // current parry cooldown.


	// Use this for initialization
	void Start () {

	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
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

	/// <summary>
	/// Detects when the AI's weapon collides with the players shield or weapon. If so then player the parry animation and stand still.
	/// </summary>
	/// <param name="col">Col.</param>
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
