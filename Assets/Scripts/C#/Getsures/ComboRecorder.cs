using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboRecorder : MonoBehaviour{

	List<Combo> comboRightHand, comboLeftHand;
	Combo curComboLeft, curComboRight;

	int minGestureInCombo = 3;
	float ogGestureGap = 1.5f, gestureGapLeft, gestureGapRight;
	bool inComboLeft = false, inComboRight = false;

	int comboLeftID = 0, comboRightId = 0;

	void Start(){
		comboLeftHand = new List<Combo> ();
		comboRightHand = new List<Combo> ();
		curComboLeft = new Combo (comboLeftID);
		curComboRight = new Combo (comboRightId);
		gestureGapLeft = ogGestureGap;
		gestureGapRight = ogGestureGap;
	}

	void Update(){

		if (inComboLeft) {
			if (gestureGapLeft <= 0) {
				Debug.Log ("ComboLeft Ended");
				inComboLeft = false;
				SaveCombo (0, curComboLeft);
			}
			gestureGapLeft -= Time.deltaTime;
		}

		if (inComboRight) {
			if (gestureGapRight <= 0) {
				Debug.Log ("ComboRight Ended");
				inComboRight = false;
				SaveCombo (1, curComboRight);
			}
			gestureGapRight -= Time.deltaTime;
		}

		if (Input.GetKeyUp (KeyCode.C)) {
			FileOutput f = new FileOutput ();
			f.ComboOutput (comboLeftHand, "leftHandCombos");
			f.ComboOutput (comboRightHand, "rightHandCombos");
		}


	}

	void SaveCombo(int LR, Combo curCombo){
		if (curCombo.GetComboLength() >= minGestureInCombo) {

			if (LR == 0) {
				comboLeftHand.Add (curCombo);
				string c = "";
				foreach (int i in curCombo.GetCombo()) {
					c += i + ", ";
				}
				Debug.Log (curCombo.GetID () + ":" + c);
				curComboLeft = new Combo (++comboLeftID);
			} else if (LR == 1) {
				comboRightHand.Add (curCombo);
				string c = "";
				foreach (int i in curCombo.GetCombo()) {
					c += i + ", ";
				}
				Debug.Log (curCombo.GetID () + ":" + c);
				curComboRight = new Combo (++comboRightId);
			}
		
		} else {
			Debug.Log ("Not enough gestures for a combo! DELETE");
			if (LR == 0) {
				curComboLeft = new Combo (comboLeftID);
			} else if (LR == 1) {
				curComboRight = new Combo (comboRightId);
			}
		}
	}

	public void AddGestureToCurrentComboLeft(int ID){
		curComboLeft.AddToCombo (ID);
		inComboLeft = true;
		gestureGapLeft = ogGestureGap;
	}

	public void AddGestureToCurrentComboRight(int ID){
		curComboRight.AddToCombo (ID);
		inComboRight = true;
		gestureGapRight = ogGestureGap;
	}

	public void UpdateComboIDsLeft(Pair[] pairs)
	{
		foreach (Pair p in pairs)
		{
			for (int i = 0; i < comboLeftHand.Count; i++)
			{
				for (int j = 0; j < comboLeftHand[i].GetCombo().Count; j++)
				{
					if (p.GetHead() == comboLeftHand[i].GetCombo()[j])
					{
						comboLeftHand[i].GetCombo()[j] = p.GetTail();
					}
				}
			}
		}
	}

	public void UpdateComboIDsRight(Pair[] pairs)
	{
		foreach (Pair p in pairs)
		{
			for (int i = 0; i < comboRightHand.Count; i++)
			{
				for (int j = 0; j < comboRightHand[i].GetCombo().Count; j++)
				{
					if (p.GetHead() == comboRightHand[i].GetCombo()[j])
					{
						comboRightHand[i].GetCombo()[j] = p.GetTail();
					}
				}
			}
		}
	}

}
