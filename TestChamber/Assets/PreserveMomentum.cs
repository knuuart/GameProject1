using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreserveMomentum : MonoBehaviour {

    public float entranceVelocity;
    public Vector3 previous;
    Rigidbody rb;

    // Use this for initialization
    void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        entranceVelocity = (transform.position - previous).magnitude / Time.deltaTime;
        previous = transform.position.normalized;
    }
}
