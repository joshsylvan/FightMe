using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Descibes the Combo data strcture.
/// </summary>
public class Combo {

	int ID; // Name of the Combo
	List<int> combo; // List of in order gestures IDs

	/// <summary>
	/// Initializes a new instance of the <see cref="Combo"/> class.
	/// </summary>
	/// <param name="ID">Name of the Gesture</param>
	public Combo(int ID){
		this.ID = ID;
		this.combo = new List<int>();
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Combo"/> class.
	/// </summary>
	/// <param name="ID">Gesture UD</param>
	/// <param name="combo">Combo to create new com sequence.</param>
	public Combo(int ID, List<int> combo){
		this.ID = ID;
		this.combo = combo;
	}

	/// <summary>
	/// Gets the ID of rhis combo.
	/// </summary>
	/// <returns>Gets the ID</returns>
	public int GetID(){
		return ID;
	}

	/// <summary>
	/// Gets the combo.
	/// </summary>
	/// <returns>The combo.</returns>
	public List<int> GetCombo(){
		return combo;
	}

	/// <summary>
	/// Sets the combo.
	/// </summary>
	/// <param name="combo">combo to set too</param>
	public void SetCombo(List<int> combo){
		this.combo = combo;
	}

	public void SetID(int ID){
		this.ID = ID;
	}

	/// <summary>
	/// Adds to combo to the current one.
	/// </summary>
	/// <param name="comboID">Combo I.</param>
	public void AddToCombo(int comboID){
		this.combo.Add (comboID);
	}

	/// <summary>
	/// Gets the length of the combo.
	/// </summary>
	/// <returns>The combo length.</returns>
	public int GetComboLength(){
		return combo.Count;
	}

}
