using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPortal : MonoBehaviour {

    public GameObject orangePortal, bluePortal;
	public GameObject behindBlue, behindOrange;
    TeleportationV2 tp;
    public float minDistance;

    // Use this for initialization
    void Awake () {
        tp = GetComponent<TeleportationV2>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("ShootGreenPortal")) {
            CreatePortal(bluePortal);
        }
        if (Input.GetButtonDown("ShootPinkPortal")) {
            CreatePortal(orangePortal);
        }
        if(behindBlue != null && behindOrange != null) {
            orangePortal.GetComponentInChildren<MeshRenderer>().enabled = true;
            bluePortal.GetComponentInChildren<MeshRenderer>().enabled = true;

        }
    }
    public void ResetCollision(Collider objectCollider, Collider ignoredCollider) {
        Physics.IgnoreCollision(objectCollider, ignoredCollider, false);
    }

    public bool CreatePortal(GameObject portal) {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit) && !hit.rigidbody) {
            var otherPortal = portal == orangePortal ? bluePortal : orangePortal;
            var portalDistance = Vector3.Distance(hit.point, otherPortal.transform.position);

            print("Hello");

            if (portalDistance < minDistance) { return false; }

            print("Hello Again");

            portal.transform.position = hit.point;

			if (Mathf.Abs (hit.normal.y) < 0.85f) {
				portal.transform.rotation = Quaternion.LookRotation (hit.normal, Vector3.up);
			} else {
				portal.transform.rotation = Quaternion.LookRotation (hit.normal, Vector3.ProjectOnPlane(ray.direction, hit.normal)); 
			}
            if (portal == bluePortal) {
                if (tp.ignoredBlueCollider != null) {
                    ResetCollision(tp.objectCollider, tp.ignoredBlueCollider);
                    ResetCollision(tp.objectCollider, tp.currentBlue);
                }
                behindBlue = hit.collider.gameObject;
            }
            if (portal == orangePortal) {
                if(tp.ignoredOrangeCollider != null) {
                    ResetCollision(tp.objectCollider, tp.ignoredOrangeCollider);
                    ResetCollision(tp.objectCollider, tp.currentOrange);
                }
                behindOrange = hit.collider.gameObject;
            }
            //portal.SetActive(true);
            return true;
        } else { return false; }
    }
}
