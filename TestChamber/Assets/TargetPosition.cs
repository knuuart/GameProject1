using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPosition : MonoBehaviour {

	public GameObject bluePortal, orangePortal;

	private void OnTriggerStay(Collider other) {
//		if (sp.behindBlue != null && sp.behindOrange != null) {
			if (other.tag == "BlueTrigger") {
				PortalCollision(bluePortal.transform, orangePortal.transform);
			}
			if (other.tag == "OrangeTrigger") {
				PortalCollision(orangePortal.transform, bluePortal.transform);

			}

//		}
	}

	void Start () {
	}
	private void OnTriggerExit(Collider other) {
	}


	void Update () {
		bluePortal = GameObject.FindGameObjectWithTag("BluePortal");
		orangePortal = GameObject.FindGameObjectWithTag("OrangePortal");
	}

	void PortalCollision(Transform portal1, Transform portal2){
		// Laskee etäisyyttä portaalista ja muuttaa sen vektorin portaalin koordinaatteihin
		Vector3 offset = transform.position - portal1.transform.position;
		Vector3 offsetInPortal1Coords = portal1.InverseTransformVector(offset);

		// Muutetaan nykyinen positio entrance portaalin locaaliin avaruuteen(inverseTransformPoint)
		// localspacepoint muutetaan worldspacepoint exit portaalin kautta(transformPoint)
		Vector3 local = portal1.InverseTransformPoint(transform.position);
		local = Quaternion.AngleAxis(180.0f, Vector3.up) * local;
		local.z = 0.019f;
		Vector3 newPos = portal2.TransformPoint(local);

		// Käännetään matriiseilla, voisi myös käyttää unityn omia funktioita(transform.transformDirection, transform.inverseTransformDirection)
		Matrix4x4 targetFlipRotation = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(180.0f, Vector3.up), Vector3.one);
		Matrix4x4 inversionMatrix = targetFlipRotation * portal1.worldToLocalMatrix;

		if (offsetInPortal1Coords.z < 0.018f) {
			transform.position = newPos;
			Quaternion newRotation = Portal.QuaternionFromMatrix(inversionMatrix) * transform.rotation;
			transform.rotation = portal2.rotation * newRotation;
		}
	}
}
