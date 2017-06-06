﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaffoldPatrol : MonoBehaviour {
	public float speed;
	public Transform[] waypoints;
	public float tolerance = 0.1f;
	public int targetIndex = 0;
	public BallSpawner bs;
    Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (bs.ballUsed) {
			MoveToPoint();		
		}
	}
	void MoveToPoint(){
		//transform.position = Vector3.MoveTowards(transform.position, waypoints[targetIndex].position, Time.deltaTime * speed);
        rb.MovePosition(Vector3.MoveTowards(transform.position, waypoints[targetIndex].position, Time.deltaTime * speed));
		Vector3 direction;
		direction = waypoints[targetIndex].position - transform.position;

		if(direction.magnitude < tolerance){
			targetIndex++;
			if(targetIndex == waypoints.Length){
				targetIndex = 0;
			}
		}
	}

	void OnCollisionStay(Collision c){
        
        if (bs.ballUsed) {
            GameObject go = c.gameObject;
            Vector3 offset = transform.position - go.transform.position;
            Vector3 waypointOffset = waypoints[targetIndex].position - offset;
            Vector3 alustaOffset = transform.position - offset;
            //go.transform.position = Vector3.MoveTowards(go.transform.position, waypointOffset, Time.deltaTime * speed);
            go.GetComponent<Rigidbody>().MovePosition(Vector3.MoveTowards(go.transform.position, alustaOffset, Time.fixedDeltaTime * speed));
        }
    }
}
