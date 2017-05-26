using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationV2 : MonoBehaviour {

	public GameObject bluePortal, orangePortal;
    ShootPortal sp;
    bool hasPorted;
    public Collider objectCollider, ignoredBlueCollider, ignoredOrangeCollider, behindBlueCollider, behindOrangeCollider;
    public Collider currentBlue, currentOrange;
	Rigidbody rb;
	ExtendedFlycam efc;
	NewPlayerBehaviour npb;
    bool inBlueTrigger, inOrangeTrigger;
//    Transform portal1, portal2;
	Camera playerCam;
    public float minY = 4f;

    [Header("Velocity Values")]
    public Vector3 velocity;
    public float velocityMagnitude;

    private void OnTriggerStay(Collider other) {
        if (sp.behindBlue != null && sp.behindOrange != null) {
            ignoredBlueCollider = behindBlueCollider;
            ignoredOrangeCollider = behindOrangeCollider;
            //behindBlueCollider = sp.behindBlue.GetComponent<Collider>();
            //behindOrangeCollider = sp.behindOrange.GetComponent<Collider>();
            if (other.tag == "BlueTrigger") {
                //ignoredBlueCollider = sp.behindBlue.GetComponent<Collider>();
                //				Physics.IgnoreCollision(objectCollider, ignoredOrangeCollider);
                

                //Physics.IgnoreCollision(objectCollider, ignoredBlueCollider);

                if (ignoredBlueCollider != null) {
                    PortalCollision(bluePortal.transform, orangePortal.transform, ignoredBlueCollider, ignoredOrangeCollider);
                    //currentBlue = ignoredBlueCollider;
                }
                //inBlueTrigger = true; // poista boolit, laita triggerit antamaan arvot updatessa juoksevaan teleporttifunktioon?
                //PortalCollision(other, bluePortal.transform, orangePortal.transform);
            }
            if (other.tag == "OrangeTrigger") {
                //ignoredOrangeCollider = sp.behindOrange.GetComponent<Collider>();

                //				Physics.IgnoreCollision(objectCollider, ignoredBlueCollider);
                //ignoredOrangeCollider = behindOrangeCollider;
                //Physics.IgnoreCollision(objectCollider, ignoredOrangeCollider);
                //ignoredBlueCollider = behindBlueCollider;
                if (ignoredOrangeCollider != null) {
                    PortalCollision(orangePortal.transform, bluePortal.transform, ignoredOrangeCollider, ignoredBlueCollider);
                    //currentOrange = ignoredOrangeCollider;
                }
                //inOrangeTrigger = true;
                //PortalCollision(other, orangePortal.transform, bluePortal.transform);

            }
            
        }
    }

    void Start () {
        objectCollider = gameObject.GetComponent<Collider>();
		sp = GameObject.FindGameObjectWithTag("Player").GetComponent<ShootPortal> ();
        rb = GetComponent<Rigidbody>();
        efc = GetComponent<ExtendedFlycam> ();
		npb = GetComponent<NewPlayerBehaviour> ();
    }
    private void OnTriggerExit(Collider other) {
        if (sp.behindBlue != null && sp.behindOrange != null) {
            if (other.tag == "BlueTrigger") {
                //inBlueTrigger = false;
                Physics.IgnoreCollision(objectCollider, currentBlue, false);
                Physics.IgnoreCollision(objectCollider, ignoredBlueCollider, false);

                //Physics.IgnoreCollision(objectCollider, ignoredOrangeCollider, false);

                //if (behindBlueCollider != ignoredBlueCollider) {
                //    Physics.IgnoreCollision(objectCollider, ignoredBlueCollider, false);
                //}
            }
            if (other.tag == "OrangeTrigger") {
                //inOrangeTrigger = false;
                Physics.IgnoreCollision(objectCollider, currentOrange, false);
                Physics.IgnoreCollision(objectCollider, ignoredOrangeCollider, false);

                //Physics.IgnoreCollision(objectCollider, ignoredBlueCollider, false);

                //if (behindOrangeCollider != ignoredOrangeCollider) {
                //    Physics.IgnoreCollision(objectCollider, ignoredOrangeCollider, false);
                //}
            }
        }	

    }
    

    void Update () {
        if (sp.behindBlue != null && sp.behindOrange != null) {
            behindBlueCollider = sp.behindBlue.GetComponent<Collider>();
            behindOrangeCollider = sp.behindOrange.GetComponent<Collider>();
        }
        velocity = rb.velocity;
        velocityMagnitude = rb.velocity.magnitude;

        if (Input.GetKey(KeyCode.Z)) {
            Time.timeScale = 0.1f;
        } else {
            Time.timeScale = 1;

        }
        bluePortal = GameObject.FindGameObjectWithTag("BluePortal");
        orangePortal = GameObject.FindGameObjectWithTag("OrangePortal");
    }

    void PortalCollision(Transform portal1, Transform portal2, Collider behindPortal1, Collider behindPortal2){
        Physics.IgnoreCollision(objectCollider, behindPortal1);
        if(behindPortal1 == ignoredBlueCollider) {
            currentBlue = ignoredBlueCollider;
        }
        if(behindPortal1 == ignoredOrangeCollider) {
            currentOrange = ignoredOrangeCollider;
        }
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
            Physics.IgnoreCollision(objectCollider, behindPortal2);
            if (currentBlue != currentOrange) {
                Physics.IgnoreCollision(objectCollider, behindPortal1, false);
            }

            if (exitVelocity.y < minY && (portal2.forward == Vector3.up)) {
                exitVelocity = exitVelocity + new Vector3(0, minY - exitVelocity.y, 0);
            }
            transform.position = newPos;
            rb.velocity = exitVelocity;
            //print("newpos:" + newPos);
            //print("offset: " + offsetInPortal1Coords.z);

            if (gameObject.tag == "Player") {
                    //				Quaternion newRotation = Portal.QuaternionFromMatrix(inversionMatrix) * efc.cameraOffset.rotation;
                    //				efc.cameraOffset.rotation = portal2.rotation * newRotation;

                Quaternion newRotation = Portal.QuaternionFromMatrix(inversionMatrix) * npb.cam.transform.rotation;
                npb.cam.transform.rotation = portal2.rotation * newRotation;
            } else {
                Quaternion newRotation = Portal.QuaternionFromMatrix(inversionMatrix) * transform.rotation;
                transform.rotation = portal2.rotation * newRotation;
            }
        }
	}
}
