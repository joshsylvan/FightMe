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
	List<Gesture> Ugestures, gestures = new List<Gesture>();
	GestureRecognizer gr;
	bool setUp = true;

	// Combo Prediction Variables
	GameObject dataRecorder;
	ComboPredictor cp;
	GestureRecognizer recognizer;

	// Navigation Variabls
	public NavMeshAgent nma;
	GameObject player;
	GameObject nodes, closeNodes; // Nodes to navigate to.
	public GameObject shield;
	bool flee;
	// Animation variables
	public bool stutter;
	public Animation sword;
	public TrainedAISword aiSword;

	// Defensive Behaviour
	bool defend = true;
	bool strafe = false;
	bool nearPlayer = false;
	float strafeTimer;
	int strafeDirection;
	int defensiveSate = 0;

	// Offensive Behaviour
	int offensiveState = 0;
	bool offensiveStrafe = false;
	int attacks = 0;

	// Parry Variables
	float OGParryCounter = 1f;
	float parryCounter = 1f;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake(){
		nma = GetComponent<NavMeshAgent> ();
		dataRecorder = GameObject.Find ("DataRecorder");
		player = GameObject.Find ("Head");
		nodes = GameObject.Find ("Nodes");
		closeNodes = GameObject.Find ("NodesCloser");
		recognizer = new GestureRecognizer ();
	}

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		// Gesture Loader
		//gl = GetComponent<GestureLoader>();
		//Ugestures = gl.GetClassifiedGesturesM ();
		//gr = new GestureRecognizer (); 
		//gestures = gr.ClassifyGestures(Ugestures, new List<Gesture>(), 0.8f, 0.3f, 0.3f);
		//Debug.Log ("Gestu  " + gestures.Count);
		//aiSword.CreateAnimationClipsFromGestures (gestures);
		//ai.CylcleAnimations()
		// AI movement
		flee = false;
		stutter = false;
//		nma.SetDestination (nodes.transform.GetChild (Random.Range (0, nodes.transform.childCount)).transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		int res = -1;
		if (dataRecorder.GetComponent<GestureRecorder> ().GetUnclassifiedGesturesRight ().Count > 1) {
			res = recognizer.NaiveRecognizer (
				dataRecorder.GetComponent<GestureRecorder> ().GetUnclassifiedGesturesRight () [
					dataRecorder.GetComponent<GestureRecorder> ().GetUnclassifiedGesturesRight ().Count - 1].GetPositionList ().ToArray (),
				aiSword.GetGestures (),
				0.8f,
				0.3f,
				0.3f
			);
			if (res != -1) {
				Debug.Log ((  PredictNextGesture( int.Parse(aiSword.GetGestures () [res].GetName())) ));
			} else {
				Debug.Log (-1);
			}
		}
		if (setUp) {
			transform.LookAt (player.transform);
			transform.rotation = Quaternion.Euler (new Vector3 (0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));

			if (res == -1) {
				shield.GetComponent<TrainedAIShield> ().StartFollow ();
			} else {
				shield.GetComponent<TrainedAIShield> ().StopFollow ();
				shield.transform.localPosition = new Vector3 (
					shield.transform.localPosition.x, 
					aiSword.GetGestures () [res].GetMatrixArray()[9].GetPosition().y, 
					shield.transform.localPosition.z
				);
			}

			if (!aiSword.IsParry ()) {
				if (GetComponent<AICombatManager> ().health <= 3) {
					OffensiveBehaviour ();
				} else {
					DefensiveBehaviour ();
				}
			} else {
				// Play Parry
				sword.Stop ();
				shield.GetComponent<TrainedAIShield> ().StopFollow ();
				sword.gameObject.transform.localPosition = new Vector3 (0.3f, -0.16f, -0.11f);
				sword.gameObject.transform.localRotation = new Quaternion (-0.5f, 0.44f, -0.04f, 0.73f);
				shield.transform.localPosition = new Vector3 (-0.594f, 0.815f, -0.073f);
				shield.transform.localRotation = new Quaternion (0.44f, -0.46f, 0.34f, 0.67f);
				parryCounter -= Time.deltaTime;
				if (parryCounter <= 0) {
					parryCounter = OGParryCounter;
					shield.GetComponent<TrainedAIShield> ().StartFollow ();
					shield.transform.localPosition = new Vector3 (-0.25f, -0.847f, 0.205f);
					shield.transform.localRotation = new Quaternion (0.64f, -1.2f, 0.14f, -0.744f);
					aiSword.EndParry ();
				}
			}
		}

	}

	/// <summary>
	/// Offensive behaviour model for the Trained AI.
	/// </summary>
	void OffensiveBehaviour(){
		int rrr = Random.Range (0, sword.GetClipCount ()-1);
		switch (offensiveState) {
		case 0:
			sword.Play (""+(sword.GetClipCount()-1));
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
				sword.Play (""+(sword.GetClipCount()-1));
				offensiveState = 0;
			} else if (!sword.isPlaying && attacks >= 1) {
				attacks--;
				sword.Play (""+(rrr));
			}
			break;
		}
	}

	/// <summary>
	/// Defensives the behaviour mode for the Trained AI.
	/// </summary>
	void DefensiveBehaviour(){
		int rrr = Random.Range (0, sword.GetClipCount ()-1);
//		transform.RotateAround (player.transform.position, Vector3.down, 20f * Time.deltaTime);
		switch (defensiveSate) {
		case 0:
			sword.Play (""+(sword.GetClipCount()-1));
			sword.gameObject.transform.position = Vector3.MoveTowards (sword.gameObject.transform.position, new Vector3 (0, 0, 0), 2f);
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
						nma.SetDestination (player.transform.position);
						nma.Resume ();
					} else {
						switch (Random.Range (0, 2)) {
						case 0:
						//Attack
							defensiveSate = 3;
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
				sword.Play (rrr+"");
				defensiveSate = 4;
			}
			break;
		case 4:
			if (!sword.isPlaying) {
				flee = false;
				stutter = false;
				nearPlayer = false;
				sword.Play ( sword.GetClipCount()-1+"");
				defensiveSate = 0;
			}
			break;
		default:
			break;
		}
	}

	public void BuildComboPredictions(List<Combo> combos){
		foreach (Combo c in combos) {
			string combo = "";
			foreach (int i in c.GetCombo()) {
				combo += i + "";
			}
			Debug.Log (combo);
		}
		cp = new ComboPredictor (combos);
		cp.CreateTreesFromCombos ();
	}

	/// <summary>
	/// Predicts the next gesture.
	/// </summary>
	/// <returns>The next gesture.</returns>
	public int PredictNextGesture(int i){
		return cp.PredictSequence(new List<int>(){i});
	}

	public TrainedAISword GetSword(){
		return aiSword;
	}

	public void SetUpComplete(){
		this.setUp = true;
	}
}
