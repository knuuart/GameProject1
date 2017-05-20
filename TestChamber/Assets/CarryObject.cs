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
        if (Input.GetKeyDown(KeyCode.F)) {
            PickupAndCarry();
        }
        if (carrying) {
            carriedObject.GetComponent<Rigidbody>().useGravity = false;
            carriedObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * distance;
            carriedObject.transform.LookAt(Camera.main.transform.position);
            if (Input.GetKeyDown(KeyCode.Mouse0) && (carriedObject != null)) {
                carrying = false;
                carriedObject.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
    void PickupAndCarry() {
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3f)) {
            if (hit.collider.tag == "Carryable") {
                carriedObject = hit.collider.gameObject;
                carrying = true;    
                print("wut");
            }
        }
    }
}
