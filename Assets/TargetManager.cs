using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {

	GameObject[] layer1, layer2;

	// Use this for initialization
	void Start () {
		layer1 = new GameObject[transform.GetChild(0).childCount];
		layer2 = new GameObject[transform.GetChild(1).childCount];

		for (int i = 0; i < layer1.Length; i++) {
			layer1 [i] = transform.GetChild (0).GetChild (i).gameObject;
		}
		for (int i = 0; i < layer2.Length; i++) {
			layer2 [i] = transform.GetChild (1).GetChild (i).gameObject;
		}
		ShowDiagonalLeft ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void HideAllTargets(){
		for (int i = 0; i < layer1.Length; i++) {
			layer1 [i].SetActive (false);
		}
		for (int i = 0; i < layer2.Length; i++) {
			layer2 [i].SetActive (false);
		}
	}

	public void ShowAllTargets(){
		for (int i = 0; i < layer1.Length; i++) {
			layer1 [i].SetActive (true);
		}
		for (int i = 0; i < layer2.Length; i++) {
			layer2 [i].SetActive (true);
		}
	}	

	public void ShowHorizontal(){
		HideAllTargets ();
		layer1 [2].SetActive (true);
		layer1 [6].SetActive (true);
		layer2 [4].SetActive (true);
	}

	public void ShowVertical(){
		HideAllTargets ();
		layer1 [0].SetActive (true);
		layer1 [4].SetActive (true);
		layer2 [4].SetActive (true);
	}

	public void ShowDiagonalRight(){
		HideAllTargets ();
		layer1 [1].SetActive (true);
		layer1 [5].SetActive (true);
		layer2 [0].SetActive (true);
		layer2 [2].SetActive (true);
		layer2 [4].SetActive (true);
	}

	public void ShowDiagonalLeft(){
		HideAllTargets ();
		layer1 [3].SetActive (true);
		layer1 [7].SetActive (true);
		layer2 [1].SetActive (true);
		layer2 [3].SetActive (true);
		layer2 [4].SetActive (true);
	}

	public void ShowLayer2(){
		HideAllTargets ();
		for (int i = 0; i < layer2.Length; i++) {
			layer2 [i].SetActive (true);
		}
	}

}
