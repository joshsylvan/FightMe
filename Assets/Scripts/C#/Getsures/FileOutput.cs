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
			contents.Add (g.GetName ());
			for (int i = 0; i < g.GetPoints ().Length; i++) {
				contents.Add (g.GetPoints ()[i].getX () + "," + g.GetPoints ()[i].getY () + "," + g.GetPoints ()[i].getZ () 
					+ g.GetRotations()[i].w + "," + g.GetRotations()[i].x + "," + g.GetRotations()[i].y + "," + g.GetRotations()[i].z
				);
			}
			contents.Add ("");
		}
		System.IO.File.WriteAllLines (path + "/" + name + ".csv", contents.ToArray());

	}

	public void ComboOutput(List<Combo> combos, string name){
		List<string> contents = new List<string> ();
		foreach (Combo c in combos) {
			string line = c.GetID() + ",";
			foreach(int g in c.GetCombo()){
				line += g + ",";
			}
			contents.Add (line);
		}
		System.IO.File.WriteAllLines (path + "/" + name + ".csv", contents.ToArray());
	}

}
