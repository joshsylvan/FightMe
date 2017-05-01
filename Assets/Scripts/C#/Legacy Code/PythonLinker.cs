using System;
using System.Diagnostics;
using System.Threading;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Creates a python shell on a seperate thread. This class was used when scikit learn was still being used.
/// </summary>
public class PythonLinker {
	string path; // path of python interpretor.
	string pythonScriptPath; // python script
	Process python; // python process
	ProcessStartInfo pythonInfo; // python process information
	List<string> output = new List<string>(); // outputs of python process
	bool completed = false; // marks is a python process has been completed

	ThreadStart ths;
	Thread th;

	public PythonLinker(string path){
		this.path = path;
		pythonInfo = new ProcessStartInfo();
		python = new Process ();
	}

	/// <summary>
	/// Runs the python shell.
	/// </summary>
	public void RunPythonShell(){
		if (path != null) {
			python.StartInfo = pythonInfo;
			pythonInfo.FileName = path;
			pythonInfo.Arguments = pythonScriptPath;

			pythonInfo.CreateNoWindow = false;

			pythonInfo.RedirectStandardOutput = true;
			pythonInfo.UseShellExecute = false;

			ths = new ThreadStart ( () => ThreadToRun() );
			th = new Thread (ths);
			th.Start ();


		} else {
			Console.WriteLine ("No python path set!");
		}
	}

	/// <summary>
	/// Threads to run python on.
	/// </summary>
	public void ThreadToRun(){
		completed = false;
		python.Start ();

		while (!python.HasExited) {
			string next = python.StandardOutput.ReadLine ();
			if (next.Equals ("Y")) {
				python.Kill ();
			} else {
				output.Add (next);
			}
		}
		completed = true;
		th.Abort ();
	}

	/// <summary>
	/// Sets the path of the python interpretor.
	/// </summary>
	/// <param name="path">Path.</param>
	public void SetPath(string path){
		this.path = path;
	}

	public void SetPythonScript(string pythonScriptPath){
		this.pythonScriptPath = pythonScriptPath;
	}

	public Process GetPythonProcess(){
		return python;
	}

	/// <summary>
	/// Determines whether this instance is process complete.
	/// </summary>
	/// <returns><c>true</c> if this instance is process complete; otherwise, <c>false</c>.</returns>
	public bool IsProcessComplete(){
		return completed;
	}

	public List<string> GetOutput(){
		return output;
	}


}
