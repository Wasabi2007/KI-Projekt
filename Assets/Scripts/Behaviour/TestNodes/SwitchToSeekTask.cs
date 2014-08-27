using UnityEngine;
using System.Collections;

public class SwitchToSeekTask : Task{

	public override void Awake ()
	{
		base.Awake ();
		info = "Switch Steering to Seek";
	}

	public override void Activate ()
	{
		base.Activate ();
		Robot robo = Owner.GetComponent <Robot>();
		robo.steeringtype = SteeringType.Seek;
	}
	
	public override void Deactivate ()
	{
		base.Deactivate ();
	}
	
	public override void Update(){
		base.Update ();
		Debug.Log ("Switch to Seek");
		parentNode.ChildTerminated (this, true);
	}
}
