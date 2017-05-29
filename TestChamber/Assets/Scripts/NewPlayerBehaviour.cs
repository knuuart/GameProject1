﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerBehaviour : MonoBehaviour {

	public float groundPower = 20f, airPower = 3f;
	public float moveMax;
	public float mouseSensitivity;
	private float mouseSensitivityY;
	public bool mInvert;
	public float maxY = 60f;
	public float minY = -60f;
	public float defaultRotation = 0f, rotationSmooth = 0.1f;
	public float groundSpeedLimit;
	public float airSpeedLimit;
	public bool grounded;
	public Camera cam;
	Rigidbody rb;
	float movePower;
	public float sphereRadius, sphereDistance, jumpForce;

	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody> ();
		mouseSensitivityY = mouseSensitivity;
		Cursor.lockState = CursorLockMode.Locked;

	}
	void Update(){
		if (mInvert) {
			mouseSensitivityY = mouseSensitivityY * -1;
		}
        var angles = cam.transform.localEulerAngles;
        angles.z = 0f;
		cam.transform.rotation = Quaternion.Slerp (cam.transform.rotation, Quaternion.Euler (angles), rotationSmooth * Time.deltaTime);
        
        if (Vector3.Angle(cam.transform.forward, Vector3.up) < .5f) {
            cam.transform.Rotate(cam.transform.right, .5f, Space.World);
        }

        if (Vector3.Angle(cam.transform.forward, Vector3.down) < .5f) {
            cam.transform.Rotate(cam.transform.right, -.5f, Space.World);
        }

        var RotX = Input.GetAxis ("Mouse X") * mouseSensitivity;
		var RotY = Input.GetAxis ("Mouse Y") * mouseSensitivityY;
		// RotY = Mathf.Clamp (RotY, minY, maxY);
		var qx = Quaternion.AngleAxis(RotX, Vector3.up);
		cam.transform.rotation = qx * cam.transform.rotation;

		var qy = Quaternion.AngleAxis(RotY, Vector3.ProjectOnPlane(-cam.transform.right, Vector3.up));
		var xzFwd = Vector3.ProjectOnPlane (cam.transform.forward, Vector3.up);
		var xzFwdAfter = Vector3.ProjectOnPlane (qy * cam.transform.forward, Vector3.up);
		// would be <90, but Vector3.Angle seems to return 90 for zero vectors,
		// this way no need to handle the vertical corner case where projections approach zero
		if (Vector3.Angle (xzFwd, xzFwdAfter) < 90f && xzFwdAfter.magnitude > 0.001f) {
			cam.transform.rotation = qy * cam.transform.rotation;
		}
		//if (Input.GetKeyDown(KeyCode.P)) print (Vector3.Angle (Vector3.zero, Vector3.zero)); // Angle between two zero vectors is 90 :thinking:
		//		var q = Quaternion.AngleAxis(RotX, Vector3.up);
		//		cam.transform.rotation = q * Quaternion.AngleAxis(RotY, q * Vector3.left) *  cam.transform.rotation;
		RaycastHit hit;
		if (Physics.SphereCast (transform.position, sphereRadius, Vector3.down, out hit, sphereDistance) && (rb.velocity.y > -5 && rb.velocity.y < 5)) {
			grounded = true;
		} else {
			grounded = false;
		}
		if (grounded) {
			if (Input.GetKeyDown (KeyCode.Space)) {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            movePower = groundPower;
        } else {
            movePower = airPower;
        }
        if (Input.GetKeyDown(KeyCode.End)) {
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
	void FixedUpdate () {
		float MoveX = Input.GetAxis ("Horizontal");
		float MoveZ = Input.GetAxis ("Vertical");

		var XZForward = Vector3.ProjectOnPlane (cam.transform.forward, Vector3.up);
		var XZRight = Vector3.ProjectOnPlane (cam.transform.right, Vector3.up);

		var aa = (XZRight * MoveX + XZForward * MoveZ).normalized * movePower * Time.deltaTime;

		var newVelocity = rb.velocity + aa;

		newVelocity = Vector3.ClampMagnitude(newVelocity, grounded ? groundSpeedLimit : airSpeedLimit);

		rb.velocity = newVelocity;
	}
	void OnDrawGizmos(){
		Gizmos.DrawSphere (transform.position - transform.up * sphereDistance, sphereRadius);
	}
}
