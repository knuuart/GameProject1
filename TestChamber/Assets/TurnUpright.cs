using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnUpright : MonoBehaviour {
	Vector3 startRotation, currentRotation;
	
	// Use this for initialization
	void Start () {
		startRotation = new Vector3(transform.rotation.x, 0 , transform.rotation.y);
	}
	
	// Update is called once per frame
	void Update () {
		currentRotation = new Vector3(transform.rotation.x, 0 , transform.rotation.y);
		Vector3 difference = currentRotation - startRotation;
		transform.Rotate ((difference) * Time.deltaTime * 25);
	}
}
