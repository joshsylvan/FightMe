using System;
using System.Diagnostics;
using System.Threading;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Text;


public class PythonLinker {

	string path;
	string pythonScriptPath;
	Process python;
	ProcessStartInfo pythonInfo;
	List<string> output = new List<string>();
	bool completed = false;

	ThreadStart ths;
	Thread th;

	public PythonLinker(string path){
		this.path = path;
		pythonInfo = new ProcessStartInfo();
		python = new Process ();
	}


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

	public void SetPath(string path){
		this.path = path;
	}

	public void SetPythonScript(string pythonScriptPath){
		this.pythonScriptPath = pythonScriptPath;
	}

	public Process GetPythonProcess(){
		return python;
	}

	public bool IsProcessComplete(){
		return completed;
	}

	public List<string> GetOutput(){
		return output;
	}


}
