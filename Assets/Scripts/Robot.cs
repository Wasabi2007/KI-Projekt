using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Robot : MonoBehaviour,GameManager.IReset {

	public float HP;
	public float FireRate;
	public float Damage;
	public int Ammo;
	public float Speed;

	public float Attdist = 40f; // Angriffsdistanz
	public float Sightdist = 60f; // Sichtweite

	public GameObject Target;

	public SteeringType steeringtype = SteeringType.Wander;
	public GameObject BulletPrefab;

	public float lastFireTime;

	public BattleStatus myBattleStatus = BattleStatus.NotInBattle;
	public BattleStatus pastBattleStatus = BattleStatus.NotInBattle;

	// Private Variablen

	private float r_HP;
	private float r_FireRate;
	private float r_Damage;
	private int r_Ammo;
	private float r_Speed;
	
	private float r_Attdist; // Angriffsdistanz
	private float r_Sightdist; // Sichtweite
	
	private GameObject r_Target;
	
	private SteeringType r_steeringtype = SteeringType.Wander;
	private GameObject r_BulletPrefab;
	
	private float r_lastFireTime;
	
	private BattleStatus r_myBattleStatus = BattleStatus.NotInBattle;
	private BattleStatus r_pastBattleStatus = BattleStatus.NotInBattle;

	// TODO: Drehung und Position über Rigidbody2D

	private Vector2 r_position;
	private float r_dreh;


	void Awake () {
		GameManager.getGameManager().addResetter(this);

		r_HP = HP;
		r_FireRate = FireRate;
		r_Damage = Damage;
		r_Ammo = Ammo;
		r_Speed = Speed;
		
		r_Attdist = Attdist; // Angriffsdistanz
		r_Sightdist = Sightdist; // Sichtweite
		
		r_Target = Target;
		
		r_steeringtype = steeringtype;
		r_BulletPrefab = BulletPrefab;
		
		r_lastFireTime = lastFireTime;
		
		r_myBattleStatus = myBattleStatus;
		r_pastBattleStatus = pastBattleStatus;

		r_position = this.rigidbody2D.position;
		r_dreh = this.rigidbody2D.rotation;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		switch (myBattleStatus) {
		case BattleStatus.AttackingFar:
			if (Time.time - lastFireTime >= FireRate && Target.GetComponent<Robot>().myBattleStatus != BattleStatus.Defeated) {
				if (Ammo > 0) {
					fireBullet ();
					lastFireTime = Time.time;
					Ammo--;
				}
			}
			else if(Target.GetComponent<Robot>().myBattleStatus == BattleStatus.Defeated)
			{
				myBattleStatus = BattleStatus.EnemyDefeated;
			
			}

			break;
		case BattleStatus.Defeated:

			Speed = 0;

			break;
		}

		switch (steeringtype) {
				case SteeringType.Wander:
						wanderSteering ();
						break;
				case SteeringType.Flee:
						fleeSteering ();
						break;
				case SteeringType.Seek:
						seekSteering ();
						break;
				case SteeringType.None:
						rigidbody2D.velocity = Vector2.zero;
						rigidbody2D.angularVelocity = 0f;
						break;
				case SteeringType.FaceOnly:
			//velocity unwichtig
						faceOnlySteering ();
						break;
				}

	}

	Vector3 CageInScreen(Vector3 Pos){
		Vector3 pixelPos = Camera.main.WorldToScreenPoint(Pos);
		Vector3 newPos = pixelPos;
		if(pixelPos.x < 0){
			newPos.x = Camera.main.pixelWidth + pixelPos.x;
		}
		if(pixelPos.y < 0){
			newPos.y = Camera.main.pixelHeight + pixelPos.y;
		}
		
		if(pixelPos.x > Camera.main.pixelWidth){
			newPos.x = pixelPos.x - Camera.main.pixelWidth;
		}
		if(pixelPos.y > Camera.main.pixelHeight){
			newPos.y = pixelPos.y-Camera.main.pixelHeight;
		}

		return Camera.main.ScreenToWorldPoint(newPos);
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
	
	private void faceOnlySteering(){


		Vector2 currentPosition = transform.position;
		Vector2 currentVelocity = calculateCircleCenter() - currentPosition;
		
		Vector2 enemyPosition   = Target.transform.position;
		Vector2 desiredVelocity = enemyPosition-currentPosition;
		
		// move away from enemy with maximum velocity
		/*Vector2 accel = (desiredVelocity - currentVelocity);
		accel.Normalize();
		accel *= Speed;*/
		float desiredRotation = Mathf.Rad2Deg*Mathf.Atan2(desiredVelocity.y,desiredVelocity.x);
		
		//rigidbody2D.MovePosition (targetPosition);
		rigidbody2D.MoveRotation (desiredRotation);

		//Debug.Log ("<" + currentVelocity.normalized + "," + desiredVelocity.normalized + "> = " + Vector2.Dot (currentVelocity.normalized, desiredVelocity.normalized));
		//rigidbody2D.angularVelocity = Mathf.Rad2Deg*Mathf.Acos(Mathf.Clamp(Vector2.Dot(currentVelocity.normalized,desiredVelocity.normalized),-1f,1f));
		//rigidbody2D.angularVelocity = (rigidbody2D.angularD

		//Debug.Log (rigidbody2D.angularVelocity);
		//rigidbody2D.velocity = accel;
	
	
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
		accel *= acc;
		
		Velocity += accel;
		if(Velocity.sqrMagnitude > Speed*Speed){
			Velocity = Velocity.normalized*Speed;
		}
		Vector2 targetPosition = (rigidbody2D.position + Velocity);	
		
		float desiredRotation = Mathf.Rad2Deg*Mathf.Atan2(Velocity.y,Velocity.x);
		
		rigidbody2D.MovePosition (CageInScreen(targetPosition));
		rigidbody2D.MoveRotation (desiredRotation);
		
		//rigidbody2D.angularVelocity = Mathf.Rad2Deg*Mathf.Acos(Mathf.Clamp(Vector2.Dot(currentVelocity.normalized,desiredVelocity.normalized),-1,1));
		
		
		//rigidbody2D.velocity = accel;
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
		accel *= acc;

		Velocity += accel;
		if(Velocity.sqrMagnitude > Speed*Speed){
			Velocity = Velocity.normalized*Speed;
		}
		Vector2 targetPosition = (rigidbody2D.position + Velocity);	
		
		float desiredRotation = Mathf.Rad2Deg*Mathf.Atan2(Velocity.y,Velocity.x);
		
		rigidbody2D.MovePosition (CageInScreen(targetPosition));
		rigidbody2D.MoveRotation (desiredRotation);

		//Debug.Log ("<" + currentVelocity.normalized + "," + desiredVelocity.normalized + "> = " + Vector2.Dot (currentVelocity.normalized, desiredVelocity.normalized));
		//rigidbody2D.angularVelocity = Mathf.Rad2Deg*Mathf.Acos(Mathf.Clamp(Vector2.Dot(currentVelocity.normalized,desiredVelocity.normalized),-1,1));
		
		
		//rigidbody2D.velocity = accel;
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

	private Vector2 Velocity = Vector2.zero;
	private float angularVelocity = 0;
	private float acc = 0.0001f;

	private void wanderSteering(){
		Vector2 currentPosition = rigidbody2D.position;
		Vector2 desiredPosition = calculateTargetPosition();
		
		Vector2 currentVelocity = Velocity;
		Vector2 desiredVelocity = desiredPosition - currentPosition;

		
		Vector2 accel = (desiredVelocity - currentVelocity);
		accel.Normalize();
		accel *= acc;

		float currentRotation = rigidbody2D.rotation;

		Velocity += accel;
		if(Velocity.sqrMagnitude > Speed*Speed){
			Velocity = Velocity.normalized*Speed;
		}
		Vector2 targetPosition = (rigidbody2D.position + Velocity);	

		float desiredRotation = Mathf.Rad2Deg*Mathf.Atan2(Velocity.y,Velocity.x);

		rigidbody2D.MovePosition (CageInScreen(targetPosition));
		rigidbody2D.MoveRotation (desiredRotation);
		//Debug.Log (rigidbody2D.angularVelocity);
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
	
	#region fireBullet
	void fireBullet()
	{
		bool flag = false;

		GameObject bla = (GameObject)GameObject.Instantiate (BulletPrefab);
		Bullet BulletComp = bla.GetComponent<Bullet>();

		//Fuer Kollisionsabfrage
		BulletComp.BulletOwner = this;

		bla.transform.parent = this.transform.parent;
		bla.transform.localScale = Vector3.one;
		bla.transform.position = this.transform.position;
		//Debug.Log (bla.transform.position);

		Vector2 currentPosition = this.transform.position;
		Vector2 currentVelocity = this.rigidbody2D.velocity;

		Vector2 desiredPosition = Target.transform.position;
		
		//Vector2 currentVelocity = rigidbody2D.velocity;
		Vector2 desiredVelocity = desiredPosition - currentPosition;

		BulletComp.Direction = desiredVelocity;
		bla.rigidbody2D.MoveRotation(Mathf.Rad2Deg*Mathf.Atan2(desiredVelocity.y,desiredVelocity.x));
		
		//BulletComp.Direction.normalized;

	}
	#endregion

	public void Reset ()
	{
		  HP = r_HP;
		  FireRate = r_FireRate;
		  Damage = r_Damage;
		  Ammo = r_Ammo;
		  Speed = r_Speed;
		
		  Attdist = r_Attdist; // Angriffsdistanz
		  Sightdist = r_Sightdist; // Sichtweite
		
		  Target = r_Target;
		
		  steeringtype = r_steeringtype;
		  BulletPrefab = r_BulletPrefab;
		 
	      lastFireTime = r_lastFireTime;
		
		  myBattleStatus = r_myBattleStatus;
		  pastBattleStatus = r_pastBattleStatus;

		  this.rigidbody2D.MovePosition(r_position);
		  this.rigidbody2D.MoveRotation(r_dreh);

		  Velocity = Vector2.zero;
		  angularVelocity = 0;
		 
	}
}
