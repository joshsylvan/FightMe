using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo {

	int ID;
	List<int> combo;

	public Combo(int ID){
		this.ID = ID;
		this.combo = new List<int>();
	}

	public Combo(int ID, List<int> combo){
		this.ID = ID;
		this.combo = combo;
	}


	public int GetID(){
		return ID;
	}

	public List<int> GetCombo(){
		return combo;
	}

	public void SetCombo(List<int> combo){
		this.combo = combo;
	}

	public void SetID(int ID){
		this.ID = ID;
	}

	public void AddToCombo(int comboID){
		this.combo.Add (comboID);
	}

	public int GetComboLength(){
		return combo.Count;
	}

}
