using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaffoldPatrol : MonoBehaviour {
	public float speed;
	public Transform[] waypoints;
	public float tolerance = 0.1f;
	public int targetIndex = 0;
	public BallSpawner bs;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (bs.ballUsed) {
			MoveToPoint();		
		}
	}
	void MoveToPoint(){
		transform.position = Vector3.MoveTowards(transform.position, waypoints[targetIndex].position, Time.deltaTime * speed);

		Vector3 direction;
		direction = waypoints[targetIndex].position - transform.position;

		if(direction.magnitude < tolerance){
			targetIndex++;
			if(targetIndex == waypoints.Length){
				targetIndex = 0;
			}
		}
	}
	IEnumerator Wait(){
		yield return new WaitForSeconds (2f);

	}

	void OnCollisionStay(Collision c){
		GameObject go = c.gameObject;
		Vector3 direction;
		direction = waypoints[targetIndex].position - go.transform.position;
		if (bs.ballUsed) {
			go.transform.position = Vector3.MoveTowards(go.transform.position, waypoints[targetIndex].position, Time.deltaTime * speed);
				
		}
	}
}
