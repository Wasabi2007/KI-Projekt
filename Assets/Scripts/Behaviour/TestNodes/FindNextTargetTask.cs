using UnityEngine;
using System.Collections;

public class FindNextTargetTask : Task {

	GameObject closest = null;

	// Use this for initialization
	void Start () {
		base.Start ();
		info = "Find next Target";
	}
	
	public override void Activate ()
	{
		base.Activate ();


		//GameObject target = GameObject.FindWithTag("Robot");
		GameObject[] targets;
		targets = GameObject.FindGameObjectsWithTag("Robot");

		float distance = Owner.GetComponent<Robot>().Sightdist;
		Vector3 position = transform.position;
		foreach (GameObject target in targets) {
			if(target == Owner) continue;
			Vector3 diff = target.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = target;
				distance = curDistance;
				Debug.Log ("Next Target is " +curDistance);

			}
		}

		/*if (target != null) 
		{
			Robot targetrobot = target.GetComponent<Robot>();*/
			Robot robo = Owner.GetComponent<Robot>();
			robo.Target = closest;
	}
	
	// Update is called once per frame
	public override void Update(){
		base.Update ();
		Debug.Log ("Find Next Target");
		if (closest == null) 
		{
			parentNode.ChildTerminated (this, false);
			Debug.Log ("No Next Target found");
		}
		else 
		{
			parentNode.ChildTerminated (this, true);
		}
	}
}
