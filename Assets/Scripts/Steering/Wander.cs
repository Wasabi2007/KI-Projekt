using UnityEngine;
using System.Collections;

public class Wander : Steering {

	public float Speed;

	private float wanderAngle;
	private float wanderAngularVelo;

	private const float MAX_WANDER_VELO = 0.1f;
	private const float CENTER_OFFSET = 1f;
	private const float WANDER_RADIUS = 0.60f;

	//debugValue
	private int segments = 50;

	// Use this for initialization
	void Start () {	
		wanderAngle = 0.0f;
		wanderAngularVelo = 0.1f*(2.0f * Random.value - 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 currentPosition = Owner.transform.position;
		Vector2 desiredPosition = calculateTargetPosition();
		
		Vector2 currentVelocity = Owner.rigidbody2D.velocity;
		Vector2 desiredVelocity = desiredPosition - currentPosition;
		
		Vector2 accel = (desiredVelocity - currentVelocity);
		Owner.rigidbody2D.AddForceAtPosition (accel, calculateCircleCenter ());

		//Owner.rigidbody2D.velocity = desiredVelocity;
		//Debug.Log(Vector2.Dot(currentVelocity,desiredVelocity));
		//Debug.Log(Mathf.Acos(Vector2.Dot(currentVelocity.normalized,desiredVelocity.normalized)));
		Owner.rigidbody2D.angularVelocity = Mathf.Rad2Deg*Mathf.Acos(Vector2.Dot(currentVelocity.normalized,desiredVelocity.normalized));

		// move the circle point randomly on the circular path
		// calculate a randomized acceleration for the circle point
		float wanderAngularAccel = (0.2f* Random.value - 0.1f);
		wanderAngularVelo += 0.5f*wanderAngularAccel;
		wanderAngularVelo = Mathf.Clamp(wanderAngularVelo,-MAX_WANDER_VELO, MAX_WANDER_VELO);
		wanderAngle += 0.5f*wanderAngularVelo;
	}

	Vector2 calculateCircleCenter() {
		Vector2 currentPosition = Owner.transform.position;
		
		// calculate the direction we're facing from the rotation
		float currentRotation = Mathf.Deg2Rad * Owner.transform.eulerAngles.z;
		Vector2 faceDirection = new Vector2(Mathf.Cos(currentRotation), Mathf.Sin(currentRotation));
		
		// calculate the center of a circle right in front of you
		return currentPosition + CENTER_OFFSET * faceDirection;
	}
	
	Vector2 calculateTargetPosition() {
		
		// calculate the center of a circle right in front of you
		Vector2 center = calculateCircleCenter();
		
		// calculate the current point on the circle
		float currentRotation = Mathf.Deg2Rad*Owner.transform.eulerAngles.z;
		float circleAngle = wanderAngle + currentRotation;
		
		Vector2 circlePoint = WANDER_RADIUS * new Vector2(Mathf.Cos(circleAngle), Mathf.Sin(circleAngle));
		
		// the point on the circle is our desired position
		return center + circlePoint;
		
	}

	void OnDrawGizmos(){
		Vector3 Pos = calculateCircleCenter();
		
		float r1 = WANDER_RADIUS;
		float r2 = WANDER_RADIUS;
		
		Vector3 from = new Vector3 (Mathf.Sin (0) * r1, Mathf.Cos (0) * r2);
		from += Pos;
		Vector3 to;
		
		for (int seg = 1; seg <= segments; seg++) {
			to = new Vector3 (Mathf.Sin ((2 * Mathf.PI) / segments * seg) * r1, Mathf.Cos ((2 * Mathf.PI) / segments * seg) * r2);
			to += Pos;
			
			Gizmos.DrawLine (from, to);
			from = to;
		}

		Gizmos.DrawCube (calculateTargetPosition(), Vector3.one*0.1f);

	}


}
