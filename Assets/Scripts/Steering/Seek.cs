﻿using UnityEngine;
using System.Collections;

public class Seek : TargetSteering {

	public float Speed = 1;
	
	// Use this for initialization
	void Start () {	
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 currentPosition = Owner.transform.position;
		Vector2 currentVelocity = Owner.rigidbody2D.velocity;
		
		Vector2 enemyPosition   = Target.transform.position;
		Vector2 desiredVelocity = enemyPosition-currentPosition;
		
		// move away from enemy with maximum velocity
		Vector2 accel = (desiredVelocity - currentVelocity);
		accel.Normalize();
		accel *= Speed;

		//Debug.Log ("<" + currentVelocity.normalized + "," + desiredVelocity.normalized + "> = " + Vector2.Dot (currentVelocity.normalized, desiredVelocity.normalized));
		Owner.rigidbody2D.angularVelocity = Mathf.Rad2Deg*Mathf.Acos(Mathf.Clamp(Vector2.Dot(currentVelocity.normalized,desiredVelocity.normalized),-1,1));

		
		Owner.rigidbody2D.velocity = accel;
	}
}
