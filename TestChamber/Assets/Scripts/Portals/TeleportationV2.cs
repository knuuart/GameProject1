using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationV2 : MonoBehaviour {

	public GameObject bluePortal, orangePortal;
    ShootPortal sp;
    bool hasPorted;
    public Collider objectCollider, ignoredBlueCollider, ignoredOrangeCollider, portalCollider;
	Rigidbody rb;
	ExtendedFlycam efc;
	playerBehaviourScript pbs;
    bool inBlueTrigger, inOrangeTrigger;
    Transform portal1, portal2;

    [Header("Velocity Values")]
    public Vector3 velocity;
    public float velocityMagnitude;

    
    private void OnTriggerStay(Collider other) {
        if (sp.behindBlue != null && sp.behindOrange != null) {

            if (other.tag == "BlueTrigger") {
                portalCollider = other.GetComponent<Collider>();
                //portal1 = bluePortal.transform;
                //portal2 = orangePortal.transform;
                inBlueTrigger = true; // poista boolit, laita triggerit antamaan arvot updatessa juoksevaan teleporttifunktioon?
                ignoredBlueCollider = sp.behindBlue.GetComponent<Collider>();
                Physics.IgnoreCollision(objectCollider, ignoredBlueCollider);
                //PortalCollision(other, bluePortal.transform, orangePortal.transform);

            }
            if (other.tag == "OrangeTrigger") {
                portalCollider = other.GetComponent<Collider>();
                //portal1 = orangePortal.transform;
                //portal2 = bluePortal.transform;
                inOrangeTrigger = true;
                ignoredOrangeCollider = sp.behindOrange.GetComponent<Collider>();
                Physics.IgnoreCollision(objectCollider, ignoredOrangeCollider);
                //PortalCollision(other, orangePortal.transform, bluePortal.transform);

            }
        }
        

    }

    void Start () {
        objectCollider = gameObject.GetComponent<Collider>();
		sp = GameObject.FindGameObjectWithTag("Player").GetComponent<ShootPortal> ();
        rb = GetComponent<Rigidbody>();
        efc = GetComponent<ExtendedFlycam> ();
		pbs = GetComponent<playerBehaviourScript> ();
    }
    private void OnTriggerExit(Collider other) {
        if (sp.behindBlue != null && sp.behindOrange != null) {
            if (other.tag == "BlueTrigger") {
                Physics.IgnoreCollision(objectCollider, ignoredBlueCollider, false);
                //if(sp.behindBlue.GetComponent<Collider>() != ignoredBlueCollider) {
                //    Physics.IgnoreCollision(objectCollider, ignoredBlueCollider, false);
                //}
                inBlueTrigger = false;
        	}
            if (other.tag == "OrangeTrigger") {
                Physics.IgnoreCollision(objectCollider, ignoredOrangeCollider, false);
                //if (sp.behindOrange.GetComponent<Collider>() != ignoredOrangeCollider) {
                //    Physics.IgnoreCollision(objectCollider, ignoredOrangeCollider, false);
                //}
                inOrangeTrigger = false;
            }
        }	

    }
    

    void Update () {

        velocity = rb.velocity;
        velocityMagnitude = rb.velocity.magnitude;

        if (Input.GetKey(KeyCode.Z)) {
            Time.timeScale = 0.1f;
        } else {
            Time.timeScale = 1;

        }
        bluePortal = GameObject.FindGameObjectWithTag("BluePortal");
        orangePortal = GameObject.FindGameObjectWithTag("OrangePortal");

        if (inBlueTrigger) { //&& (sp.behindBlue != null && sp.behindOrange != null)) {
            PortalCollision(portalCollider, bluePortal.transform, orangePortal.transform);
        }
        if (inOrangeTrigger) { //&& (sp.behindBlue != null && sp.behindOrange != null)) {
            PortalCollision(portalCollider, orangePortal.transform, bluePortal.transform);
        }
        //if(portalCollider != null && portal1 != null && portal2 != null) {
        //    PortalCollision(portalCollider, portal1, portal2);
        //}
    }

	void PortalCollision(Collider other, Transform portal1, Transform portal2){
        // Laskee etäisyyttä portaalista ja muuttaa sen vektorin portaalin koordinaatteihin
        Vector3 offset = transform.position - portal1.transform.position;
        Vector3 offsetInPortal1Coords = portal1.InverseTransformVector(offset);

        // Muuttaa velocityvektorin sisäänmeno portaalin koordinaatteihin ja muuttaa sen ulostulo-portaalin kautta maailman koordinaatteihin 
        Vector3 inPortal1Coords = portal1.InverseTransformVector(rb.velocity);
        inPortal1Coords.z *= -1;
        inPortal1Coords.x *= -1;
        Vector3 exitVelocity = portal2.TransformVector(inPortal1Coords);

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
			rb.velocity = exitVelocity;

            if (gameObject.tag == "Player") {
//				Quaternion newRotation = Portal.QuaternionFromMatrix(inversionMatrix) * efc.cameraOffset.rotation;
//				efc.cameraOffset.rotation = portal2.rotation * newRotation;

                Quaternion newRotation = Portal.QuaternionFromMatrix(inversionMatrix) * transform.rotation;
//                transform.rotation = portal2.rotation * newRotation;
				pbs.SetRotation(portal2.rotation * newRotation);
            } else {
                Quaternion newRotation = Portal.QuaternionFromMatrix(inversionMatrix) * transform.rotation;
                transform.rotation = portal2.rotation * newRotation;

            }

        }
	}
}
