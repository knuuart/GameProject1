using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class BigButtonBehaviour : MonoBehaviour {

    [Header("Button Properties")]
    [Tooltip("Minimum mass of the other rigidbody required for actiavtion")]
    public float activationMass;

    [Space(10)]
    [Tooltip("When the button is pushed down")]
    public UnityEvent Activate;
    [Tooltip("When the button gets back up")]
    public UnityEvent DeActivate;

    void OnCollisionEnter(Collision coll) {
        if (coll.rigidbody.mass >= activationMass) {
            Activate.Invoke();
        }
    }

    private void OnCollisionExit(Collision coll) {
        if (coll.rigidbody.mass >= activationMass) {
            DeActivate.Invoke();
        }
    }
}
