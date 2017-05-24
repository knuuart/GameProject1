using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
	public float movePower;
	public float moveMax;
	public float mouseSensitivity;
	private float mouseSensitivityY;
	public bool mInvert;
	public float maxY = 60f;
	public float minY = -60f;
	public float defaultRotation = 0f, rotationSmooth = 0.1f;
	public Camera cam;
	Rigidbody rb;

	// Use this for initialization
	void Awake () {
		rb = GetComponent<Rigidbody> ();
		mouseSensitivityY = mouseSensitivity;
		Cursor.lockState = CursorLockMode.Locked;

	}
	void Update(){
		cam.transform.rotation = Quaternion.Slerp (Quaternion.Euler (cam.transform.localEulerAngles), Quaternion.Euler (cam.transform.localEulerAngles.x, 
			cam.transform.localEulerAngles.y, defaultRotation), Time.time * rotationSmooth);

		var RotX = Input.GetAxis ("Mouse X") * mouseSensitivity;
		var RotY = Input.GetAxis ("Mouse Y") * mouseSensitivityY;
		// RotY = Mathf.Clamp (RotY, minY, maxY);
		var qx = Quaternion.AngleAxis(RotX, Vector3.up);
		cam.transform.rotation = qx * cam.transform.rotation;
		var qy = Quaternion.AngleAxis(RotY, Vector3.ProjectOnPlane(-cam.transform.right, Vector3.up));
		cam.transform.rotation = qy * cam.transform.rotation;

//		var q = Quaternion.AngleAxis(RotX, Vector3.up);
//		cam.transform.rotation = q * Quaternion.AngleAxis(RotY, q * Vector3.left) *  cam.transform.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float MoveX = Input.GetAxis ("Horizontal");
		float MoveZ = Input.GetAxis ("Vertical");

		var aa = (cam.transform.right * MoveX + cam.transform.forward * MoveZ).normalized * movePower * Time.deltaTime;


		rb.velocity = new Vector3 (aa.x, rb.velocity.y, aa.z);
	}
}
