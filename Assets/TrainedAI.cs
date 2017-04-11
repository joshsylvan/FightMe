using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrainedAI : MonoBehaviour {

	NavMeshAgent nma;
	GameObject player;
	GameObject nodes, closeNodes;
	bool flee;
	public bool stutter;
	public Animation sword;

	public int behaviour = 0;

	// Defensive
	bool defend = true;
	bool strafe = false;
	bool nearPlayer = false;

	float strafeTimer;
	int strafeDirection;

	int defensiveSate = 0;


	// Use this for initialization
	void Start () {
		flee = false;
		stutter = false;
		nma = GetComponent<NavMeshAgent> ();
		player = GameObject.Find ("Head");
		nodes = GameObject.Find ("Nodes");
		closeNodes = GameObject.Find ("NodesCloser");
//		nma.SetDestination (nodes.transform.GetChild (Random.Range (0, nodes.transform.childCount)).transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (player.transform);
		transform.rotation = Quaternion.Euler (new Vector3(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
		DefensiveBehaviour();


//		switch (behaviour) {
//		case 0: // Defensive
//			DefensiveBehaviour();
//			break;
//		case 1: // Offensive
//			
//			break;
//		case 2: // Quick
//			break;
//		case 3: // brute force
//			break;
//		default:
//			Debug.Log ("Not a valid Behaviour");
//			break;
//		}

	}

	void DefensiveBehaviour(){
//		transform.RotateAround (player.transform.position, Vector3.down, 20f * Time.deltaTime);
		switch (defensiveSate) {
		case 0:
			if (nma.remainingDistance <= nma.stoppingDistance) {
				nma.SetDestination (nodes.transform.GetChild (Random.Range (0, nodes.transform.childCount)).transform.position);
				defensiveSate = 1;
			}
			break;
		case 1:
			if (!strafe) {
				if (nma.remainingDistance <= nma.stoppingDistance) {
					nma.Stop ();
					strafe = true;
					strafeTimer = Random.Range (3, 10);
				}
			} else {
				strafeTimer -= Time.deltaTime;
				if (strafeTimer > 0) {
					transform.RotateAround (player.transform.position, Vector3.down, 20f * Time.deltaTime);
				} else {
					strafe = false;
					if (nearPlayer) {
						defensiveSate = 3;
						nearPlayer = false;
					} else {
						switch (Random.Range (0, 2)) {
						case 0:
						//Attack
							defensiveSate = 3;
							break;
						case 1:
							defensiveSate = 2;
							break;
						default:
							break;
						}
					}
				}
			}
			break;
		case 2:
			nma.SetDestination (closeNodes.transform.GetChild (Random.Range (0, closeNodes.transform.childCount)).transform.position);
			nma.Resume ();
			nearPlayer = true;
			defensiveSate = 1;
			break;
		case 3:
			nma.stoppingDistance = 1.5f;
			nma.SetDestination (player.transform.position);
			nma.Resume ();
			break;
		default:
			break;
		}
//			nma.SetDestination (nodes.transform.GetChild (Random.Range (0, nodes.transform.childCount)).transform.position);
//		if (defend) {
//			if (running) {
//				if (nma.remainingDistance <= nma.stoppingDistance) {
//					strafe = true;
//					strafeTimer = Random.Range (3, 10);
//					strafeDirection = Random.Range (0, 2);
//					running = false;
//					Debug.Log ("asdasd");
//				}
//			}
//			if (strafe) {
//				switch (strafeDirection) {
//				case 0:
//					transform.RotateAround (player.transform.position, Vector3.up, 20f * Time.deltaTime);
//					break;
//				case 1:
//					transform.RotateAround (player.transform.position, Vector3.down, 20f * Time.deltaTime);
//					break;
//				}
//			}
//		}
	}

	void OffensiveBehaviour(){
	
	}
}
