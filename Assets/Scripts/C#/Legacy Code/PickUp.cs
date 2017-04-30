using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour {

	bool held = false;

	public void setHeld(bool held){
		this.held = held;
	}

	public bool IsHeld(){
		return held;
	}

}
