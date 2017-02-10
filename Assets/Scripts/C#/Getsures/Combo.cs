using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo {

	string name;
	List<Gesture> combo;

	public Combo(string name){
		this.name = name;
	}

	public Combo(string name, List<Gesture> combo){
		this.name = name;
		this.combo = combo;
	}

	public string GetName(){
		return name;
	}

	public List<Gesture> GetGestures(){
		return combo;
	}

	public void SetGestures(List<Gesture> patterns){
		this.combo = combo;
	}

	public void SetName(string name){
		this.name = name;
	}

}
