using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboRecorder : MonoBehaviour{

	List<Combo> combos;
	List<Gesture> curCombo;

	int minGestureInCombo = 3, comboIndex = 0;
	float ogGestureGap = 1.5f, gestureGap;
	bool inCombo = false;


	void Start(){
		combos = new List<Combo> ();
		curCombo = new List<Gesture> ();
		gestureGap = ogGestureGap;
	}

	void Update(){

		if (inCombo) {
			if (gestureGap <= 0) {
				inCombo = false;
				SaveCombo ();
			}
			gestureGap -= Time.deltaTime;
		}


	}

	public void AddGestureToCurrentCombo(Gesture gesture){
		curCombo.Add (gesture);
		inCombo = true;
		gestureGap = ogGestureGap;
	}

	void ResetCurrentCombo(){
		curCombo = new List<Gesture> ();
	}

	void SaveCombo(){
		if (curCombo.Count >= minGestureInCombo) {
			combos.Add (new Combo ("" + comboIndex++, curCombo));
			ResetCurrentCombo ();
		} else {
			Debug.Log ("Not enough gestures for a combo!");
		}
	}

	public List<Combo> GetCombos(){ 
		return combos;
	}

	public void SetCombos(List<Combo> combos){
		this.combos = combos;
	}

}
