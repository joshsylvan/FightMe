﻿using UnityEngine;
using System.Collections;

public class Gesture  {

	Point[] points;
	Quaternion[] rotations;
	float duration;
	string name;
	// POSSIBLE ADDITIONS
	float distanceFromTarge = 0;
	bool success = false;
	int successCount = 0;

	public Gesture(string name, Point[] points){
		this.name = name;
		this.points = points;
	}

	public Gesture(string name, Point[] points, Quaternion[] rotations){
		this.name = name;
		this.points = points;
		this.rotations = rotations;
	}

	public Gesture(string name){
		this.name = name;
		points = null;
		rotations = null;
	}

	public string GetName(){
		return name;
	}

	public Point[] GetPoints(){
		return points;
	}

	public float GetDuration(){
		return duration;
	}

	public Quaternion[] GetRotations(){
		return rotations;
	}

	public void SetName(string name){
		this.name = name;
	}

	public void SetRotations(Quaternion[] rotations){
		this.rotations = rotations;
	}

	public void SetDuration(float duration){
		this.duration = duration;
	}



	public void AddPoint(Point p){

		if (points == null) {
			points = new Point[]{ p };
		} else {
			Point[] newPoints = new Point[points.Length + 1];
			for (int i = 0; i < points.Length; i++) {
				newPoints [i] = points [i];
			}
			newPoints [newPoints.Length - 1] = p;
			points = newPoints;
		}
	}

}