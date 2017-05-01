using UnityEngine;
using System.Collections;


/// <summary>
/// Used to spawn Warriors into the game world.
/// </summary>
public class WarriorSpawner : MonoBehaviour {

	GameObject warrior; // objects to load
	float origionalCoolDown = 10; // cooldown for continuous spawning
	float coolDown = 10;
	public GameObject enemies;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	

		coolDown -= Time.deltaTime;

		if (coolDown <= 0) {
			warrior = Resources.Load ("Prefab/NPC/NaiveWarrior") as GameObject;
			warrior = Instantiate (warrior);
			warrior.transform.SetParent (enemies.transform);
			warrior.transform.position = this.transform.position;
			coolDown = origionalCoolDown;
		}

		if (Input.GetKeyUp (KeyCode.S)) {
			warrior = Resources.Load ("Prefab/NPC/NaiveWarrior") as GameObject;
			warrior = Instantiate (warrior);
			warrior.transform.SetParent (enemies.transform);
			warrior.transform.position = this.transform.position;
			coolDown = origionalCoolDown;
		}


	}

	public void SpawnWarrior(){
		warrior = Resources.Load ("Prefab/NPC/NaiveWarrior") as GameObject;
		warrior = Instantiate (warrior);
		warrior.transform.SetParent (enemies.transform);
		warrior.transform.position = this.transform.position;
		coolDown = origionalCoolDown;
	}
}
