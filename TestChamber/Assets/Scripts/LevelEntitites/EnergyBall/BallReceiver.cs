using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReceiver : MonoBehaviour {

    public GameObject bs;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "EnergyBall") {
            EnergyBall eb = other.GetComponent<EnergyBall>();
            eb.DestroyBall();
            bs.GetComponent<BallSpawner>().ballUsed = true;
        }
    }
}
