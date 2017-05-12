using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPortal : MonoBehaviour {

    public GameObject orangePortal, bluePortal;
    public Quaternion orangeRotation, blueRotation;
    public Collider behindBlue, behindOrange;

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
        if(Physics.Raycast(ray, out hit)) {
            Quaternion hitObjectRotation = Quaternion.LookRotation(hit.normal);
            portal.transform.position = hit.point;
            portal.transform.rotation = hitObjectRotation;
            //if (portal == bluePortal) {
            //    blueRotation = hitObjectRotation;
            //    behindBlue = hit.collider;

            //}
            //if (portal == orangePortal) {
            //    orangeRotation = hitObjectRotation;
            //    behindOrange = hit.collider;
            //}
        }
    }
}
