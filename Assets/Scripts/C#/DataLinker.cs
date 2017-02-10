using UnityEngine;
using System.Collections;
using System.Threading;

public class DataLinker : MonoBehaviour {

	PythonLinker pl, p2;
	string[] output;

	// Use this for initialization
	void Start () {
		pl = new PythonLinker("C:/Users/Josh/Anaconda3/python.exe");

		pl.SetPythonScript ( "-i " + Application.dataPath + "/Scripts/Python/Hello.py" );

		pl.RunPythonShell ();

	
	}
	
	// Update is called once per frame
	void Update () {
		if (pl.IsProcessComplete()) {
			for (int i = 0; i < pl.GetOutput ().Count; i++) {
				Debug.Log (pl.GetOutput()[i]);
			}
		}
	}
}
