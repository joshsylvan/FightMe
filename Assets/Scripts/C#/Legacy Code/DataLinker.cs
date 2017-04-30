using UnityEngine;
using System.Collections;
using System.Threading;

/// <summary>
/// This class was used to start a python thread in unity when Sci Kit learn was used for machine learning.
/// </summary>
public class DataLinker : MonoBehaviour {

	PythonLinker pl, p2;
	string[] output;

	// Used to initiate the python thread.
	void Start () {
		pl = new PythonLinker("C:/Users/Josh/Anaconda3/python.exe");

		pl.SetPythonScript ( "-i " + Application.dataPath + "/Scripts/Python/Hello.py" );

		pl.RunPythonShell ();

	
	}
	
	// Update is called once per frame. Wait until python shell finishes executing code and output the result to the debugger
	void Update () {
		if (pl.IsProcessComplete()) {
			for (int i = 0; i < pl.GetOutput ().Count; i++) {
				Debug.Log (pl.GetOutput()[i]);
			}
		}
	}
}
