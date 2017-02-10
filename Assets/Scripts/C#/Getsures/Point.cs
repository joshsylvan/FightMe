using System.Collections;

public class Point {

	private float x, y, z;
	private bool compared = false;

	public Point(float x, float y, float z){
		this.x = x;
		this.y = y;
		this.z = z;
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

	public void setCompared(bool compared){
		this.compared = compared;
	}
}
