using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryObject : MonoBehaviour {
    GameObject carriedObject;
    public bool carrying;
    public float distance;
    
	void Start () {
	}
	void Update () {


        if (Input.GetKeyDown(KeyCode.F) && carrying == false) {
            PickupAndCarry();
        }
        if (carrying) {
            Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * distance;
            carriedObject.GetComponent<Rigidbody>().freezeRotation = true;
            carriedObject.GetComponent<Rigidbody>().useGravity = false;
            Physics.IgnoreCollision(GetComponent<Collider>(), carriedObject.GetComponent<Collider>());
			carriedObject.GetComponent<Rigidbody>().MovePosition(targetPosition);
            carriedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

            if (Input.GetKeyDown(KeyCode.G) && (carriedObject != null)) { // || newDirection.magnitude > 5f) {
                carrying = false;
                carriedObject.GetComponent<Rigidbody>().useGravity = true;
                carriedObject.GetComponent<Rigidbody>().freezeRotation = false;
                Physics.IgnoreCollision(GetComponent<Collider>(), carriedObject.GetComponent<Collider>(), false);
            }
        }
    }

    void PickupAndCarry() {
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
    }
}
