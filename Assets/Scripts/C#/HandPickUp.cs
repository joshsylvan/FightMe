using UnityEngine;
using System.Collections;

public class HandPickUp : MonoBehaviour {

	GameObject currentObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.GetComponent<PickUp> () != null) {
			currentObject = col.gameObject;
		}
	}

	void OnTriggerStay(Collider col){
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject == currentObject) {
			currentObject = null;
		}
	}

	public GameObject GetObjectInContactWith(){
		return currentObject;
	}
}
