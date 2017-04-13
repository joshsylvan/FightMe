using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainedAISword : MonoBehaviour {

	Animation anim;
	List<Gesture> gestures;
	List<AnimationClip> animationClips;
	int index = 0;
	int playIndex = 0;
	bool cycleAnimations = false;

	void Awake(){
		anim = GetComponent<Animation> ();
		animationClips = new List<AnimationClip> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (cycleAnimations) {
			if (!anim.isPlaying) {
				if (playIndex >= index) {
					playIndex = 0;
				}
				Debug.Log (playIndex);
				anim.Play ("" + playIndex++);
			}
		}
	}

	public void CreateAnimationClipsFromGestures(List<Gesture> gestures){
		
		animationClips = new List<AnimationClip> ();
		this.gestures = gestures;
		foreach (Gesture g in this.gestures) {
			AnimationClip clip = new AnimationClip ();
			clip.legacy = true;

			AnimationCurve curve;
			// Position Keyframes
			Keyframe[] keysX = new Keyframe[g.GetMatrixArray().Length];
			Keyframe[] keysY = new Keyframe[g.GetMatrixArray().Length];
			Keyframe[] keysZ = new Keyframe[g.GetMatrixArray().Length];
			// Rotation Keyframes
			Keyframe[] keysRX = new Keyframe[g.GetMatrixArray().Length];
			Keyframe[] keysRY = new Keyframe[g.GetMatrixArray().Length];
			Keyframe[] keysRZ = new Keyframe[g.GetMatrixArray().Length];
			Keyframe[] keysRW = new Keyframe[g.GetMatrixArray().Length];
			for (int i = 0; i < g.GetMatrixArray().Length; i++) {
				keysX [i] = new Keyframe (g.GetDeltaTimes()[i], g.GetMatrixArray()[i].GetPosition().x);
				keysY [i] = new Keyframe (g.GetDeltaTimes()[i], g.GetMatrixArray()[i].GetPosition().y);
				keysZ [i] = new Keyframe (g.GetDeltaTimes()[i], g.GetMatrixArray()[i].GetPosition().z);
				keysRX [i] = new Keyframe (g.GetDeltaTimes()[i], g.GetMatrixArray()[i].GetRotation().x);
				keysRY [i] = new Keyframe (g.GetDeltaTimes()[i], g.GetMatrixArray()[i].GetRotation().y);
				keysRZ [i] = new Keyframe (g.GetDeltaTimes()[i], g.GetMatrixArray()[i].GetRotation().z);
				keysRW [i] = new Keyframe (g.GetDeltaTimes()[i], g.GetMatrixArray()[i].GetRotation().w);
			}
			curve = new AnimationCurve(keysX);
			clip.SetCurve ("", typeof(Transform), "localPosition.x", curve);
			curve = new AnimationCurve(keysY);
			clip.SetCurve ("", typeof(Transform), "localPosition.y", curve);
			curve = new AnimationCurve(keysZ);
			clip.SetCurve ("", typeof(Transform), "localPosition.z", curve);
			curve = new AnimationCurve(keysRX);
			clip.SetCurve ("", typeof(Transform), "localRotation.x", curve);
			curve = new AnimationCurve(keysRY);
			clip.SetCurve ("", typeof(Transform), "localRotation.y", curve);
			curve = new AnimationCurve(keysRZ);
			clip.SetCurve ("", typeof(Transform), "localRotation.z", curve);
			curve = new AnimationCurve(keysRW);
			clip.SetCurve ("", typeof(Transform), "localRotation.w", curve);
			animationClips.Add (clip);
			anim.AddClip(clip, ""+index++);
		}
		/* NEEDS REFACTOR
		*/ //END
	}

	public void CylcleAnimations(){
		cycleAnimations = true;
	}

	public void ResetClips(){
		for (int i = 0; i < index; i++) {
			anim.RemoveClip ("" + i);
		}
		index = 0;
	}

	public void SetGestures(List<Gesture> gestures){
		this.gestures = gestures;
	}
}
