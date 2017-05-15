using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationV2 : MonoBehaviour {

	public GameObject bluePortal, orangePortal;
    ShootPortal sp;
    bool hasPorted;
    Collider objectCollider;
	Rigidbody rb;

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
        if (other.tag == "BlueMesh") {
			Physics.IgnoreCollision(objectCollider, sp.behindBlue.GetComponent<Collider>());

        }
        if (other.tag == "OrangeMesh") {
			Physics.IgnoreCollision(objectCollider, sp.behindOrange.GetComponent<Collider>());
        }
    }

    private void OnTriggerStay(Collider other) {
		if (other.tag == "BlueMesh") {
			Vector3 surfacePoint = other.ClosestPoint (transform.position);
			Ray ray;
			RaycastHit hit;
			Physics.Raycast (transform.position, surfacePoint, out hit);
			print (hit.distance);
			Debug.DrawRay (transform.position, surfacePoint, Color.black);
		}
    }

    void Start () {
        objectCollider = gameObject.GetComponent<Collider>();
		sp = Camera.main.GetComponent<ShootPortal> ();
		rb = gameObject.GetComponent<Rigidbody> ();
        

    }
    private void OnTriggerExit(Collider other) {
        if (other.tag == "BlueMesh") {
			Physics.IgnoreCollision(objectCollider, sp.behindBlue.GetComponent<Collider>(), false);
        }
        if (other.tag == "OrangeMesh") {
			Physics.IgnoreCollision(objectCollider, sp.behindOrange.GetComponent<Collider>(), false);
        }

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
