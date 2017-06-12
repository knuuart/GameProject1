using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : MonoBehaviour {

    private void OnTriggerStay(Collider other) {
        other.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
