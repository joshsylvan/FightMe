using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Combo prediction test class. Majority of code is comment out and is there as a reference.
/// </summary>
public class ComboPredictionTests : MonoBehaviour {

	List<Combo> combos = new List<Combo>();
	float timer = 0;
	bool startTime = false;

	// Use this for initialization
	void Start () {

		// Classified gestrues are 0, 1, 2, 4, 5, 6, 7

//		combos.Add (new Combo(11, new List<int>() {1, 3, 4, 0}));
//		combos.Add (new Combo(12, new List<int>() {1, 0, 0}));
//		combos.Add (new Combo(12, new List<int>() {1, 0, 0}));
//		combos.Add (new Combo(13, new List<int>() {1, 1, 2, 7, 2}));
//		combos.Add (new Combo(14, new List<int>() {1, 4, 2, 7, 2}));
//		combos.Add (new Combo(14, new List<int>() {1, 4, 2, 7, 2}));
//		combos.Add (new Combo(15, new List<int>() {1, 2, 2, 0, 2}));
//		combos.Add (new Combo(16, new List<int>() {1, 1, 0, 7, 2}));
//		combos.Add (new Combo(16, new List<int>() {1, 1, 0, 7, 2}));
//		combos.Add (new Combo(17, new List<int>() {1, 0, 2, 4, 6}));
//		combos.Add (new Combo(18, new List<int>() {1, 1, 0, 0, 1}));
//		combos.Add (new Combo(19, new List<int>() {1, 2, 5, 0, 2, 4, 9}));
//		combos.Add (new Combo(19, new List<int>() {1, 2, 5, 0, 2, 4, 9}));
//		combos.Add (new Combo(19, new List<int>() {1, 2, 5, 0, 2, 4, 9}));
//		combos.Add (new Combo(20, new List<int>() {1, 0, 7}));
//		combos.Add (new Combo(21, new List<int>() {1, 6, 1, 1}));

//		combos.Add (new Combo(0, new List<int>() {0, 3, 4}));
//		combos.Add (new Combo(1, new List<int>() {0, 3, 4}));
//		combos.Add (new Combo(2, new List<int>() {0, 0, 0}));
//		combos.Add (new Combo(3, new List<int>() {0, 1, 2, 7, 2}));
//		combos.Add (new Combo(4, new List<int>() {0, 1, 2, 7, 2}));
//		combos.Add (new Combo(5, new List<int>() {0, 1, 2, 7, 2}));
//		combos.Add (new Combo(6, new List<int>() {0, 1, 2, 7, 2}));
//		combos.Add (new Combo(7, new List<int>() {0, 1, 2, 7, 2}));
//		combos.Add (new Combo(8, new List<int>() {0, 1, 2, 7, 2}));
//		combos.Add (new Combo(9, new List<int>() {0, 1, 2, 4}));
//		combos.Add (new Combo(10, new List<int>() {0, 1, 5, 0, 1}));
//		combos.Add (new Combo(11, new List<int>() {0, 1, 5, 0, 2}));
//		combos.Add (new Combo(12, new List<int>() {0, 6, 7}));
//		combos.Add (new Combo(13, new List<int>() {0, 6, 1}));

		int numerOfCombos = 1000;
		int comboLength = 5;
		int numberOfGestures = 20;
		for (int i = 0; i < numerOfCombos; i++) {
			List<int> seq = new List<int> ();
			for (int j = 0; j < comboLength; j++) {
				seq.Add (Random.Range(0, numberOfGestures));
			}
			combos.Add (new Combo (i, seq));
		}
		combos.Add (new Combo (-1, new List<int> (){ 0, 1, 2, 3, 4}));
		combos.Add (new Combo (-1, new List<int> (){ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9}));
		combos.Add (new Combo (-1, new List<int> (){ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14}));
		combos.Add (new Combo (-1, new List<int> (){ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19}));


		float sysTime = System.DateTime.Now.Millisecond;

//		startTime = true;
		ComboPredictor cp = new ComboPredictor (combos);
		cp.CreateTreesFromCombos ();

		int ii = cp.PredictSequence ( new List<int>(){ 0, 1, 2, 3});
//		int ii = cp.PredictSequence ( new List<int>(){0, 1, 5, 0, 1, 1});

		float newTime = System.DateTime.Now.Millisecond - sysTime;
		Debug.Log ("Input: " + "0, 1, 5, 0, 1, 1" + " -> next gesture = " + ii);
		Debug.Log ("Time taken : " + newTime + " milliSeconds.");
	}
	
	// Update is called once per frame
	void Update () {
	}
}
