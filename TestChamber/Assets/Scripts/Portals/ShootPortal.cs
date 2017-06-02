using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPortal : MonoBehaviour {

    public GameObject orangePortal, bluePortal;
	public GameObject behindBlue, behindOrange;
    TeleportationV2 tp;
    public float minDistance, yMin = 1.6f, xMin = 0.9f;
    float yDifference, xDifference;
    Vector3 newPos;

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

    Vector3 PortalPosition(RaycastHit hit) {
        var localHitPoint = hit.transform.InverseTransformPoint(hit.point);
        Vector3 lossyScale = hit.transform.lossyScale;
        localHitPoint.x *= lossyScale.x;
        localHitPoint.y *= lossyScale.y;
        //        localHitPoint.z *= lossyScale.z;
        //print("localhitX:" + Mathf.Abs(localHitPoint.x));
        //print("localhitY:" + Mathf.Abs(localHitPoint.y));
        float yMax = 0.5f * lossyScale.y;
        float xMax = 0.5f * lossyScale.x;
        if (Mathf.Abs(localHitPoint.y) > (yMax - yMin)) {
            yDifference = (yMax - yMin) - (Mathf.Abs(localHitPoint.y));
        } else {
            yDifference = 0;
        }
        if (Mathf.Abs(localHitPoint.x) > (xMax - xMin)) {
            xDifference = (xMax - xMin) - (Mathf.Abs(localHitPoint.x));
        } else {
            xDifference = 0;
        }
        Vector3 differences = new Vector3(xDifference, yDifference, 0);
        if (localHitPoint.x < 0) {
            differences.x *= -1;
        }
        if (localHitPoint.y < 0) {
            differences.y *= -1;
        }
        Vector3 newLocal = localHitPoint + differences;
        newLocal = new Vector3(newLocal.x / lossyScale.x, newLocal.y / lossyScale.y, newLocal.z);
        Vector3 newGlobal = hit.transform.TransformPoint(newLocal);
        return newGlobal;
    }

    public bool CreatePortal(GameObject portal) {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit) && !hit.rigidbody) {
            var otherPortal = portal == orangePortal ? bluePortal : orangePortal;
            var portalDistance = Vector3.Distance(hit.point, otherPortal.transform.position);

                //print("Hello");

            if (portalDistance < minDistance) { return false; }

            //print("Hello Again");
            portal.SetActive(true);
            portal.transform.position = PortalPosition(hit);

            // Alla pois kommentoituna koodi joka yrittää estää portaaleja menemästä päällekkäin, huonosti kirjoitettu ja toiminta jotakin sinnepäin
//            Vector3 hitPoint = hit.point;
//            Vector3 offset = otherPortal.transform.position - hitPoint; // portal.transform.position;
//            Vector3 otherOffset = otherPortal.transform.InverseTransformVector(offset);
//            Vector3 newLocal = otherOffset;
//            if (otherOffset.magnitude < 3.2f && (portal.transform.forward == otherPortal.transform.forward)) {
//				if (Mathf.Abs (otherOffset.x) < 0.8f) {
//					newLocal.y = otherOffset.y * -1;
//					newLocal.x = 1.8f;
//					if (otherOffset.x > 0) {
//						newLocal.x *= -1;
//					}
//				} else if (Mathf.Abs(otherOffset.x) < 1.2f) {
//					newLocal.y = 3.2f;
//					newLocal.x = otherOffset.x * -1;
//					if (otherOffset.y > 0) {
//						newLocal.y *= -1;
//					}
//				} else if (Mathf.Abs(otherOffset.y) < 1.9f) {
//					newLocal.x = 1.8f;
//					newLocal.y = otherOffset.y * -1;
//					if (otherOffset.x > 0) {
//						newLocal.x *= -1;
//					}
//				} else {
//					newLocal *= -1;
//				}
//                
//                Vector3 newGlobal = otherPortal.transform.TransformPoint(newLocal);
//                print(otherOffset);
//                portal.transform.position = newGlobal;
//            } else {
//                portal.transform.position = PortalPosition(hit);
//            }


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
            return true;
        } else { return false; }
    }
    
}
