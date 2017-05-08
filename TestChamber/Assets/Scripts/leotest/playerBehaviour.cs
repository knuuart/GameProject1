using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehaviour : MonoBehaviour {

    public float movePower;
    float movX;
    float movY;
    float lookX;
    float lookY;
    public Rigidbody rb;
    Vector3 inputDirection;
    public Camera cam; 

    private void Update() {
        //Record input
        float movX = Input.GetAxisRaw("Horizontal");
        float movY = Input.GetAxisRaw("Vertical");
        float lookX = Input.GetAxisRaw("Mouse X");
        float lookY = Input.GetAxisRaw("Mouse Y");

        inputDirection = new Vector3(movX, 0, movY);

    }

    private void FixedUpdate() {

    }

    
}
