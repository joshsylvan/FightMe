using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AninTest : MonoBehaviour {

	Animation anim;
//	AnimationClip clip;
	void Start () {
		anim = GetComponent<Animation>();



		AnimationClip clip = new AnimationClip();
		clip.legacy = true;


		AnimationCurve xCurve;
		Keyframe[] keysX;
		keysX = new Keyframe[3];
		keysX[0] = new Keyframe(0.0f, 0.0f);
		keysX[1] = new Keyframe(1.0f, 1.5f);
		keysX[2] = new Keyframe(2.0f, 0.0f);
		xCurve = new AnimationCurve(keysX);
		clip.SetCurve ("", typeof(Transform), "localPosition.x", xCurve);

		AnimationCurve yCurve;
		Keyframe[] keys;
		keys = new Keyframe[3];
		keys[0] = new Keyframe(0.0f, 0.0f);
		keys[1] = new Keyframe(1.0f, 1.5f);
		keys[2] = new Keyframe(2.0f, 0.0f);
		xCurve = new AnimationCurve(keys);
		clip.SetCurve ("", typeof(Transform), "localPosition.y", xCurve);

		AnimationCurve rxCurve;
		Keyframe[] rxKeys;
		rxKeys = new Keyframe[3];
		rxKeys[0] = new Keyframe(0.0f, 0.0f);
		rxKeys[1] = new Keyframe(1.0f, 2.0f);
		rxKeys[2] = new Keyframe(2.0f, 0.0f);

		rxCurve = new AnimationCurve(rxKeys);
		clip.SetCurve ("", typeof(Transform), "localRotation.y", rxCurve);

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
