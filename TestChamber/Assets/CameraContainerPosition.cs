using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContainerPosition : MonoBehaviour {

    public GameObject playerCamera, portal, otherPortal, rotationTool;
    Vector3 offset, startPosition, newPosition;
    float kulma;
    float rotationX, rotationY, rotationZ;

    // Use this for initialization
    void Start () {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update () {
        offset = portal.transform.position - playerCamera.transform.position;
        newPosition = new Vector3((startPosition.x + offset.x * -1f), (startPosition.y + offset.y * -1f), startPosition.z);
        gameObject.transform.LookAt(otherPortal.transform.position);
        transform.position = newPosition;
        //rotationX = rotationTool.transform.rotation.x;
        //rotationY = rotationTool.transform.rotation.y;
        //rotationZ = rotationTool.transform.rotation.z;

        //rotationX *= -10;
        //rotationY *= -10;
        //rotationZ *= -10;

        //transform.eulerAngles = new Vector3(rotationX, rotationY, rotationZ);

        //transform.LookAt(playerCamera.transform.position);
        //kulma = Vector3.Angle(portal.transform.position, otherPortal.transform.position);
        //offset = otherPortal.transform.position - playerCamera.transform.position;
        ////transform.RotateAround(portal.transform.position, new Vector3(0, 1, 0),  kulma);
        //Debug.DrawLine(playerCamera.transform.position, otherPortal.transform.position);
        //print(kulma);
    }
}
