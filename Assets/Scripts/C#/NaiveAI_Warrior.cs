using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class NaiveAI_Warrior : MonoBehaviour {

	NavMeshAgent nma;
	GameObject player;
	GameObject nodes;
	bool walkback, strafe, attack, setUp;
	float strafeTimer;
	int strafeDirection;
	int rNodeIndex;
	Animator anim;

	// Use this for initialization
	void Start () {
		walkback = false;
		strafe = false;
//		player = GameObject.Find ("Camera (eye)");
		player = GameObject.Find ("Player");
		nodes = GameObject.Find ("NodesCloser");
		nma = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (player.transform);
		transform.rotation = Quaternion.Euler (new Vector3(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
		if (!strafe) {
			nma.SetDestination (player.transform.position);
			if (nma.remainingDistance <= nma.stoppingDistance) {
				strafe = true;
			}
		} else {
			if (!setUp) {
				strafeTimer = Random.Range (3, 10);
				strafeDirection = Random.Range (0, 2);
				setUp = true;
			}
			if (strafeTimer >= 0) {
				strafeTimer -= Time.deltaTime;
				switch(strafeDirection){
				case 0:
					transform.RotateAround (player.transform.position, Vector3.up, 20f * Time.deltaTime);
					break;
				case 1:
					transform.RotateAround (player.transform.position, Vector3.down, 20f * Time.deltaTime);
					break;
				}
				rNodeIndex = ClosestNode();

			} else {
				if (!attack) {
					nma.stoppingDistance = 1.5f;
					nma.SetDestination (player.transform.position);
					nma.Resume ();
					if (nma.remainingDistance <= nma.stoppingDistance) {
						attack = true;
						rNodeIndex = ClosestNode ();
						anim.SetTrigger("Slash");
					}
				} else {
					//wait for attack animation to finish
					walkback = true;
				}
				if (walkback) {
					nma.stoppingDistance = 0.3f;
					nma.speed = 1;
					nma.SetDestination (  nodes.transform.GetChild(rNodeIndex).transform.position  );
					if (nma.remainingDistance <= nma.stoppingDistance) {
						nma.stoppingDistance = 1.5f;
						nma.speed = 2.5f;
						walkback = false;
						attack = false;
						strafeTimer = Random.Range (3, 10);
						strafeDirection = Random.Range (0, 2);
						nma.Stop ();
					}
				}
			}
		}


	}

	int ClosestNode(){
		int currentIndex = 0;
		float currentDistsance = Vector3.Distance(transform.position, nodes.transform.GetChild(currentIndex).position);

		for (int i = 1; i < nodes.transform.childCount; i++) {
			if (Vector3.Distance (transform.position, nodes.transform.GetChild (i).position) <= currentDistsance ) {
				currentIndex = i;
				currentDistsance = Vector3.Distance (transform.position, nodes.transform.GetChild (i).position);
			}
		}
		return currentIndex;
	}


}
