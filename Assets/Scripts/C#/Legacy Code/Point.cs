using System.Collections;
using UnityEngine;

/// <summary>
/// Point class. Using in the old structures of gestures.
/// </summary>
public class Point {

	private float x, y, z;
	private bool compared = false;
	private float deltaTime = 0;

	/// <summary>
	/// Initializes a new instance of the <see cref="Point"/> class.
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	/// <param name="deltaTime">Delta time.</param>
	public Point(float x, float y, float z, float deltaTime){
		this.x = x;
		this.y = y;
		this.z = z;
		this.deltaTime = deltaTime;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Point"/> class.
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public Point(float x, float y, float z){
		this.x = x;
		this.y = y;
		this.z = z;
		this.deltaTime = 0;
	}

	/// <summary>
	/// Gets the x.
	/// </summary>
	/// <returns>The x.</returns>
	public float getX(){
		return x;
	}

	/// <summary>
	/// Gets the y.
	/// </summary>
	/// <returns>The y.</returns>
	public float getY(){
		return y;
	}

	/// <summary>
	/// Gets the z.
	/// </summary>
	/// <returns>The z.</returns>
	public float getZ(){
		return z;
	}

	/// <summary>
	/// Has node been compared the compared.
	/// </summary>
	/// <returns><c>true</c>, if point was compared, <c>false</c> otherwise.</returns>
	public bool isCompared(){
		return compared;
	}

	/// <summary>
	/// Gets the delta time.
	/// </summary>
	/// <returns>The delta time.</returns>
	public float GetDeltaTime(){
		return deltaTime;
	}

	/// <summary>
	/// Sets the compared boolean of a point.
	/// </summary>
	/// <param name="compared">has the point been compared? <c>true</c> compared.</param>
	public void setCompared(bool compared){
		this.compared = compared;
	}

	/// <summary>
	/// Gets the position vector.
	/// </summary>
	/// <returns>The position vector.</returns>
	public Vector3 GetPositionVector(){
		return new Vector3 (x, y, z);
	}
}
