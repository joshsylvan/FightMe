using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {

	GameObject[] layer1, layer2;

	int state = 0;

	public GameObject arrowLauncher;

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
		arrowLauncher.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp (KeyCode.Space)) {
			state++;
			switch (state) {
			case 0:
				ShowAllTargets ();
				break;
			case 1:
				ShowHorizontal ();
				break;
			case 2:
				ShowVertical ();
				break;
			case 3:
				ShowDiagonalLeft ();
				break;
			case 4:
				ShowDiagonalRight ();
				break;
			case 5:
				ShowLayer2 ();
				break;
			case 6:
				HideAllTargets ();
				break;
			}
		}
		if (Input.GetKeyUp (KeyCode.Backspace)) {
			state--;
			switch (state) {
			case 0:
				ShowAllTargets ();
				break;
			case 1:
				ShowHorizontal ();
				break;
			case 2:
				ShowVertical ();
				break;
			case 3:
				ShowDiagonalLeft ();
				break;
			case 4:
				ShowDiagonalRight ();
				break;
			case 5:
				ShowLayer2 ();
				break;
			case 6:
				HideAllTargets ();
				break;
			}
		}

		if (Input.GetKeyUp (KeyCode.A)) {
			// Arrow spawner
			arrowLauncher.SetActive(!arrowLauncher.activeSelf);
		}
		if (arrowLauncher.activeSelf) {
			if (Input.GetKey (KeyCode.UpArrow)) {
				arrowLauncher.transform.position =  4 * Time.deltaTime * Vector3.forward + arrowLauncher.transform.position;
			} else if(Input.GetKey (KeyCode.DownArrow)){
				arrowLauncher.transform.position =  4 * Time.deltaTime * Vector3.back + arrowLauncher.transform.position;
			} else if(Input.GetKey(KeyCode.LeftArrow)){
				arrowLauncher.transform.position =  4 * Time.deltaTime * Vector3.left + arrowLauncher.transform.position;
			} else if(Input.GetKey(KeyCode.RightArrow)){
				arrowLauncher.transform.position =  4 * Time.deltaTime * Vector3.right + arrowLauncher.transform.position;
			}
			if (Input.GetKey (KeyCode.Q)) {
				arrowLauncher.transform.position = Time.deltaTime * Vector3.up + arrowLauncher.transform.position;
			} else if (Input.GetKey (KeyCode.Z)) {
				arrowLauncher.transform.position = Time.deltaTime * Vector3.down + arrowLauncher.transform.position;
			}
		}



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
