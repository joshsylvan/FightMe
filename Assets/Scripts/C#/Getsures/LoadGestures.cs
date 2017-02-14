using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadGestures : MonoBehaviour {


	public GameObject HMD;

	List<GameObject> gestures;

	// Use this for initialization
	void Start () {
		LoadFile ("test.csv");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LoadFile(string name){

		StreamReader theReader = new StreamReader (Application.dataPath + "/" + name);

		GameObject node = Resources.Load ("GNode") as GameObject;
		gestures = new List<GameObject> ();

		string line;

		int index = 0;
		GameObject gesture = new GameObject("" + index++);


		using (theReader) {

			do {
				
				line = theReader.ReadLine ();

				if (line != null && line.Length > 0) {
					string[] point = line.Split (',');
					GameObject temp = Instantiate (node);
					temp.transform.position = new Vector3(
						float.Parse(point[0]) * 1,
						float.Parse(point[1]) * 1,
						float.Parse(point[2]) * 1
					);
					temp.transform.SetParent(gesture.transform);
				}

				if( line != null && line.Equals("")){
					gestures.Add (gesture);
					gesture = new GameObject("" + index++);
				}

			} while (line != null);
			theReader.Close ();
		}



	}




}
