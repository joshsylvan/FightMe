using UnityEngine;
using System.Collections;

public class ProjectileLauncher : MonoBehaviour {

	GameObject player, projectile;
	float coolDown, OGCooldown = 3f;

	// Use this for initialization
	void Start () {
//		player = GameObject.Find ("Camera (eye)");
		player = GameObject.Find ("Head");
		projectile = Resources.Load ("Prefab/Projectiles/Arrow") as GameObject;
		coolDown = OGCooldown;
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (player.transform.position);

		coolDown -= Time.deltaTime;


		if (coolDown < 0) {
			coolDown = OGCooldown;
			Debug.Log ("Fire the arrow!!!");
			GameObject arrow = Instantiate(projectile);
			arrow.transform.position = this.transform.position;
			arrow.transform.rotation = this.transform.rotation;
			arrow.GetComponent<Rigidbody> ().AddForce (arrow.transform.rotation * Vector3.forward * (160));
			arrow.GetComponent<Rigidbody> ().AddForce (arrow.transform.rotation * Vector3.up * (10));


		}




	}
}
