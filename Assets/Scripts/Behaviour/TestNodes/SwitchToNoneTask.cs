using UnityEngine;
using System.Collections;

public class SwitchToNoneTask : Task {
	
	// Use this for initialization
	void Start () {
		base.Start ();
		info = "Switch Steering to None";
	}

	public override void Activate ()
	{
		base.Activate ();
		Robot robo = Owner.GetComponent <Robot>();
		robo.steeringtype = SteeringType.None;

	}
	// Update is called once per frame
	public override void Update(){
		base.Update ();
		Debug.Log ("Switch to None");
		parentNode.ChildTerminated (this,true);
	}
}
