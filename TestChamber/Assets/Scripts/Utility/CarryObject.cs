using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryObject : MonoBehaviour {
    GameObject carriedObject;
    public static bool carrying;
    public float distance, maxDistance = 3.5f, throwForce = 7f, moveForce = 20f;
    
	void Start () {
	}
	void Update () {
        PickupAndCarry();
    }
    IEnumerator ThrowObject() {
        yield return new WaitForSeconds(0.001f);
        carriedObject.GetComponent<Rigidbody>().useGravity = true;
        carriedObject.GetComponent<Rigidbody>().freezeRotation = false;
        Physics.IgnoreCollision(GetComponent<Collider>(), carriedObject.GetComponent<Collider>(), false);
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
        if (carrying) {
            Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * distance;
            carriedObject.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
            carriedObject.GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(carriedObject.transform.position, targetPosition, Time.fixedDeltaTime * moveForce)); // moveForce about 20f
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
