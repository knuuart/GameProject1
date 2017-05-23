using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviourScript : MonoBehaviour {

    public float movePower;
    public float moveMax;
    public float mouseSensitivity;
    private float mouseSensitivityY;
    public bool mInvert;
    public float maxY = 60f;
    public float minY = -60f;
	public float defaultRotation = 0f, rotationSmooth = 0.1f;
    public Transform cameraOffset, cameraTransform;



    public float RotX;
    public float RotY;

    public Camera cam;
    Rigidbody rb;

	
	void Start () {
        rb = GetComponent<Rigidbody>();

        mouseSensitivityY = mouseSensitivity;
        if (mInvert) {
            mouseSensitivityY = mouseSensitivityY * -1;
        }
        Cursor.lockState = CursorLockMode.Locked;
    }
	public void SetRotation(Quaternion rot){
		transform.rotation = rot;
		RotX = transform.localEulerAngles.y;
	}
	
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.End)) {
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
        }
		if (Input.GetKeyDown (KeyCode.H)) {
			transform.Rotate (0, 180f, 0);
			RotX = transform.localEulerAngles.y; 
		}
    }

    private void FixedUpdate() {

		//Rotates the player upright when stepping out of a portal in an angle etc.
		if (transform.localRotation.x != defaultRotation) {
			transform.rotation = Quaternion.Slerp (Quaternion.Euler (transform.localEulerAngles), Quaternion.Euler (defaultRotation, 
				transform.localEulerAngles.y, defaultRotation), Time.time * rotationSmooth);
		}


		//The most important setting
		if (mInvert) {
			mouseSensitivityY = mouseSensitivityY * -1;
		}

		//Getting input
		float MoveX = Input.GetAxis ("Horizontal");
		float MoveZ = Input.GetAxis ("Vertical");


		RotX += Input.GetAxis ("Mouse X") * mouseSensitivity;

		RotY += Input.GetAxis ("Mouse Y") * mouseSensitivityY;
		RotY = Mathf.Clamp (RotY, minY, maxY);

		Vector3 oldT = cam.transform.localEulerAngles;
		cam.transform.localEulerAngles = new Vector3 (-RotY, oldT.y);

		transform.localEulerAngles = new Vector3 (transform.localEulerAngles.x, 
			RotX, 
			transform.localEulerAngles.z);

		var aa = (transform.right * MoveX + transform.forward * MoveZ).normalized * movePower * Time.deltaTime;

		rb.velocity = new Vector3 (aa.x, rb.velocity.y, aa.z);

	}
}
