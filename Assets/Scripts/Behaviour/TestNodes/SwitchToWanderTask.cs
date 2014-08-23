using UnityEngine;
using System.Collections;

public class SwitchToWanderTask : Task {
	
	public override void Awake ()
	{
		base.Awake ();
	}
	
	public override void Start ()
	{
		base.Start ();
		info = "Set Steering to Wander";
	}
	
	public override void Activate ()
	{
		base.Activate ();
		Robot robo = Owner.GetComponent <Robot>();
		robo.steeringtype = SteeringType.Wander;
	}
	
	public override void Deactivate ()
	{
		base.Deactivate ();
	}
	
	public override void Update(){
		base.Update ();
		Debug.Log ("Switch to Wanderer");
		parentNode.ChildTerminated (this,true);
	}
}
