using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wave manager is used to send out waves of enemies to the players.
/// </summary>
public class WaveManager : MonoBehaviour {

	GameObject enemies;
	GameObject runner, warrior;
	GameObject trainedAI;
	public GameObject[] spawnPoints;
	GestureRecorder gr;

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
		gr = GameObject.Find ("DataRecorder").GetComponent<GestureRecorder>();
		runner = Resources.Load ("Prefab/NPC/Runner2") as GameObject;
		warrior = Resources.Load ("Prefab/NPC/NaiveWarrior") as GameObject;
		trainedAI = Resources.Load ("Prefab/NPC/TrainedAI") as GameObject;
		runnersToSpawn = 1;
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
				waveState = 3;
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
			SpawnTrainedAI ();
			waveState++;
			break;
		case 4:
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

	void SpawnTrainedAI(){
		gr.ClassifyGestures ();
		if (gr.GetClassifiedGesturesRight ().Count > 1) {
			Debug.Log (gr.GetClassifiedGesturesRight ().Count);
			trainedAI = Instantiate (trainedAI);
			trainedAI.transform.position = spawnPoints [Random.Range (0, 4)].transform.position;
			trainedAI.transform.SetParent (enemies.transform);
			trainedAI.GetComponent<TrainedAI> ().GetSword ().CreateAnimationClipsFromGestures (gr.GetClassifiedGesturesRight ());
			trainedAI.GetComponent<TrainedAI> ().BuildComboPredictions (gr.GetComboRecorder().GetCombosRight());
			trainedAI.GetComponent<TrainedAI> ().SetUpComplete ();

		} else {
			//start waves again until more gestures are found
		}
	}
}
