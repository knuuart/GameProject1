using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationV2 : MonoBehaviour {

	public GameObject bluePortal, orangePortal;
    ShootPortal sp;
    bool hasPorted;
    Collider objectCollider;
	Rigidbody rb;
	public Transform blueExit, orangeExit;
	ExtendedFlycam efc;

    private void OnTriggerEnter(Collider other) {
//        if (other.tag == "BluePortal" && hasPorted == false) {
//            transform.position = orangePortal.transform.position;
//            hasPorted = true;
//            StartCoroutine("Reset");
//
//        }
//        if (other.tag == "OrangePortal" && hasPorted == false) {
//            transform.position = bluePortal.transform.position;
//            hasPorted = true;
//            StartCoroutine("Reset");
//        }
        if (other.tag == "BlueMesh") {
			Physics.IgnoreCollision(objectCollider, sp.behindBlue.GetComponent<Collider>());

        }
        if (other.tag == "OrangeMesh") {
			Physics.IgnoreCollision(objectCollider, sp.behindOrange.GetComponent<Collider>());
        }
    }

    private void OnTriggerStay(Collider other) {

		if (other.tag == "BlueMesh") {
			PortalCollision (other, bluePortal.transform, orangePortal.transform);
		}
		if (other.tag == "OrangeMesh") {
			PortalCollision (other, orangePortal.transform, bluePortal.transform);
		}
    }

    void Start () {
        objectCollider = gameObject.GetComponent<Collider>();
		sp = GetComponent<ShootPortal> ();
		rb = gameObject.GetComponent<Rigidbody> ();
		efc = GetComponent<ExtendedFlycam> ();

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
	void PortalCollision(Collider other, Transform portal1, Transform portal2){
		Vector3 surfacePoint = other.ClosestPoint (transform.position);
		float distanceFromSurface = Vector3.Distance(surfacePoint, transform.position);


		if (distanceFromSurface <= 0.05f) {
			// Muutetaan nykyinen positio sinisen portaalin locaaliin avaruuteen(inverseTransformPoint)
			//localspacepoint muutetaan worldspacepoint oranssin portaalin kautta(transformPoint)
			Vector3 local = portal1.InverseTransformPoint(transform.position);
			local = Quaternion.AngleAxis (180.0f, Vector3.up) * local;
			local.z = 0.5f;
			Vector3 newPos = portal2.TransformPoint(local);
			transform.position = newPos;


			//Käännetään matriiseilla, voisi myös käyttää unityn omia funktioita(transform.transformDirection, transform.inverseTransformDirection)
			Matrix4x4 targetFlipRotation = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(180.0f, Vector3.up), Vector3.one);
			Matrix4x4 inversionMatrix = targetFlipRotation * portal1.worldToLocalMatrix;

			Quaternion newRotation = Portal.QuaternionFromMatrix(inversionMatrix) * efc.cameraOffset.rotation;
			efc.cameraOffset.rotation = portal2.rotation * newRotation;
		}
	}
}
