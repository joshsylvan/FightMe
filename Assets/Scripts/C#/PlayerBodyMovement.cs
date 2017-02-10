using UnityEngine;
using System.Collections;

public class PlayerBodyMovement : MonoBehaviour {

	public GameObject playerHead;
	Vector3 headPosition, headRotation;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		headPosition = playerHead.transform.position;
		headRotation = playerHead.transform.rotation.eulerAngles;
		transform.position = new Vector3 (headPosition.x, headPosition.y, headPosition.z);

		if (headRotation.x <= 80 && headRotation.x >= -80) {
			transform.rotation = Quaternion.Euler (new Vector3 (0, headRotation.y, 0));
		} 
	}
}
