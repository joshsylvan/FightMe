using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadGestures : MonoBehaviour {


	public GameObject HMD;

	List<GameObject> gestures;

	int gestureLength = 21;

	List<Gesture> classifiedGestures;

	// Use this for initialization
	void Start () {
		Init ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public void Init(){
		FileInput input = new FileInput ();
		string[] file = input.LoadGestureFile ("RightHandU");
		//		foreach (string s in file) {
		//			Debug.Log (s);
		//		}

		classifiedGestures = ParseGestureFile (file);
		DrawGesture (classifiedGestures, 20);
	}

	public void DrawGesture(List<Gesture> gestures, int i){
	
		GameObject node = Resources.Load ("GNode") as GameObject;
		GameObject gesture = new GameObject("GGG");
		float mult = 1;
		foreach (Point p in gestures[i].GetPoints()) {
			GameObject temp = Instantiate (node);
			temp.transform.SetParent (gesture.transform);
			temp.transform.localPosition = new Vector3 (p.getX()*mult, p.getY()*mult, p.getZ()*mult);
		}

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

//	void LoadFile(string name){
//
//		StreamReader theReader = new StreamReader (Application.dataPath + "/" + name);
//
//		GameObject node = Resources.Load ("GNode") as GameObject;
//		gestures = new List<GameObject> ();
//
//		string line;
//
//		int index = 0;
//		GameObject gesture = new GameObject("" + index++);
//
//
//		using (theReader) {
//
//			do {
//				
//				line = theReader.ReadLine ();
//
//				if (line != null && line.Length > 0) {
//					string[] point = line.Split (',');
//					GameObject temp = Instantiate (node);
//					temp.transform.position = new Vector3(
//						float.Parse(point[0]) * 1,
//						float.Parse(point[1]) * 1,
//						float.Parse(point[2]) * 1
//					);
//					temp.transform.SetParent(gesture.transform);
//				}
//
//				if( line != null && line.Equals("")){
//					gestures.Add (gesture);
//					gesture = new GameObject("" + index++);
//				}
//
//			} while (line != null);
//			theReader.Close ();
//		}
//
//
//
//	}




}
