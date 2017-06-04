using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryObject : MonoBehaviour {
    GameObject carriedObject;
    public static bool carrying;
    public float distance, throwForce = 7f, correctionForce = 5f;
    
	void Start () {
	}
	void Update () {
        
        
        PickupAndCarry();
        if (carrying) {
            Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * distance;
            Vector3 pushForce = targetPosition - carriedObject.transform.position;
            //carriedObject.GetComponent<Rigidbody>().velocity = pushForce.normalized * carriedObject.GetComponent<Rigidbody>().velocity.magnitude;
            //carriedObject.GetComponent<Rigidbody>().AddForce(pushForce * correctionForce);
            //carriedObject.GetComponent<Rigidbody>().velocity *= Mathf.Min(1.0f, pushForce.magnitude / 2);
            carriedObject.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
            carriedObject.GetComponent<Rigidbody>().MovePosition(targetPosition);

            carriedObject.GetComponent<Rigidbody>().freezeRotation = true;
            carriedObject.GetComponent<Rigidbody>().useGravity = false;
            Physics.IgnoreCollision(GetComponent<Collider>(), carriedObject.GetComponent<Collider>());
            //carriedObject.transform.position = Vector3.Lerp(carriedObject.transform.position, targetPosition, Time.deltaTime * 25);
            //carriedObject.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(carriedObject.transform.position, targetPosition, Time.deltaTime * 50));
            //carriedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            if (Input.GetMouseButtonDown(0)) {
                StartCoroutine(ThrowObject());

            }
        }

    }
    IEnumerator ThrowObject() {
        yield return new WaitForSeconds(0.001f);
        carriedObject.GetComponent<Rigidbody>().useGravity = true;
        carriedObject.GetComponent<Rigidbody>().freezeRotation = false;
        Physics.IgnoreCollision(GetComponent<Collider>(), carriedObject.GetComponent<Collider>(), false);
        Vector3 projected = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);
        carriedObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);
        carrying = false;
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
                    }
                }
            } else {
                carriedObject.GetComponent<Rigidbody>().useGravity = true;
                carriedObject.GetComponent<Rigidbody>().freezeRotation = false;
                Physics.IgnoreCollision(GetComponent<Collider>(), carriedObject.GetComponent<Collider>(), false);
                carrying = false;
            }
        }
    }
}
