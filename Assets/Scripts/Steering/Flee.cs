using UnityEngine;
using System.Collections;

public class Flee : TargetSteering {

	public float Speed = 1;
	
	// Use this for initialization
	void Start () {	

	}
	
	// Update is called once per frame
	void Update () {
		Vector2 currentPosition = Owner.transform.position;
		Vector2 currentVelocity = Owner.rigidbody2D.velocity;
		
		Vector2 enemyPosition   = Target.transform.position;
		Vector2 desiredVelocity = currentPosition - enemyPosition;
		
		// move away from enemy with maximum velocity
		Vector2 accel = (desiredVelocity - currentVelocity);
		accel.Normalize();
		accel *= Speed;
		
		Owner.rigidbody2D.velocity = accel;
	}
		
}
