using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class CameraPosition : MonoBehaviour {
    public GameObject playerCamera, portal, otherPortal;
    Vector3 offset, startPosition, newPosition;
    bool parallel, facing, angled;



    // Try setting camera to LookAt() middle of portal, but move around slightly depending on playerposition

    
    void Start () {

    }
    
    void Update () {
        //CheckPortalAngles();

        //offset = otherPortal.transform.position - playerCamera.transform.position;
        //newPosition = new Vector3((startPosition.x + offset.x * -1f) , (startPosition.y + offset.y * -1f), startPosition.z);
        gameObject.transform.LookAt(otherPortal.transform.position);
        //transform.position = newPosition;



        // TÄMÄ NYT VISSIIN TOIMII KU PORTAALIT ON VASTAKKAIN, TÄYTYY MUUTTAA LASKUJA PORTAALIEN KULMISTA RIIPPUEN


    }
    //private void LateUpdate() {
    //    if (facing) {
    //        PortalsFacing();
    //    }
    //}
    void CheckPortalAngles() {
        if (portal.transform.rotation.y - otherPortal.transform.rotation.y == 180) {
            facing = true;
        } else if (portal.transform.rotation.y - otherPortal.transform.rotation.y == 90) {
            angled = true;
        } else if (portal.transform.rotation.y - otherPortal.transform.rotation.y == 0) {
            parallel = true;
        } else {
            facing = false;
            angled = false;
            parallel = false;
        }
    }
    void PortalsFacing() {
        offset = otherPortal.transform.position - playerCamera.transform.position;
        newPosition = new Vector3((startPosition.x + offset.x) * -1, (startPosition.y + offset.y) * -1, startPosition.z);
        gameObject.transform.LookAt(otherPortal.transform.position);
        transform.position = newPosition;
    }
}
