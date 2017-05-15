using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationV2 : MonoBehaviour {

	public GameObject bluePortal, orangePortal;
    Quaternion blueNormal, orangeNormal;
    ShootPortal sp;
    bool hasPorted;
    Collider objectCollider, behindOrange, behindBlue;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "BluePortal" && hasPorted == false) {
            transform.position = orangePortal.transform.position;
            hasPorted = true;
            StartCoroutine("Reset");

        }
        if (other.tag == "OrangePortal" && hasPorted == false) {
            transform.position = bluePortal.transform.position;
            hasPorted = true;
            StartCoroutine("Reset");
        }
        //if (other.tag == "BlueMesh") {
        //    Physics.IgnoreCollision(objectCollider, sp.behindBlue);
        //}
        //if (other.tag == "OrangeMesh") {
        //    Physics.IgnoreCollision(objectCollider, sp.behindOrange);
        //}
        //Physics.IgnoreCollision(objectCollider, sp.behindBlue.GetComponent<Collider>());
        //Physics.IgnoreCollision(objectCollider, sp.behindOrange.GetComponent<Collider>());


        Physics.IgnoreLayerCollision(9, 8);
    }

    private void OnTriggerStay(Collider other) {
    }

    void Start () {
        objectCollider = gameObject.GetComponent<Collider>();
        

    }
    private void OnTriggerExit(Collider other) {
        //if (other.tag == "BlueMesh") {
        //    Physics.IgnoreCollision(objectCollider, behindBlue, false);
        //}
        //if (other.tag == "OrangeMesh") {
        //    Physics.IgnoreCollision(objectCollider, behindOrange, false);
        //}

        Physics.IgnoreLayerCollision(8, 9, false);

    }

    void Update () {
        bluePortal = GameObject.FindGameObjectWithTag("Blue");
        orangePortal = GameObject.FindGameObjectWithTag("Orange");
    }
    IEnumerator Reset() {
        yield return new WaitForSeconds(0.1f);
        hasPorted = false;
    }
}
