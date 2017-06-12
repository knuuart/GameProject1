using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {
	public AudioClip[] stepSounds;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void PlaySoundOnFrame(){
		Vector3 soundPos = transform.position;
		AudioSource.PlayClipAtPoint (stepSounds [Random.Range (0, stepSounds.Length)], soundPos, 0.15f);
	}
}
