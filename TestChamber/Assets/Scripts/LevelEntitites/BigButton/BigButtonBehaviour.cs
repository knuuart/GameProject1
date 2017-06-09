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

    [Header("Sounds for button press & release")]
    public UnityEvent buttonUp;
    public UnityEvent buttonDown;


    private float weightOnButton;
    private List<GameObject> objectsOnButton = new List<GameObject>();
	bool activated;

    void OnCollisionEnter(Collision coll) {
        if (coll.gameObject.GetComponent<Rigidbody>() != null) {
            var rb = coll.gameObject.GetComponent<Rigidbody>();
            weightOnButton += rb.mass;
            ButtonStateCheck();
        }
    }

    void OnCollisionExit(Collision coll) {
        if (coll.gameObject.GetComponent<Rigidbody>() != null) {
            var rb = coll.gameObject.GetComponent<Rigidbody>();
            weightOnButton -= rb.mass;
            ButtonStateCheck();
        }
    }

    void ButtonStateCheck() {
		if (weightOnButton >= activationMass && activated == false) {
            Activate.Invoke();
            buttonDown.Invoke();
            activated = true;
		} else if (weightOnButton < activationMass){
            DeActivate.Invoke();
            buttonUp.Invoke();
            activated = false;
        }
    }
}
