using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public float speed = 10f;
	public bool limitMovement = false;
	public bool zoomTilt = false;
	public float xMin = -20f;
	public float xMax = 20f;
	public float yMin = 8f;
	public float yMax = 20f;
	public float zMin = -20f;
	public float zMax = 10f;
	public float tiltAngle = 3f;
	Vector3 direction;

	void Start () {
		
	}

	void CameraMove(){
		transform.Translate (direction.normalized * Time.deltaTime * speed);/*, Space.World);*/
	}

	void Update () {

		if (Input.GetKey (KeyCode.A)) {
			direction = Vector3.left;
			CameraMove();
		}
		if (Input.GetKey (KeyCode.D)) {
			direction = Vector3.right;
			CameraMove();
		}
		if (Input.GetKey (KeyCode.W)) {
			direction = Vector3.forward;
			CameraMove();
		}
		if (Input.GetKey (KeyCode.S)) {
			direction = Vector3.back;
			CameraMove();
		}

		if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
			transform.Translate (Vector3.down, Space.World);
			if (transform.position.y >= yMin && zoomTilt) {
				transform.Rotate (Vector3.left * tiltAngle);
			}
		}

		if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
			transform.Translate (Vector3.up, Space.World);
			if (transform.position.y <= yMax && zoomTilt) {
				transform.Rotate (Vector3.right * tiltAngle);
			}
		}
		if (limitMovement) {
			transform.position = new Vector3 (Mathf.Clamp (transform.position.x, xMin, xMax),
												Mathf.Clamp (transform.position.y, yMin, yMax),
												Mathf.Clamp (transform.position.z, zMin, zMax));
		}
	}
}