using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneObjects : MonoBehaviour {
	public GameObject portal1, portal2, clonedObject;
	public GameObject cube;
	// Use this for initialization
	void OnTriggerEnter(Collider other){
		if (other.tag == "PickupAble") {
			Matrix4x4 targetFlipRotation = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(180.0f, Vector3.up), Vector3.one);
			Matrix4x4 inversionMatrix = targetFlipRotation * portal1.transform.worldToLocalMatrix;
			Quaternion newRotation = Portal.QuaternionFromMatrix(inversionMatrix) * other.transform.rotation;
			if (clonedObject == null) {
				Vector3 newPos = CheckPosition (portal1, portal2, other.gameObject);
				clonedObject = Instantiate (cube, newPos, portal2.transform.rotation * newRotation);
                Physics.IgnoreCollision(other, clonedObject.GetComponent<Collider>());
			}
		}

	}
	void OnTriggerStay(Collider other){
        if (other.tag == "PickupAble") {
            Matrix4x4 targetFlipRotation = Matrix4x4.TRS(Vector3.zero, Quaternion.AngleAxis(180.0f, Vector3.up), Vector3.one);
            Matrix4x4 inversionMatrix = targetFlipRotation * portal1.transform.worldToLocalMatrix;
            Quaternion newRotation = Portal.QuaternionFromMatrix(inversionMatrix) * other.transform.rotation;
            if (clonedObject != null) {
                clonedObject.transform.position = CheckPosition(portal1, portal2, other.gameObject);
                clonedObject.transform.rotation = portal2.transform.rotation * newRotation;
            }
        }
	}

	void OnTriggerExit(Collider other){
        if (other.tag == "PickupAble") {
            if (clonedObject != null) {
                Destroy(clonedObject);
            }
        }
            
	}
	Vector3 CheckPosition(GameObject portal1, GameObject portal2, GameObject other) {
		Vector3 local = portal1.transform.InverseTransformPoint(other.transform.position);
		local.z *= -1;
		local.x *= -1;
		Vector3 portal1Global = portal2.transform.TransformPoint(local);
		Vector3 newTarget = portal1Global;
		return newTarget;
	}
}
