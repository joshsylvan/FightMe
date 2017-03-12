using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class NaiveAI_Runner : MonoBehaviour {

	NavMeshAgent nma;
	GameObject player;
	GameObject nodes;
	float fleeDistance = 1f;
	bool flee; 
	public bool stutter;
	public Animator anim;

	// Use this for initialization
	void Start () {
		flee = false;
		stutter = false;
		nma = GetComponent<NavMeshAgent> ();
//		player = GameObject.Find ("Camera (eye)");
		player = GameObject.Find ("Head");
		nodes = GameObject.Find ("Nodes");
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (player.transform);
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

	public void StartStutter(){
		stutter = true;
		nma.Stop ();
	}

	public void EndStutter(){
		stutter = false;
		nma.Resume ();
	}

	public bool GetStutter(){
		return stutter;
	}
}
