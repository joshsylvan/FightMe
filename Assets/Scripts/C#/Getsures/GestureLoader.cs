using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureLoader : MonoBehaviour {

	int gestureLength = 21;

	List<Gesture> classifiedGestures;
	List<GestureM> classifiedGesturesM;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Init(){
		FileInput input = new FileInput ();
		string[] file = input.LoadGestureFile ("LeftHandU");
		//		foreach (string s in file) {
		//			Debug.Log (s);
		//		}

		classifiedGesturesM = ParseGestureFileM (file);
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

	List<GestureM> ParseGestureFileM(string[] file){
		List<GestureM> gestures = new List<GestureM>();
		for (int i = 0; i < file.Length/gestureLength; i++) {
			GestureM g = new GestureM ("TEMP");
			for (int j = i * gestureLength; j < gestureLength + (gestureLength * i); j++) {
				if (j % gestureLength == 0) {
					g.SetName (file [i * 21]);
				} else {
					string[] temp = file [j].Split(',');
					Matrix4x4 m = new Matrix4x4();
					m.SetColumn (0, new Vector4 (
						float.Parse(temp[0]), 
						float.Parse(temp[1]),
						float.Parse(temp[2]), 
						0)
					);
					m.SetColumn (1, new Vector4 (
						float.Parse(temp[3]), 
						float.Parse(temp[4]),
						float.Parse(temp[5]), 
						0)
					);
					m.SetColumn (2, new Vector4 (
						float.Parse(temp[6]), 
						float.Parse(temp[7]),
						float.Parse(temp[8]), 
						0)
					);
					m.SetColumn (3, new Vector4 (
						float.Parse(temp[9]), 
						float.Parse(temp[10]),
						float.Parse(temp[11]), 
						1)
					);
					g.AddMatrix (m);
				}
			}
			gestures.Add (g);
		}

		return gestures;
	}

	public List<Gesture> GetClassifiedGestures(){
		return classifiedGestures;
	}

	public List<GestureM> GetClassifiedGesturesM(){
		return classifiedGesturesM;
	}
}
