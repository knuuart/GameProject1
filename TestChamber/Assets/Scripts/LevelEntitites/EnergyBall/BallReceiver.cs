using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallReceiver : MonoBehaviour {
	public UnityEvent onBallReceived;
	public BallSpawner bs;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other) {
		if (other.tag == "EnergyBall" && bs.ballUsed == false) {
            EnergyBall eb = other.GetComponent<EnergyBall>();
            eb.DestroyBall();
            bs.ballUsed = true;
			onBallReceived.Invoke ();
        }
    }
}
