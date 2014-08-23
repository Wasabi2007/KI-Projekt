using UnityEngine;
using System.Collections;

public class SwitchToFleeTask : Task {

	public string Message = "";
	
	public override void Awake ()
	{
		base.Awake ();
	}

	
	public override void Start ()
	{
		base.Start ();
		info = "Set Steering To Flee";
	}
	
	public override void Activate ()
	{
		base.Activate ();
		Robot robo = Owner.GetComponent <Robot>();
		robo.steeringtype = SteeringType.Flee;
	}
	
	public override void Deactivate ()
	{
		base.Deactivate ();
	}
	
	public override void Update(){
				base.Update ();
				Debug.Log ("Switch to Flee");
				parentNode.ChildTerminated (this, true);
		}
}
