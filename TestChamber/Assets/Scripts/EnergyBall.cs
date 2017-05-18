using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour {
    Rigidbody rb;
    public float lifeTime, timer;
    TeleportationV2 tp;
    GameObject spawner;
    BallSpawner bs;

	// Use this for initialization
	void Start () {
        spawner = GameObject.Find("BallSpawner");
        bs = spawner.GetComponent<BallSpawner>();
        rb = gameObject.GetComponent<Rigidbody>();
        tp = GetComponent<TeleportationV2>();
        lifeTime = bs.lifeTime;
        rb.AddForce(spawner.transform.forward, ForceMode.Impulse);
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
