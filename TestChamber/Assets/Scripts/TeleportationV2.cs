using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationV2 : MonoBehaviour {

	public Transform bluePortal, orangePortal;	

    private void OnTriggerEnter(Collider other) {
        	
//		if (other.tag == "BluePortal") {
//			transform.position = orangePortal.transform.position;
//		}
//		if (other.tag == "OrangePortal") {
//			transform.position = bluePortal.transform.position;
//		}
		Physics.IgnoreLayerCollision(9, 8);
    }

    private void OnTriggerStay(Collider other) {

    }

    void Start () {
        
	}
    private void OnTriggerExit(Collider other) {

		Physics.IgnoreLayerCollision(8, 9, false);
        
    }

    void Update () {
		
	}
}
