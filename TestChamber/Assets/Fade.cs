using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {
	float t = 1;
	public float endFade = 1.5f, startFade = 3f;
	bool start;

	// Use this for initialization
	void Start () {
		start = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (start) {
			t -= Time.deltaTime / startFade;
			GetComponent<Renderer> ().material.color = new Color(0,0,0, t);
			if (t < 0.1f) {
				start = false;
				gameObject.SetActive (false);
			}
		}
		if (gameObject.activeSelf == true && start == false) {
			t += Time.deltaTime / endFade;
			if (t > 0.9f) {
				GetComponent<Renderer> ().material.color = Color.black;
			} else {
				GetComponent<Renderer> ().material.color = new Color(0,0,0, t);			
			}

		}
	}
}
