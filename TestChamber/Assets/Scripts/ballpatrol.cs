using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballpatrol : MonoBehaviour {
	public float speed = 2f;
	public Transform[] waypoints;
	public float tolerance = 0.2f;
	float dir;

	int targetIndex;

	void Start () {
	}

	void Update () {

		Vector3 direction;
		direction = waypoints[targetIndex].position - transform.position;
		float distanceToWaypoint = direction.magnitude;

		transform.Translate (Vector3.right * dir * Time.deltaTime * speed);

		if (targetIndex == 0) {
			dir = -1;
		} else {
			dir = 1;
		}

		if (direction.magnitude < tolerance) {
			targetIndex += 1;
			if (targetIndex == waypoints.Length)
				targetIndex = 0;
		}

	}
}
