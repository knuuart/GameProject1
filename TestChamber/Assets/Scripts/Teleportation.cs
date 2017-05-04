using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Teleportation : MonoBehaviour {

    //Figure out how to preserve momentum you dumdum
    //stopping continous back and forth teleporting should be in other gameobjects/scripts so cubes/player won't block eachother from porting

    public GameObject otherPortal, portal;
    GameObject go;
    bool usedPortal = false;
    float time, allowPort = 0.2f;
    Vector3 velocity;
    float portalAnglesY;
    int rotationAngle;

    private void OnTriggerEnter(Collider other) {
        go = other.gameObject;
        //velocity = go.GetComponent<Rigidbody>().velocity;
        if (usedPortal == false && otherPortal.GetComponent<Teleportation>().usedPortal == false) {
            go.transform.Rotate(0f, rotationAngle, 0f);
            go.transform.position = otherPortal.transform.position;
            
            usedPortal = true;

        }
        
    }

    private void OnTriggerStay(Collider other) {

    }

    void Start () {
        portal.GetComponent<Transform>();
        otherPortal.GetComponent<Transform>();
	}
    private void OnTriggerExit(Collider other) {
        
    }

    void Update () {
        ResetPortal();
        portalAnglesY = portal.transform.rotation.y - otherPortal.transform.rotation.y;
        if (portalAnglesY == 1f || portalAnglesY == -1f) {
            rotationAngle = 0;
        } else if (portalAnglesY < -0.1f) {
            rotationAngle = -90;
        } else if (portalAnglesY > 0.1f) {
            rotationAngle = 90;
        } else if (portalAnglesY == 0) {
            rotationAngle = 180;
        }
        
        
        print(portalAnglesY);
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
