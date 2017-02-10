using UnityEngine;
using System.Collections;

public class RunnerSpawner : MonoBehaviour {

	GameObject runner;
	float origionalCoolDown = 10;
	float coolDown = 0;
	public GameObject enemies;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		// spawn a runner every 10 seconds

		coolDown -= Time.deltaTime;

		if(coolDown <= 0){
			runner = Resources.Load ("Prefab/NPC/Runner") as GameObject;
			runner = Instantiate (runner);
			runner.transform.SetParent (enemies.transform);
			runner.transform.position = this.transform.position;
			coolDown = origionalCoolDown;
		}

	}
}
