using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPortal : MonoBehaviour {

    public GameObject orangePortal, bluePortal;
    public Quaternion orangeRotation, blueRotation;
    public GameObject behindBlue, behindOrange;
    public Transform playerCam;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            CreatePortal(bluePortal);
        }
        if (Input.GetMouseButtonDown(1)) {
            CreatePortal(orangePortal);
        }
    }

    public void CreatePortal(GameObject portal) {
		portal.SetActive(true);
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit) && !hit.rigidbody) {
			portal.transform.position = hit.point;

			if (Mathf.Abs (hit.normal.y) < 0.85f) {
				portal.transform.rotation = Quaternion.LookRotation (hit.normal, Vector3.up);
			} else {
				portal.transform.rotation = Quaternion.LookRotation (hit.normal, Vector3.ProjectOnPlane(ray.direction, hit.normal)); 
			}
            if (portal == bluePortal) {
                behindBlue = hit.collider.gameObject;
            }
            if (portal == orangePortal) {
                behindOrange = hit.collider.gameObject;
            }
        }
    }
}
