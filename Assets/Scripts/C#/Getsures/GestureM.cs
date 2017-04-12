using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureM {

	Matrix4x4[] transforms;
	float[] deltaTimes;
	string name;

	public GestureM(string name){
		this.name = name;
	}

	public GestureM(string name, Matrix4x4[] transforms, float[] deltaTimes){
		this.name = name;
		this.transforms = transforms;
		this.deltaTimes = deltaTimes;
	}

	public string GetName(){
		return name;
	}

	public void SetName(string name){
		this.name = name;
	}

	public Matrix4x4[] GetMatrixArray(){
		return transforms;
	}

	public void AddMatrix(Matrix4x4 matrix){
		if (transforms == null) {
			transforms = new Matrix4x4[] { matrix };
		} else {
			Matrix4x4[] newTransforms = new Matrix4x4[transforms.Length + 1];
			for(int i = 0; i < transforms.Length; i++){
				newTransforms [i] = transforms [i];
			}
			newTransforms [newTransforms.Length - 1] = matrix;
			transforms = newTransforms;
		}
	}

	public void SetMatrix(int i, Matrix4x4 matrix){
		transforms [i] = matrix;
	}

	public void AddTime(float time){
		if (deltaTimes == null) {
			deltaTimes = new float[] { time };
		} else {
			float[] newTimes = new float[deltaTimes.Length + 1];
			for(int i = 0; i < deltaTimes.Length; i++){
				newTimes [i] = deltaTimes [i];
			}
			newTimes [newTimes.Length - 1] = time;
			deltaTimes = newTimes;
		}
	}

	public void SetPosition(int i, float x, float y, float z){
		transforms [i].SetColumn (3, new Vector4 (z, y, z, 1));
	}

	public float[] GetDeltaTimes(){
		return deltaTimes;
	}

}
