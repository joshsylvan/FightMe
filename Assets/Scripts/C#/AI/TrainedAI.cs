using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Trained AI class, handling navigation attacking an learning.
/// </summary>
public class TrainedAI : MonoBehaviour {
	// Gesture Loading when gestures are loaded from CSV files.
	GestureLoader gl;
	List<Gesture> Ugestures, gestures;
	GestureRecognizer gr;

	// Navigation Variabls
	public NavMeshAgent nma;
	GameObject player;
	GameObject nodes, closeNodes; // Nodes to navigate to.
	bool flee;
	// Animatino variables
	public bool stutter;
	public Animation sword;
	public TrainedAISword ai;


	public int behaviour = 0; // Behaviout model to use

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

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake(){
		nma = GetComponent<NavMeshAgent> ();
		player = GameObject.Find ("Head");
		nodes = GameObject.Find ("Nodes");
		closeNodes = GameObject.Find ("NodesCloser");
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		// Gesture Loader
		gl = GetComponent<GestureLoader>();
		Ugestures = gl.GetClassifiedGesturesM ();
		gr = new GestureRecognizer (); 
		gestures = gr.ClassifyGestures(Ugestures, new List<Gesture>(), 0.8f, 0.3f, 0.3f);
		Debug.Log ("Gestu  " + gestures.Count);
		ai.CreateAnimationClipsFromGestures (gestures);
		sword.Play (""+gestures.Count);
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
		int choice = behaviour % 2;
		switch(choice){
		case 0:
			DefensiveBehaviour ();
			break;
		case 1:
			DefensiveBehaviour ();
			break;
		default:
			DefensiveBehaviour ();
			break;
		}
		DefensiveBehaviour();
		OffensiveBehaviour();

	}

	/// <summary>
	/// Offensive behaviour model for the Trained AI.
	/// </summary>
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
				sword.Play (""+(gestures.Count));
				offensiveState = 0;
			} else if (!sword.isPlaying && attacks >= 1) {
				attacks--;
				sword.Play (Random.Range (0, gestures.Count-1)+"");
			}
			break;
		}
	}

	/// <summary>
	/// Defensives the behaviour mode for the Trained AI.
	/// </summary>
	void DefensiveBehaviour(){
//		transform.RotateAround (player.transform.position, Vector3.down, 20f * Time.deltaTime);
		switch (defensiveSate) {
		case 0:
			sword.gameObject.transform.position = Vector3.MoveTowards (sword.gameObject.transform.position, new Vector3 (0, 0, 0), 2f);
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

	/// <summary>
	/// Changes the behaviour.
	/// </summary>
	public void ChangeBehaviour(){
		behaviour++;
	}

	/// <summary>
	/// Predicts the next gesture.
	/// </summary>
	/// <returns>The next gesture.</returns>
	public int PredictNextGesture(){
		return 0;
	}
}
