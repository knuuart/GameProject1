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
            //carriedObject.transform.position = Vector3.Lerp(carriedObject.transform.position, targetPosition, 10f);
            //carriedObject.transform.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
            carriedObject.GetComponent<Rigidbody>().useGravity = false;

            carriedObject.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(carriedObject.transform.position, targetPosition, 10f));
            //Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * distance;
            //Vector3 newDirection = targetPosition - carriedObject.transform.position;
            //carriedObject.GetComponent<Rigidbody>().freezeRotation = true;
            //carriedObject.transform.LookAt(Camera.main.transform.position);
            //carriedObject.transform.up = transform.up;

            carriedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

            carriedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;
            carriedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
            carriedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
            //carriedObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;


            if (Input.GetKeyDown(KeyCode.G) && (carriedObject != null)) { // || newDirection.magnitude > 5f) {
                carrying = false;
                carriedObject.GetComponent<Rigidbody>().useGravity = true;
                carriedObject.GetComponent<Rigidbody>().freezeRotation = false;
                carriedObject.transform.SetParent(null);


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
