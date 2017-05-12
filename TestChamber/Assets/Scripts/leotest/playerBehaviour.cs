using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviour : MonoBehaviour {

    public float movePower;
    public float sens;
    public float colSphereRadius;
    public LayerMask sphereCastLayer;
    float movX;
    float movY;
    float lookX;
    float lookY;
    public Rigidbody rb;
    public Vector3 spherePosition;
    Vector3 inputDirection;
    public Camera cam;
    float camHoriz;

    private void Update() {
        //Record input
        float movX = Input.GetAxisRaw("Horizontal");
        float movY = Input.GetAxisRaw("Vertical");
        float lookX = Input.GetAxisRaw("Mouse X");
        float lookY = Input.GetAxisRaw("Mouse Y");

        inputDirection = new Vector3(movX, 0, movY);

        print(cam.transform.forward);
        Debug.DrawRay(Vector3.zero, Vector3.zero - cam.transform.forward);
        float camHoriz = cam.transform.localRotation.x;
    }

    private void FixedUpdate() {
        rb.velocity = inputDirection.normalized * movePower;
        camHoriz = cam.transform.localRotation.x * sens * lookY;

        //AAA

        rb.velocity = rb.velocity + inputDirection.normalized * movePower * Time.fixedDeltaTime;

        Ray sphereCast;
        RaycastHit hitinfo;

        Physics.SphereCast(spherePosition, colSphereRadius, Vector3.zero, out hitinfo, Mathf.Infinity, sphereCastLayer);

	}
    
    void OnCollisionEnter(Collision coll) {
        rb.drag = 5;
    }

    void OnCollisionExit(Collision coll) {
        rb.drag = 0;
    }
}
