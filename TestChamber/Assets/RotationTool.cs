using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTool : MonoBehaviour {
    public GameObject playerCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(playerCamera.transform.position);
	}
}
