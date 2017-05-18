using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnUpright : MonoBehaviour {
	Vector3 startRotation, currentRotation;
	
	// Use this for initialization
	void Start () {
        startRotation = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);
	}
	
	// Update is called once per frame
	void Update () {
        //if(transform.rotation != Quaternion.identity) {
        //    transform.rotation = Quaternion.
        //}
        //currentRotation = new Vector3(transform.rotation.x, 0, transform.rotation.z);
        //Vector3 difference = startRotation - currentRotation;
        //transform.Rotate((difference) * Time.deltaTime * 25);
    }
}
