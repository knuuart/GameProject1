using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour {
    Rigidbody rb;
    public float lifeTime = 5f, timer;
    TeleportationV2 tp;

	// Use this for initialization
	void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward, ForceMode.Impulse);
        tp = GetComponent<TeleportationV2>();
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        
        if (timer > lifeTime) {
            DestroyBall();
        }
    }
    public void DestroyBall() {
        Destroy(gameObject);
    }

}
