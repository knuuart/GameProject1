using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour {
    public GameObject playerCamera, portal, otherPortal;
    public bool printteri;
	// Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update () {
        // CopyPaste coding didn't work WutFace
        var portalPos = portal.transform.position;
        var otherPortalPos = otherPortal.transform.position;
        var playerCameraPos = playerCamera.transform.position;

        var playeroffsetfromportal = portalPos - playerCameraPos;
        transform.position = otherPortalPos + playeroffsetfromportal;

        //var playerOffsetFromOtherPortal = otherPortalPos + playerCameraPos;
        //transform.position = portalPos + playerOffsetFromOtherPortal * -1;


        //if (printteri) {
        //    print(playerOffsetFromPortal);
        //}
        //var angleDifferenceBetweenPortalRotations = Quaternion.Angle(portal.transform.rotation, otherPortal.transform.rotation);
        //var portalRotationalDifference = Quaternion.AngleAxis(angleDifferenceBetweenPortalRotations, Vector3.up);
        //var newFacing = portalRotationalDifference * playerCamera.transform.forward;
        //transform.rotation = Quaternion.LookRotation(newFacing, Vector3.up);
    }
}
