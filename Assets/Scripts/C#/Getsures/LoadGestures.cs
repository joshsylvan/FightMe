using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadGestures : MonoBehaviour {

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

		string line;

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
				}
			} while (line != null);
			theReader.Close ();
		}

	}
}
