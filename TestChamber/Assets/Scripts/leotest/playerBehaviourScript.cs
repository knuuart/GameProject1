using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviourScript : MonoBehaviour {

    public float movePower;
    public float moveMax;
    public float mouseSensitivity;
    private float mouseSensitivityY;
    public bool mInvert;
    public float maxAngle;
    public float minAngle;

    public Camera cam;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        mouseSensitivityY = mouseSensitivity;
        if (mInvert) {
            mouseSensitivityY = mouseSensitivityY * -1;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate() {
        float MoveX = Input.GetAxis("Horizontal");
        float MoveZ = Input.GetAxis("Vertical");

        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");

        var aa = (transform.right * MoveX +transform.forward * MoveZ).normalized * movePower * Time.deltaTime;

        //rb.AddForce(aa, ForceMode.VelocityChange);

        transform.Rotate(0, MouseX * mouseSensitivity, 0);
        cam.transform.Rotate(MouseY * mouseSensitivityY, 0, 0);

        rb.velocity = new Vector3(aa.x, rb.velocity.y, aa.z);

        //Vector3 movementDir = new Vector3(MoveX, 0, MoveY);
        //rb.velocity = rb.velocity + movementDir.normalized * movePower;
    }
}
