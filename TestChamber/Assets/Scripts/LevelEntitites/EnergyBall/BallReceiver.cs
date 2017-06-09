using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallReceiver : MonoBehaviour {
    public UnityEvent onBallReceived;
    public UnityEvent ballReceiveSound;
    public BallSpawner bs;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "EnergyBall" && bs.ballUsed == false) {
            EnergyBall eb = other.GetComponent<EnergyBall>();
            eb.DestroyBall();
            bs.ballUsed = true;
            onBallReceived.Invoke();
            ballReceiveSound.Invoke();
        }
    }
}
