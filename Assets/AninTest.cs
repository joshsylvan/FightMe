using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AninTest : MonoBehaviour {

	Animation anim;
	List<Gesture> gestures;
	public GestureLoader gl;
//	AnimationClip clip;
	void Start () {

		gl.Init ();
		gestures = gl.GetClassifiedGestures();
		Debug.Log (gestures.Count);
		anim = GetComponent<Animation>();
		AnimationClip clip = new AnimationClip();
		clip.legacy = true;


		AnimationCurve curve;
		Keyframe[] keysX;
		keysX = new Keyframe[gestures[13].GetPoints().Length];
		for (int i = 0; i < gestures [13].GetPoints ().Length; i++) {
			keysX [i] = new Keyframe (gestures [13].GetPoints ()[i].GetDeltaTime(), gestures [13].GetPoints ()[i].getX());
		}
		curve = new AnimationCurve(keysX);
		clip.SetCurve ("", typeof(Transform), "localPosition.x", curve);

		Keyframe[] keysY;
		keysY = new Keyframe[gestures[13].GetPoints().Length];
		for (int i = 0; i < gestures [13].GetPoints ().Length; i++) {
			keysY [i] = new Keyframe (gestures [13].GetPoints ()[i].GetDeltaTime(), gestures [13].GetPoints ()[i].getY());
		}
		curve = new AnimationCurve(keysY);
		clip.SetCurve ("", typeof(Transform), "localPosition.y", curve);

		Keyframe[] keysZ;
		keysZ = new Keyframe[gestures[13].GetPoints().Length];
		for (int i = 0; i < gestures [13].GetPoints ().Length; i++) {
			keysZ [i] = new Keyframe (gestures [13].GetPoints ()[i].GetDeltaTime(), gestures [13].GetPoints ()[i].getZ());
		}
		curve = new AnimationCurve(keysZ);
		clip.SetCurve ("", typeof(Transform), "localPosition.z", curve);


		Keyframe[] rKeyX;
		rKeyX = new Keyframe[gestures [13].GetRotations ().Length];
		for (int i = 0; i < gestures [13].GetRotations ().Length; i++) {
			rKeyX [i] = new Keyframe (gestures [13].GetPoints ()[i].GetDeltaTime(), gestures [13].GetRotations ()[i].x);
		}
		curve = new AnimationCurve(rKeyX);
		clip.SetCurve ("", typeof(Transform), "localRotation.x", curve);

		Keyframe[] rKeyY;
		rKeyY = new Keyframe[gestures [13].GetRotations ().Length];
		for (int i = 0; i < gestures [13].GetRotations ().Length; i++) {
			rKeyY [i] = new Keyframe (gestures [13].GetPoints ()[i].GetDeltaTime(), gestures [13].GetRotations ()[i].y);
		}
		curve = new AnimationCurve(rKeyY);
		clip.SetCurve ("", typeof(Transform), "localRotation.y", curve);

		Keyframe[] rKeyZ;
		rKeyZ = new Keyframe[gestures [13].GetRotations ().Length];
		for (int i = 0; i < gestures [13].GetRotations ().Length; i++) {
			rKeyZ [i] = new Keyframe (gestures [13].GetPoints ()[i].GetDeltaTime(), gestures [13].GetRotations ()[i].z);
		}
		curve = new AnimationCurve(rKeyZ);
		clip.SetCurve ("", typeof(Transform), "localRotation.z", curve);

		Keyframe[] rKeyW;
		rKeyW = new Keyframe[gestures [13].GetRotations ().Length];
		for (int i = 0; i < gestures [13].GetRotations ().Length; i++) {
			rKeyW [i] = new Keyframe (gestures [13].GetPoints ()[i].GetDeltaTime(), gestures [13].GetRotations ()[i].w);
		}
		curve = new AnimationCurve(rKeyW);
		clip.SetCurve ("", typeof(Transform), "localRotation.w", curve);

		anim.AddClip(clip, "test");
		anim.Play("test");
	}
	
	// Update is called once per frame
	void Update () {
		if (!anim.isPlaying) {
			anim.Play("test");
		}
	}

}
