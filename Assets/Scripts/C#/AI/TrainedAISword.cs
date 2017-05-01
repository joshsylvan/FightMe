using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Trained AI sword, Used for the Trained AI's weapons.
/// </summary>
public class TrainedAISword : MonoBehaviour {

	public Animation anim; // Animation ofbject to play Gestures.
	List<Gesture> gestures; // List of all known classified gestures
	List<AnimationClip> animationClips = new List<AnimationClip> (); // List of animation attacks based on gestures.
	int index = 0; // index is used to signify the number of animations.
	int playIndex = 0; // current gesture playing
	bool cycleAnimations = false;
	bool parry = false;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake(){
		//anim = GetComponent<Animation> ();
		//animationClips = new List<AnimationClip> ();
	}
	
	/// <summary>
	/// Update the sword model if gesture animations are cycled.
	/// </summary>
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

	/// <summary>
	/// Creates animation clips for attack from a list of classified gestures.
	/// </summary>
	/// <param name="gestures">Getsures to use to make animatnions.</param>
	public void CreateAnimationClipsFromGestures(List<Gesture> gestures){
		animationClips = new List<AnimationClip> ();
		this.gestures = gestures;
		foreach (Gesture g in gestures) {
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
			//anim.AddClip(clip, ""+index++);
		}

		AnimationClip clipI = new AnimationClip ();
		clipI.legacy = true;

		AnimationCurve curveI;
		// Position Keyframes
		Keyframe[] keysXI = new Keyframe[1]{new Keyframe(0, 0.274f)};
		Keyframe[] keysYI = new Keyframe[1]{ new Keyframe (0, -0.215f) };
		Keyframe[] keysZI = new Keyframe[1]{ new Keyframe (0, 0) };
		// Rotation Keyframes
		Keyframe[] keysRXI = new Keyframe[1]{new Keyframe(0, -0.49f)};
		Keyframe[] keysRYI = new Keyframe[1]{new Keyframe(0, 0.34f)};
		Keyframe[] keysRZI = new Keyframe[1]{new Keyframe(0, 0.08f)};
		Keyframe[] keysRWI = new Keyframe[1]{new Keyframe(0, 0.79f)};
		curveI = new AnimationCurve(keysXI);
		clipI.SetCurve ("", typeof(Transform), "localPosition.x", curveI);
		curveI = new AnimationCurve(keysYI);
		clipI.SetCurve ("", typeof(Transform), "localPosition.y", curveI);
		curveI = new AnimationCurve(keysZI);
		clipI.SetCurve ("", typeof(Transform), "localPosition.z", curveI);
		curveI = new AnimationCurve(keysRXI);
		clipI.SetCurve ("", typeof(Transform), "localRotation.x", curveI);
		curveI = new AnimationCurve(keysRYI);
		clipI.SetCurve ("", typeof(Transform), "localRotation.y", curveI);
		curveI = new AnimationCurve(keysRZI);
		clipI.SetCurve ("", typeof(Transform), "localRotation.z", curveI);
		curveI = new AnimationCurve(keysRWI);
		clipI.SetCurve ("", typeof(Transform), "localRotation.w", curveI);
		animationClips.Add (clipI);
		//anim.AddClip(clipI, ""+index++);
		//anim.Play (""+gestures.Count);
		//Debug.Log ("Created " + animationClips.Count + " animations from Gestures");
		/* NEEDS REFACTOR
		*/ //END
		index = 0;
		foreach (AnimationClip a in animationClips) {
			anim.AddClip (a, ""+index++);
		}

		Debug.Log ("Created " + anim.GetClipCount() + " animations from Gestures");
	}

	/// <summary>
	/// Cylcles the animations the sword has.
	/// </summary>
	public void CylcleAnimations(){
		cycleAnimations = true;
	}

	// Reset player animatnio clips.
	public void ResetClips(){
		for (int i = 0; i < index; i++) {
			anim.RemoveClip ("" + i);
		}
		index = 0;
	}

	/// <summary>
	/// Sets the gestures.
	/// </summary>
	/// <param name="gestures">Gestures.</param>
	public void SetGestures(List<Gesture> gestures){
		this.gestures = gestures;
	}

	/// <summary>
	/// Plays the clip with required index.
	/// </summary>
	/// <param name="index">Index to play..</param>
	public void PlayClip(int index){
		anim.Play ("" + index);
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "PlayerWeapon" || col.gameObject.tag == "PlayerShield") {
			parry = true;

		}
	}

	public List<Gesture> GetGestures(){
		return gestures;
	}

	public void EndParry(){
		this.parry = false;
	}

	public bool IsParry(){
		return parry;
	}
}
