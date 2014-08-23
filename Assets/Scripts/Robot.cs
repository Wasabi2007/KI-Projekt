using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Robot : MonoBehaviour {

	public float HP;
	public float FireRate;
	public float Damage;
	public int Ammo;
	public float Speed;

	public float Attdist = 20f; // Angriffsdistanz
	public float Sightdist = 40f; // Sichtweite

	public GameObject Target;

	public SteeringType steeringtype = SteeringType.Wander;

	public BattleStatus myBattleStatus = BattleStatus.NotInBattle;
	public BattleStatus pastBattleStatus = BattleStatus.NotInBattle;
	public BattleStatus enemyBattleStatus = BattleStatus.NotInBattle;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		switch (steeringtype) {
		case SteeringType.Wander:
			wanderSteering();
			break;
		case SteeringType.Flee:
			fleeSteering();
			break;
		case SteeringType.Seek:
			seekSteering();
			break;
		case SteeringType.None:
			rigidbody2D.velocity = Vector2.zero;
			rigidbody2D.angularVelocity = 0f;
			break;
		}
	
	}

	void OnDrawGizmos(){
		if (steeringtype == SteeringType.Wander) {
						Vector3 Pos = calculateCircleCenter ();
		
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
		
						Gizmos.DrawCube (calculateTargetPosition (), Vector3.one * 0.1f);
				}
		
	}

	#region FleeSteering
	private void fleeSteering(){
		Vector2 currentPosition = transform.position;
		Vector2 currentVelocity = rigidbody2D.velocity;
		
		Vector2 enemyPosition   = Target.transform.position;
		Vector2 desiredVelocity = currentPosition - enemyPosition;
		
		// move away from enemy with maximum velocity
		Vector2 accel = (desiredVelocity - currentVelocity);
		accel.Normalize();
		accel *= Speed;
		
		rigidbody2D.angularVelocity = Mathf.Rad2Deg*Mathf.Acos(Mathf.Clamp(Vector2.Dot(currentVelocity.normalized,desiredVelocity.normalized),-1,1));
		
		
		rigidbody2D.velocity = accel;
	}
	#endregion

	#region SeekSteering
	private void seekSteering(){
		Vector2 currentPosition = transform.position;
		Vector2 currentVelocity = rigidbody2D.velocity;
		
		Vector2 enemyPosition   = Target.transform.position;
		Vector2 desiredVelocity = enemyPosition-currentPosition;
		
		// move away from enemy with maximum velocity
		Vector2 accel = (desiredVelocity - currentVelocity);
		accel.Normalize();
		accel *= Speed;
		
		//Debug.Log ("<" + currentVelocity.normalized + "," + desiredVelocity.normalized + "> = " + Vector2.Dot (currentVelocity.normalized, desiredVelocity.normalized));
		rigidbody2D.angularVelocity = Mathf.Rad2Deg*Mathf.Acos(Mathf.Clamp(Vector2.Dot(currentVelocity.normalized,desiredVelocity.normalized),-1,1));
		
		
		rigidbody2D.velocity = accel;
	}
	#endregion

	#region WanderSteering
	
	private float wanderAngle;
	private float wanderAngularVelo;
	
	private const float MAX_WANDER_VELO = 0.1f;
	private const float CENTER_OFFSET = 0.5f;
	private const float WANDER_RADIUS = 0.30f;
	
	//debugValue
	private int segments = 50;

	private void wanderSteering(){
		Vector2 currentPosition = transform.position;
		Vector2 desiredPosition = calculateTargetPosition();
		
		Vector2 currentVelocity = rigidbody2D.velocity;
		Vector2 desiredVelocity = desiredPosition - currentPosition;
		
		Vector2 accel = (desiredVelocity - currentVelocity)*Speed;
		rigidbody2D.AddForceAtPosition (accel, calculateCircleCenter ());
		
		//Owner.rigidbody2D.velocity = desiredVelocity;
		//Debug.Log(Vector2.Dot(currentVelocity,desiredVelocity));
		//Debug.Log(Mathf.Acos(Vector2.Dot(currentVelocity.normalized,desiredVelocity.normalized)));
		rigidbody2D.angularVelocity = Mathf.Rad2Deg*Mathf.Acos(Mathf.Clamp(Vector2.Dot(currentVelocity.normalized,desiredVelocity.normalized),-1f,1f));
		
		// move the circle point randomly on the circular path
		// calculate a randomized acceleration for the circle point
		float wanderAngularAccel = (0.2f* Random.value - 0.1f);
		wanderAngularVelo += 0.5f*wanderAngularAccel;
		wanderAngularVelo = Mathf.Clamp(wanderAngularVelo,-MAX_WANDER_VELO, MAX_WANDER_VELO);
		wanderAngle += 0.5f*wanderAngularVelo;
	}

	Vector2 calculateCircleCenter() {
		Vector2 currentPosition = transform.position;
		
		// calculate the direction we're facing from the rotation
		float currentRotation = Mathf.Deg2Rad * transform.eulerAngles.z;
		Vector2 faceDirection = new Vector2(Mathf.Cos(currentRotation), Mathf.Sin(currentRotation));
		
		// calculate the center of a circle right in front of you
		return currentPosition + CENTER_OFFSET * faceDirection;
	}
	
	Vector2 calculateTargetPosition() {
		
		// calculate the center of a circle right in front of you
		Vector2 center = calculateCircleCenter();
		
		// calculate the current point on the circle
		float currentRotation = Mathf.Deg2Rad*transform.eulerAngles.z;
		float circleAngle = wanderAngle + currentRotation;
		
		Vector2 circlePoint = WANDER_RADIUS * new Vector2(Mathf.Cos(circleAngle), Mathf.Sin(circleAngle));
		
		// the point on the circle is our desired position
		return center + circlePoint;
		
	}
	#endregion
}
