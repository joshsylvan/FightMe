using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileInput {

	string path;

	// Use this for initialization
	public FileInput () {
		path = Application.dataPath;
	}

	public string[] LoadGestureFile(string name){

		List<string> file = new List<string>();

		StreamReader fileReader = new StreamReader(path + "/" + name + ".csv");

		while (!fileReader.EndOfStream) {
			string temp = fileReader.ReadLine ();
			if (temp != "") {
				file.Add (temp);
			}
		}

		fileReader.Close (); 
		return file.ToArray ();

	}

}
