using UnityEngine;
using System.Collections;

public class SwitchToNoneTask : Task {
	
	// Use this for initialization

	public override void Awake ()
	{
		base.Awake ();
		info = "Switch to None-Task";
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

		parentNode.ChildTerminated (this,true);
	}
}
