using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Combo recorder. Used to record combos.
/// </summary>
public class ComboRecorder : MonoBehaviour{

	List<Combo> comboRightHand, comboLeftHand; // Combos for each hand
	Combo curComboLeft, curComboRight; // current combo for each hand

	int minGestureInCombo = 3; // Minimum length of a combo
	float ogGestureGap = 1.5f, gestureGapLeft, gestureGapRight;// Combo time intervals
	bool inComboLeft = false, inComboRight = false;

	int comboLeftID = 0, comboRightId = 0; // Combo ID counters

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start(){
		comboLeftHand = new List<Combo> ();
		comboRightHand = new List<Combo> ();
		curComboLeft = new Combo (comboLeftID);
		curComboRight = new Combo (comboRightId);
		gestureGapLeft = ogGestureGap;
		gestureGapRight = ogGestureGap;
	}

	/// <summary>
	/// Records combos.
	/// </summary>
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

	/// <summary>
	/// Saves the combo to the list of the specified list of combos.
	/// </summary>
	/// <param name="LR">Indicator to check what Hand is being used</param>
	/// <param name="curCombo">Current combo.</param>
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

	/// <summary>
	/// Adds the gesture to current combo left.
	/// </summary>
	/// <param name="ID">Gesture ID</param>
	public void AddGestureToCurrentComboLeft(int ID){
		curComboLeft.AddToCombo (ID);
		inComboLeft = true;
		gestureGapLeft = ogGestureGap;
	}

	/// <summary>
	/// Adds the gesture to current combo right.
	/// </summary>
	/// <param name="ID">Gesture ID</param>
	public void AddGestureToCurrentComboRight(int ID){
		curComboRight.AddToCombo (ID);
		inComboRight = true;
		gestureGapRight = ogGestureGap;
	}

	/// <summary>
	/// Updates the combos in left hand when classification occurs.
	/// </summary>
	/// <param name="pairs">A list of paris containing combos that need to be renamed.</param>
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

	/// <summary>
	/// Updates the combos in right hand when classification occurs.
	/// </summary>
	/// <param name="pairs">A list of paris containing combos that need to be renamed.</param>
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

	public List<Combo> GetCombosRight(){
		return comboRightHand;
	}

}
