using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class NaiveAI_Runner : MonoBehaviour {

	NavMeshAgent nma;
	GameObject player;
	GameObject nodes;
	float fleeDistance = 1f;
	bool flee;
	public Animator anim;

	// Use this for initialization
	void Start () {
		flee = false;
		nma = GetComponent<NavMeshAgent> ();
//		player = GameObject.Find ("Camera (eye)");
		player = GameObject.Find ("Player");
		nodes = GameObject.Find ("Nodes");
		anim = transform.GetChild (0).GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (player.transform);
		transform.rotation = Quaternion.Euler (new Vector3(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
		if (!flee) {
			nma.SetDestination (player.transform.position);
			if(nma.hasPath && nma.remainingDistance <= nma.stoppingDistance){
				//attack
				anim.SetTrigger("Slash");


				flee = true;
				int rNodeIndex = Random.Range (0, nodes.transform.childCount);
				nma.SetDestination (nodes.transform.GetChild(rNodeIndex).transform.position);
			}
		} else {
			if(nma.hasPath && nma.remainingDistance <= nma.stoppingDistance){
				flee = false;
				nma.SetDestination (player.transform.position);
			}
		}
	}
}
