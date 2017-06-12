using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryObject : MonoBehaviour {
    GameObject bluePortal, orangePortal;
    GameObject carriedObject;
    public static bool carrying;
    public float distance, maxDistance = 3.5f, throwForce = 7f, moveForce = 20f, sphereRadius = 0.2f;
    TeleportationV2 tp;
    Transform targetTransform;
    
	void Start () {
        tp = GetComponent<TeleportationV2>();
        targetTransform = GameObject.Find("TargetPosition").transform;
        bluePortal = GameObject.FindGameObjectWithTag("BluePortal");
        orangePortal = GameObject.FindGameObjectWithTag("OrangePortal");
    }
    void LateUpdate () {
        PickupAndCarry();
        //bluePortal = GameObject.FindGameObjectWithTag("BluePortal");
        //orangePortal = GameObject.FindGameObjectWithTag("OrangePortal");
    }

    IEnumerator ThrowObject() {
        yield return new WaitForSeconds(0.001f);
        carriedObject.GetComponent<Rigidbody>().useGravity = true;
        carriedObject.GetComponent<Rigidbody>().freezeRotation = false;
        Physics.IgnoreCollision(GetComponent<Collider>(), carriedObject.GetComponent<Collider>(), false);
		if (Vector3.Distance (carriedObject.transform.position, targetTransform.position) > 0.5f) {
			Vector3 newVelocity = GetComponent<TeleportationV2> ().exitVelocity;
			carriedObject.GetComponent<Rigidbody> ().velocity = newVelocity;
		} else {
			carriedObject.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;		
		}

        carriedObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);

        carrying = false;
    }
    public void DropCube() {
        carriedObject.GetComponent<Rigidbody>().useGravity = true;
        carriedObject.GetComponent<Rigidbody>().freezeRotation = false;
        Physics.IgnoreCollision(GetComponent<Collider>(), carriedObject.GetComponent<Collider>(), false);
        carrying = false;
    }

    Vector3 CheckPosition(GameObject portal1, GameObject portal2) {
        Vector3 local = portal1.transform.InverseTransformPoint(targetTransform.position);
        local.z *= -1;
        local.x *= -1;
        Vector3 portal1Global = portal2.transform.TransformPoint(local);
        Vector3 newTarget = portal1Global;
        return newTarget;
    }

    void PickupAndCarry() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if(carrying == false) {
                int x = Screen.width / 2;
                int y = Screen.height / 2;
                
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 3f)) {
                    if (hit.collider.tag == "PickupAble") {
                        carriedObject = hit.collider.gameObject;
                        carrying = true;
                    } else {
                        if (Vector3.Distance(targetTransform.position, bluePortal.transform.position) < Vector3.Distance(targetTransform.position, orangePortal.transform.position)) {
                            Collider[] blueHitColliders = Physics.OverlapSphere(CheckPosition(bluePortal, orangePortal), sphereRadius);
                            foreach (Collider c in blueHitColliders) {
                                if (c.gameObject.tag == "PickupAble" && Vector3.Distance(c.gameObject.transform.position, orangePortal.transform.position) < maxDistance) {
                                    carriedObject = c.gameObject;
                                    carrying = true;
                                }
                            }
                        }
                        if (Vector3.Distance(targetTransform.position, orangePortal.transform.position) < Vector3.Distance(targetTransform.position, bluePortal.transform.position)) {
                            Collider[] orangeHitColliders = Physics.OverlapSphere(CheckPosition(orangePortal, bluePortal), sphereRadius);
                            foreach(Collider c in orangeHitColliders) {
                                if(c.gameObject.tag == "PickupAble" && Vector3.Distance(c.gameObject.transform.position, bluePortal.transform.position) < maxDistance) {
                                    carriedObject = c.gameObject;
                                    carrying = true;
                                }
                            }
                        }
                    }

                } 
            } else {
                carriedObject.GetComponent<Rigidbody>().useGravity = true;
                carriedObject.GetComponent<Rigidbody>().freezeRotation = false;
                Physics.IgnoreCollision(GetComponent<Collider>(), carriedObject.GetComponent<Collider>(), false);
				if (Vector3.Distance (carriedObject.transform.position, targetTransform.position) > 0.5f) {
					carriedObject.GetComponent<Rigidbody> ().velocity = GetComponent<TeleportationV2>().exitVelocity;
				} else {
					carriedObject.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;		
				}

                carrying = false;
            }
        }
        if (carrying) {
            CubeTeleport ct = carriedObject.GetComponent<CubeTeleport>();
            //Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * distance;
            Vector3 targetPosition = ct.targetPosition;
            //carriedObject.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
            carriedObject.GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(carriedObject.transform.position, targetPosition, Time.deltaTime * ct.moveForce)); // moveForce about 20f
            //carriedObject.transform.position = Vector3.MoveTowards(carriedObject.transform.position, targetPosition, Time.fixedDeltaTime * moveForce);

            carriedObject.GetComponent<Rigidbody>().freezeRotation = true;
            carriedObject.GetComponent<Rigidbody>().useGravity = false;
            Physics.IgnoreCollision(GetComponent<Collider>(), carriedObject.GetComponent<Collider>());
            if (Input.GetMouseButtonDown(0)) { //|| Vector3.Distance(transform.position, carriedObject.transform.position) > maxDistance) {
                StartCoroutine(ThrowObject());
            }
        }
    }
}
