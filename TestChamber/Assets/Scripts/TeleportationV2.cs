using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationV2 : MonoBehaviour {

	public GameObject bluePortal, orangePortal;
    Quaternion blueNormal, orangeNormal;
    ShootPortal sp;
    bool hasPorted;

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
        //if(other.tag == "BlueMesh") {
        //    Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), sp.behindBlue);
        //}
        //if (other.tag == "OrangeMesh") {
        //    Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), sp.behindOrange);
        //}


        Physics.IgnoreLayerCollision(9, 8);
    }

    private void OnTriggerStay(Collider other) {
    }

    void Start () {
        
	}
    private void OnTriggerExit(Collider other) {
        //if (other.tag == "BlueMesh") {
        //    Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), sp.behindBlue, false);
        //}
        //if (other.tag == "OrangeMesh") {
        //    Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), sp.behindOrange, false);
        //}

        Physics.IgnoreLayerCollision(8, 9, false);

    }

    void Update () {
        //if(hasPorted == true) {
        //    StartCoroutine("Reset");
        //}
        //if (Input.GetKeyDown(KeyCode.F)) {
        //    Debug.DrawRay(gameObject.transform.position, sp.behindBlue.transform.position);

        //}
    }
    IEnumerator Reset() {
        yield return new WaitForSeconds(0.5f);
        hasPorted = false;
    }
}
