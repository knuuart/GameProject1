using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class BigButtonBehaviour : MonoBehaviour {

    [Header("Button Properties")]
    [Space(10)]
    [Tooltip("Minimum mass of the other rigidbody required for actiavtion")]
    public float activationMass;

    [Space(10)]
    [Tooltip("When the button is pushed down")]
    public UnityEvent Activate;
    [Space(10)]
    [Tooltip("When the button gets back up")]
    public UnityEvent DeActivate;


    private float weightOnButton;
    private List<GameObject> objectsOnButton = new List<GameObject>();

    void OnCollisionEnter(Collision coll) {
        if (coll.gameObject.GetComponent<Rigidbody>() != null) {
            var rb = coll.gameObject.GetComponent<Rigidbody>();
            weightOnButton += rb.mass;
            ButtonStateCheck();
        }
    }

    private void OnCollisionExit(Collision coll) {
        if (coll.gameObject.GetComponent<Rigidbody>() != null) {
            var rb = coll.gameObject.GetComponent<Rigidbody>();
            weightOnButton -= rb.mass;
            ButtonStateCheck();
        }
    }

    private void ButtonStateCheck() {
        if (weightOnButton >= activationMass) {
            Activate.Invoke();
        } else {
            DeActivate.Invoke();
        }
    }
}
