using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrainedAI : MonoBehaviour {
	//
	GestureLoader gl;
	List<Gesture> gestures;
	//
	public NavMeshAgent nma;
	GameObject player;
	GameObject nodes, closeNodes;
	bool flee;
	public bool stutter;
	public Animation sword;
	public TrainedAISword ai;

	public int behaviour = 0;

	// Defensive
	bool defend = true;
	bool strafe = false;
	bool nearPlayer = false;
	float strafeTimer;
	int strafeDirection;
	int defensiveSate = 0;
	// Offensive
	int offensiveState = 0;
	bool offensiveStrafe = false;
	int attacks = 0;

	void Awake(){
		nma = GetComponent<NavMeshAgent> ();
		player = GameObject.Find ("Head");
		nodes = GameObject.Find ("Nodes");
		closeNodes = GameObject.Find ("NodesCloser");
	}

	// Use this for initialization
	void Start () {
		// Gesture Loader
		gl = GetComponent<GestureLoader>();
		gestures = gl.GetClassifiedGesturesM ();
		ai.CreateAnimationClipsFromGestures (gestures);
//		ai.CylcleAnimations()

		// AI movement
		flee = false;
		stutter = false;
//		nma.SetDestination (nodes.transform.GetChild (Random.Range (0, nodes.transform.childCount)).transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (player.transform);
		transform.rotation = Quaternion.Euler (new Vector3(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
//		DefensiveBehaviour();
		OffensiveBehaviour();
//		Debug.Log (defensiveSate);

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

	void OffensiveBehaviour(){
		switch (offensiveState) {
		case 0:
			nma.SetDestination (closeNodes.transform.GetChild (Random.Range (0, closeNodes.transform.childCount)).transform.position);
			offensiveState = 1;
			break;
		case 1:
			if (!offensiveStrafe) {
				if (nma.remainingDistance <= nma.stoppingDistance) {
					nma.Stop ();
					offensiveStrafe = true;
					strafeTimer = Random.Range (3, 10);
				}
			} else {
				strafeTimer -= Time.deltaTime;
				if (strafeTimer > 0) {
					transform.RotateAround (player.transform.position, Vector3.down, 20f * Time.deltaTime);
				} else {
					offensiveState = 2;
					offensiveStrafe = false;
					nma.stoppingDistance = 1.5f;
					nma.SetDestination (player.transform.position);
					nma.Resume ();
				}
			}
			break;
		case 2:
			if (nma.remainingDistance <= nma.stoppingDistance) {
				attacks = Random.Range (0, 4);
				offensiveState = 3;
			}
			break;
		case 3:
			if (!sword.isPlaying && attacks <= 0) {
				offensiveState = 0;
			} else if (!sword.isPlaying && attacks >= 1) {
				attacks--;
				sword.Play (Random.Range (0, gestures.Count)+"");
			}
			break;
		}
	}

	void DefensiveBehaviour(){
//		transform.RotateAround (player.transform.position, Vector3.down, 20f * Time.deltaTime);
		switch (defensiveSate) {
		case 0:
			if (nma.remainingDistance <= nma.stoppingDistance) {
				nma.SetDestination (nodes.transform.GetChild (Random.Range (0, nodes.transform.childCount)).transform.position);
				defensiveSate = 1;
				nma.stoppingDistance = 0f;
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
						nma.stoppingDistance = 1.5f;
						nma.SetDestination (player.transform.position);
						nma.Resume ();
					} else {
						switch (Random.Range (0, 2)) {
						case 0:
						//Attack
							defensiveSate = 3;
							nma.stoppingDistance = 1.5f;
							nma.SetDestination (player.transform.position);
							nma.Resume ();
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
			if (nma.remainingDistance <= nma.stoppingDistance) {
				sword.Play (Random.Range (0, gestures.Count)+"");
				defensiveSate = 4;
			}
			break;
		case 4:
			if (!sword.isPlaying) {
				flee = false;
				stutter = false;
				nearPlayer = false;
				defensiveSate = 0;
			}
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
}
