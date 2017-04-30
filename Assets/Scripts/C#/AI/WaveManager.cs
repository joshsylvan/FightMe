using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wave manager is used to send out waves of enemies to the players.
/// </summary>
public class WaveManager : MonoBehaviour {

	GameObject enemies;
	GameObject runner, warrior;
	public GameObject[] spawnPoints;

	public int waveState = 0;

	int runnersToSpawn = 0;
	int warriorsToSpawn = 0;
	//
	float waveCounter;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		enemies = GameObject.Find ("Enemies");
		runner = Resources.Load ("Prefab/NPC/Runner2") as GameObject;
		warrior = Resources.Load ("Prefab/NPC/NaiveWarrior") as GameObject;
		runnersToSpawn = 5;
	}
	
	// Update is called once per frame and spawns the specific enemies when needed depending on wave state.
	void Update () {
		switch (waveState) {
		case 0:
			waveCounter += Time.deltaTime;
			if (waveCounter >= 7) {
				waveCounter = 0;
				SpawnRunner ();
			}
			if (runnersToSpawn <= 0 && enemies.transform.childCount <= 0) {
				waveState++;
				runnersToSpawn = 5;
				warriorsToSpawn = 2;
				waveCounter = 0;
			}
			break;
		case 1:
			waveCounter += Time.deltaTime;
			if (runnersToSpawn > 0) {
				if (waveCounter >= 6) {
					waveCounter = 0;
					SpawnRunner ();
				}
			} else {
				if (warriorsToSpawn > 0 && enemies.transform.childCount <= 0) {
					SpawnWarrior ();
				} else if(enemies.transform.childCount <= 0){
					waveState++;
					runnersToSpawn = 0;
					warriorsToSpawn = 4;
					waveCounter = 0;
				}
			}
			break;
		case 2:
			if (warriorsToSpawn > 0 && enemies.transform.childCount <= 0) {
				SpawnWarrior ();
			} else if (enemies.transform.childCount <= 0) {
				waveState++;
			}
			break;
		case 3:
			// Do learning Build an AI and then apply to a shell.
			break;
		}
	}


	/// <summary>
	/// Spawns the Runner into the envirnoment..
	/// </summary>
	void SpawnRunner(){
		if (runnersToSpawn > 0) {
			runnersToSpawn--;
			GameObject tRunner = runner;
			tRunner.transform.position = spawnPoints [Random.Range (0, 4)].transform.position;
			tRunner = Instantiate (tRunner);
			tRunner.transform.SetParent (enemies.transform);
		}
	}

	/// <summary>
	/// Spawns a warrior into the environment.
	/// </summary>
	void SpawnWarrior(){
		if (warriorsToSpawn > 0) {
			warriorsToSpawn--;
			GameObject tWarrior = warrior;
			tWarrior.transform.position = spawnPoints [Random.Range (0, 4)].transform.position;
			tWarrior = Instantiate (tWarrior);
			tWarrior.transform.SetParent (enemies.transform);
		}
	}
}
