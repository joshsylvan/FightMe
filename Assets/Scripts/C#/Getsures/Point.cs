using System.Collections;
using UnityEngine;

public class Point {

	private float x, y, z;
	private bool compared = false;
	private float deltaTime = 0;

	public Point(float x, float y, float z, float deltaTime){
		this.x = x;
		this.y = y;
		this.z = z;
		this.deltaTime = deltaTime;
	}

	public Point(float x, float y, float z){
		this.x = x;
		this.y = y;
		this.z = z;
		this.deltaTime = 0;
	}

	public float getX(){
		return x;
	}

	public float getY(){
		return y;
	}

	public float getZ(){
		return z;
	}

	public bool isCompared(){
		return compared;
	}

	public float GetDeltaTime(){
		return deltaTime;
	}

	public void setCompared(bool compared){
		this.compared = compared;
	}

	public Vector3 GetPositionVector(){
		return new Vector3 (x, y, z);
	}
}
