using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour {
    public GameObject energyBall;
    public bool ballAlive, ballUsed;
    EnergyBall eb;
    float timer, recharge = 2f;
    public float lifeTime;

	void Start () {
	}
	
	void Update () {
        timer += Time.deltaTime;
        if(ballUsed == false) {
            if (timer > lifeTime + recharge) {
                ballAlive = false;
                timer -= timer;
            }
        }
        SpawnBall();
	}
    void SpawnBall() {
        if(ballAlive == false) {
            Instantiate(energyBall, gameObject.transform.position, Quaternion.identity);
            ballAlive = true;
        }
    }
    
}
