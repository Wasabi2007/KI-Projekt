using UnityEngine;
using System.Collections;


public class Bullet : MonoBehaviour {

	public int Damage = 3;
	public float TravelTime = 2;
	public Vector2 Direction;
	public float Speed = 5;
	public Robot BulletOwner;
	//bool Hit;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		TravelTime -= Time.deltaTime;
		if (TravelTime <= 0)
			GameObject.Destroy (gameObject);

		rigidbody2D.velocity = Direction.normalized * Speed;
	}

	void OnTriggerEnter2D(Collider2D coll) {
			//coll.gameObject.SendMessage("ApplyDamage", 10);
		if (!coll.gameObject.CompareTag ("Robot") || coll.gameObject.GetComponent<Robot>() == BulletOwner)
						return;
		coll.gameObject.GetComponent<Robot> ().HP -= Damage;
		if (coll.gameObject.GetComponent<Robot> ().HP <= 0)
			coll.gameObject.GetComponent<Robot> ().myBattleStatus = BattleStatus.Defeated;

		GameObject.DestroyObject(gameObject);
	}

}
