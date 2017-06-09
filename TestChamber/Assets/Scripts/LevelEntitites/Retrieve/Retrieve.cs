using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retrieve : MonoBehaviour {


    //This script will stop objects from falling into the void!
    Vector3 startPos;
    public float minimumY; 

    void Awake() {
        startPos = transform.position;
    }

    private void Update() {
        if (transform.position.y <= minimumY) {
            transform.position = startPos;
        }
    }
}
