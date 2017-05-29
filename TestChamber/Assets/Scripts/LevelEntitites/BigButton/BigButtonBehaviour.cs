using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class BigButtonBehaviour : MonoBehaviour {

    UnityEvent Activate;
    public LayerMask buttonPresser;

    void OnCollisionStay(Collision coll) {
        if (coll.gameObject.layer == buttonPresser) ;
    }
}
