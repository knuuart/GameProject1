using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryObject : MonoBehaviour {
    GameObject carriedObject;
    public bool carrying;
    public float distance, throwForce = 7f, correctionForce = 5f;
    
	void Start () {
	}
	void Update () {


        if (Input.GetKeyDown(KeyCode.F) && carrying == false) {
            PickupAndCarry();
        }
        if (carrying) {

            // tätä muokkaamalla saattais toimia hyvin
            Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * distance;
            Vector3 pushForce = targetPosition - carriedObject.transform.position;
            carriedObject.GetComponent<Rigidbody>().velocity = pushForce.normalized * carriedObject.GetComponent<Rigidbody>().velocity.magnitude;
            carriedObject.GetComponent<Rigidbody>().AddForce(pushForce * correctionForce);
            carriedObject.GetComponent<Rigidbody>().velocity *= Mathf.Min(1.0f, pushForce.magnitude / 2);

            carriedObject.GetComponent<Rigidbody>().freezeRotation = true;
            carriedObject.GetComponent<Rigidbody>().useGravity = false;
            //Physics.IgnoreCollision(GetComponent<Collider>(), carriedObject.GetComponent<Collider>());
            //carriedObject.transform.position = Vector3.Lerp(carriedObject.transform.position, targetPosition, Time.deltaTime * 25);
            //carriedObject.GetComponent<Rigidbody>().MovePosition(Vector3.Lerp(carriedObject.transform.position, targetPosition, Time.deltaTime * 50));
            //carriedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

            if (Input.GetKeyDown(KeyCode.G) && (carriedObject != null)) { // || newDirection.magnitude > 5f) {
                carrying = false;
                carriedObject.GetComponent<Rigidbody>().useGravity = true;
                carriedObject.GetComponent<Rigidbody>().freezeRotation = false;
                //Physics.IgnoreCollision(GetComponent<Collider>(), carriedObject.GetComponent<Collider>(), false);
                carriedObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);
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
