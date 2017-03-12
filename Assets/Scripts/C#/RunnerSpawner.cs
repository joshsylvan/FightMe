using UnityEngine;
using System.Collections;

public class RunnerSpawner : MonoBehaviour {

	GameObject runner;
	float origionalCoolDown = 5;
	float coolDown = 5;
	public GameObject enemies;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		// spawn a runner every 10 seconds

		coolDown -= Time.deltaTime;

		if (coolDown <= 0) {
			runner = Resources.Load ("Prefab/NPC/Runner2") as GameObject;
			runner = Instantiate (runner);
			runner.transform.SetParent (enemies.transform);
			runner.transform.position = this.transform.position;
			coolDown = origionalCoolDown;
		}

		if (Input.GetKeyUp (KeyCode.W)) {
			runner = Resources.Load ("Prefab/NPC/Runner2") as GameObject;
			runner = Instantiate (runner);
			runner.transform.SetParent (enemies.transform);
			runner.transform.position = this.transform.position;
			coolDown = origionalCoolDown;
		}
		if (Input.GetKeyUp (KeyCode.S)) {
			runner = Resources.Load ("Prefab/NPC/NaiveWarrior") as GameObject;
			runner = Instantiate (runner);
			runner.transform.SetParent (enemies.transform);
			runner.transform.position = this.transform.position;
			coolDown = origionalCoolDown;
		}
		if (Input.GetKeyUp (KeyCode.R)) {
			for (int i = 0; i < enemies.transform.childCount; i++) {
				Destroy (enemies.transform.GetChild (i).gameObject);
			}
		}
//		}

	}
}
