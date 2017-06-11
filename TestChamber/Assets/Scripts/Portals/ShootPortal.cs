using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootPortal : MonoBehaviour {

    public GameObject orangePortal, bluePortal;
	public GameObject behindBlue, behindOrange;
    TeleportationV2 tp;
    public float minDistance, yMin = 1.6f, xMin = 0.9f;
    public LayerMask passRaycast, ignoreRaycast;
    escapeMenu escMenu;
    [Header("Sounds for shooting")]
    public UnityEvent pinkPortalShoot;
    public UnityEvent greenPortalShoot;
    public UnityEvent portalAppear;

    // Use this for initialization
    void Awake () {
        escMenu = GameObject.Find("Escape Menu Handler").GetComponent<escapeMenu>();
        tp = GetComponent<TeleportationV2>();
        passRaycast = passRaycast | ignoreRaycast;
        passRaycast = ~passRaycast;
	}

    // Update is called once per frame
    void Update() {
        if (!escMenu.menusOpen) {
            if (Input.GetButtonDown("ShootGreenPortal") && !CarryObject.carrying) {
                CreatePortal(bluePortal);
                greenPortalShoot.Invoke();
            }
            if (Input.GetButtonDown("ShootPinkPortal") && !CarryObject.carrying) {
                CreatePortal(orangePortal);
                pinkPortalShoot.Invoke();
            }
            if (behindBlue != null && behindOrange != null) {
                orangePortal.GetComponentInChildren<MeshRenderer>().enabled = true;
                bluePortal.GetComponentInChildren<MeshRenderer>().enabled = true;
				GameObject[] colorMesh = GameObject.FindGameObjectsWithTag ("ColorMesh");
				foreach (GameObject mesh in colorMesh) {
					mesh.SetActive (false);
				}
            }
        }
    }
    public void ResetCollision(Collider objectCollider, Collider ignoredCollider) {
        Physics.IgnoreCollision(objectCollider, ignoredCollider, false);
    }

    Vector3 PortalPosition(RaycastHit hit, GameObject portal, GameObject otherPortal) {
        Vector3 localHitPoint = hit.transform.InverseTransformPoint(hit.point);
        Vector3 lossyScale = hit.transform.lossyScale;
		float yDifference = 0, xDifference = 0;
        localHitPoint.x *= lossyScale.x;
        localHitPoint.y *= lossyScale.y;
        //        localHitPoint.z *= lossyScale.z;
        //print("localhitX:" + Mathf.Abs(localHitPoint.x));
        //print("localhitY:" + Mathf.Abs(localHitPoint.y));
        float yMax = 0.5f * lossyScale.y;
        float xMax = 0.5f * lossyScale.x;
        if (Mathf.Abs(localHitPoint.y) > (yMax - yMin)) {
            yDifference = (yMax - yMin) - (Mathf.Abs(localHitPoint.y));
        }
        if (Mathf.Abs(localHitPoint.x) > (xMax - xMin)) {
            xDifference = (xMax - xMin) - (Mathf.Abs(localHitPoint.x));
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

        Vector3 portalOffset = otherPortal.transform.position - newGlobal;
        if (hit.normal == otherPortal.transform.forward && portalOffset.magnitude < 3.2f) {
            Vector3 finalGlobal = PortalFinal(newGlobal, portal, otherPortal);
            //return finalGlobal;
            Vector3 finalLocal = hit.transform.InverseTransformPoint(finalGlobal);
            finalLocal.x *= lossyScale.x;
            finalLocal.y *= lossyScale.y;
            if (Mathf.Abs(finalLocal.x) > (xMax - xMin * 0.8f) || Mathf.Abs(finalLocal.y) > (yMax - yMin * 0.8f)) {
                return Vector3.zero;
            } else {
                return finalGlobal;
            }

        } else {
            return newGlobal;
        }


        //return newGlobal;
    }

    Vector3 PortalFinal(Vector3 portalPos, GameObject portal, GameObject otherPortal){
        Vector3 offset = otherPortal.transform.position - portalPos;
        Vector3 otherOffset = otherPortal.transform.InverseTransformVector(offset);
        Vector3 newLocal = otherOffset;
        if (otherOffset.magnitude < 3.2f) {
            newLocal.z *= -1;
            if (Mathf.Abs(otherOffset.y) > 1.6f) {
                newLocal.y = 3.2f;
                newLocal.x = otherOffset.x * -1;
                if (otherOffset.y > 0) {
                    newLocal.y *= -1;
                }
            } else if (Mathf.Abs(otherOffset.x) < 1.8f) {
                newLocal.x = 1.8f;
                newLocal.y = otherOffset.y * -1;
                if (otherOffset.x > 0) {
                    newLocal.x *= -1;
                }
            } else {
                newLocal.x *= -1;
                newLocal.y *= -1;
            }
            Vector3 newGlobal = otherPortal.transform.TransformPoint(newLocal);
            return newGlobal;
        } else {
            return portalPos;
        }
        
    }

    public bool CreatePortal(GameObject portal) {
        int x = Screen.width / 2;
        int y = Screen.height / 2;
        
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200f, passRaycast)) {//(Physics.Raycast(ray, out hit) && !hit.rigidbody) {
            if (hit.collider.tag == "NoPortal") {
                return false;
            }
            //print(hit.collider.gameObject);
            var otherPortal = portal == orangePortal ? bluePortal : orangePortal;

            if (PortalPosition(hit, portal, otherPortal) == Vector3.zero) {
                return false;
            } else {
                portal.transform.position = PortalPosition(hit, portal, otherPortal);
                //HERE
                portalAppear.Invoke();
            }
            //portal.transform.position = PortalPosition(hit, portal, otherPortal);

            //var portalDistance = Vector3.Distance(hit.point, otherPortal.transform.position);

            //    //print("Hello");

            //if (portalDistance < minDistance) { return false; }

            //print("Hello Again");
            //portal.SetActive(true);
            //portal.transform.position = PortalPosition(hit);


            // Alla pois kommentoituna/testissä koodi joka yrittää estää portaaleja menemästä päällekkäin, huonosti kirjoitettu ja toiminta jotakin sinnepäin
            //if(hit.normal == otherPortal.transform.forward){
            //	Vector3 hitPoint = hit.point;
            //	Vector3 offset = otherPortal.transform.position - hitPoint; // portal.transform.position;
            //	Vector3 otherOffset = otherPortal.transform.InverseTransformVector(offset);
            //	Vector3 newLocal = otherOffset;
            //	if (otherOffset.magnitude < 3.2f) {
            //		newLocal.z *= -1;
            //		if (Mathf.Abs(otherOffset.y) > 1.6f) {
            //			newLocal.y = 3.2f;
            //			newLocal.x = otherOffset.x * -1;
            //			if (otherOffset.y > 0) {
            //				newLocal.y *= -1;
            //			}
            //		} else if (Mathf.Abs(otherOffset.x) < 1.8f) {
            //			newLocal.x = 1.8f;
            //			newLocal.y = otherOffset.y * -1;
            //			if (otherOffset.x > 0) {
            //				newLocal.x *= -1;
            //			}
            //		} else {
            //			newLocal.x *= -1;
            //			newLocal.y *= -1;
            //		}

            //		Vector3 newGlobal = otherPortal.transform.TransformPoint(newLocal);
            //		print(otherOffset);
            //		portal.transform.position = newGlobal;
            //	} 
            //} else {
            //	portal.transform.position = PortalPosition(hit);
            //}



            if (Mathf.Abs (hit.normal.y) < 0.85f) {
				portal.transform.rotation = Quaternion.LookRotation (hit.normal, Vector3.up);
			} else {
				portal.transform.rotation = Quaternion.LookRotation (hit.normal, Vector3.ProjectOnPlane(ray.direction, hit.normal)); 
			}
            if (portal == bluePortal) {
				if (tp.ignoredPortal1 != null) {
					ResetCollision(tp.objectCollider, tp.ignoredPortal1);
                }
                behindBlue = hit.collider.gameObject;
            }
            if (portal == orangePortal) {
				if(tp.ignoredPortal1 != null) {
					ResetCollision(tp.objectCollider, tp.ignoredPortal1);
                }
                behindOrange = hit.collider.gameObject;
            }
            return true;
        } else { return false; }
    }
    
}
