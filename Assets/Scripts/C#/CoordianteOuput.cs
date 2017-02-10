using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoordianteOuput : MonoBehaviour {

	public Text right, left;
	string outputRight, outputLeft;
	// Use this for initialization
	void Start () {
		outputRight = "Right: ";
		outputLeft = "Left: ";
	}
	
	// Update is called once per frame
	void Update () {
		if (right != null && left != null) {
			right.text = outputRight;
			left.text = outputLeft;
		}
	}

	public void SetRight(string text){
		outputRight = text;
	}

	public void SetLeft(string text){
		outputLeft = text;
	}
}
