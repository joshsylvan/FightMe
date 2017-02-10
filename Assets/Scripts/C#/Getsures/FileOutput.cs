using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileOutput {

	string path;

	public FileOutput(){
		path = Application.dataPath;
	}

	public void GestureOutput(List<Gesture> gestures, string name){
		List<string> contents = new List<string> ();
		foreach (Gesture g in gestures) {
			foreach (Point p in g.GetPoints()) {
				contents.Add (p.getX () + "," + p.getY () + "," + p.getZ ());
			}
			contents.Add ("");
		}
		System.IO.File.WriteAllLines (path + "/" + name + ".csv", contents.ToArray());

	}

}
