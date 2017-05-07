using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPortal : MonoBehaviour {

    public GameObject orangePortal, bluePortal;

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

    void CreatePortal(GameObject portal) {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)) {
            Quaternion hitObjectRotation = Quaternion.LookRotation(hit.normal * -1);
            portal.transform.position = hit.point; //- new Vector3(0.01f, 0.01f, 0.01f);
            portal.transform.rotation = hitObjectRotation;
        }
    }
}
