using UnityEngine;
using System.Collections;
using UnityEngine.AI;

/// <summary>
/// Naive AI Runner class handle the Runners behaviours and attacks.
/// </summary>
public class NaiveAI_Runner : MonoBehaviour {

	NavMeshAgent nma;
	GameObject player; // Players game object.
	GameObject nodes;
	float fleeDistance = 1f;
	bool flee; 
	public bool stutter;
	public Animator anim;

	// Used for initialization
	void Start () {
		flee = false;
		stutter = false;
		nma = GetComponent<NavMeshAgent> ();
//		player = GameObject.Find ("Camera (eye)"); Old pre Newton VR method.
		player = GameObject.Find ("Head");
		nodes = GameObject.Find ("Nodes");
	}
	
	// Update is called once per frame. Controlls the AI's behaviours.
	void Update () {
		transform.LookAt (player.transform); // Look at the player
		transform.rotation = Quaternion.Euler (new Vector3(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
		if (!stutter) {
			if (!flee) {
				nma.SetDestination (player.transform.position);
				if (nma.hasPath && nma.remainingDistance <= nma.stoppingDistance) {
					//attack
					switch ((int)Random.Range (1, 3)) {
					case 1:
						anim.SetTrigger ("Slash");
						break;
					case 2:
						anim.SetTrigger ("Stab");
						break;
					default:
						Debug.Log ("Not a valid attack!");
						break;
					}
					flee = true;
					int rNodeIndex = Random.Range (0, nodes.transform.childCount);
					nma.SetDestination (nodes.transform.GetChild (rNodeIndex).transform.position);
				}
			} else {
				if (nma.hasPath && nma.remainingDistance <= nma.stoppingDistance) {
					flee = false;
					nma.SetDestination (player.transform.position);
				}
			}
		}
	}

	/// <summary>
	/// Starts the stutter after a parry.
	/// </summary>
	public void StartStutter(){
		stutter = true;
		nma.Stop ();
	}

	/// <summary>
	/// Ends the stutter after a parry.
	/// </summary>
	public void EndStutter(){
		stutter = false;
		nma.Resume ();
	}

	/// <summary>
	/// Gets the stutter.
	/// </summary>
	/// <returns><c>true</c>, If the player is in the process of being parried <c>false</c> Not being parried.</returns>
	public bool GetStutter(){
		return stutter;
	}
}
