using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureLoader : MonoBehaviour {

	int gestureLength = 21;

	List<Gesture> classifiedGestures;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Init(){
		FileInput input = new FileInput ();
		string[] file = input.LoadGestureFile ("RightHandc");
		//		foreach (string s in file) {
		//			Debug.Log (s);
		//		}

		classifiedGestures = ParseGestureFile (file);
	}

	List<Gesture> ParseGestureFile(string[] file){
		List<Gesture> gestures = new List<Gesture>();
		for (int i = 0; i < file.Length/gestureLength; i++) {
			Gesture g = new Gesture ("TEMP");
			for (int j = i * gestureLength; j < gestureLength + (gestureLength * i); j++) {
				if (j % gestureLength == 0) {
					g.SetName (file [i * 21]);
				} else {
					string[] temp = file [j].Split(',');
					g.AddPoint(new Point(
						float.Parse(temp[0]),
						float.Parse(temp[1]),
						float.Parse(temp[2]),
						float.Parse(temp[7])
					));
					g.AddRotation(new Quaternion(
						float.Parse(temp[3]),
						float.Parse(temp[4]),
						float.Parse(temp[5]),
						float.Parse(temp[6])
					));
				}
			}
			gestures.Add (g);
		}

		return gestures;
	}

	public List<Gesture> GetClassifiedGestures(){
		return classifiedGestures;
	}
}
