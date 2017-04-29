using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Teleportation : MonoBehaviour {

    //Figure out how to preserve momentum you dumdum
    //stopping continous back and forth teleporting should be in other gameobjects/scripts so cubes/player won't block eachother from porting

    public GameObject otherPortal;
    GameObject go;
    bool usedPortal = false;
    float time, allowPort = 0.2f;
    Vector3 velocity;

    private void OnTriggerEnter(Collider other) {
        go = other.gameObject;
        //velocity = go.GetComponent<Rigidbody>().velocity;
        if (usedPortal == false && otherPortal.GetComponent<Teleportation>().usedPortal == false) {
            go.transform.position = otherPortal.transform.position;
            //go.GetComponent<Rigidbody>().velocity += velocity / 4; 
            usedPortal = true;
        }
        
    }

    private void OnTriggerStay(Collider other) {

    }

    void Start () {

	}
    private void OnTriggerExit(Collider other) {

    }

    void Update () {
        ResetPortal();
	}
    void ResetPortal() {
        if (usedPortal || otherPortal.GetComponent<Teleportation>().usedPortal) {
            time += Time.deltaTime;
            if (time > allowPort) {
                usedPortal = false;
                time -= allowPort;
            }
        }
    }
}
