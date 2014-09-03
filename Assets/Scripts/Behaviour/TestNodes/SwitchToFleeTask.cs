using UnityEngine;
using System.Collections;

public class SwitchToFleeTask : Task {

	public string Message = "";
	
	public override void Awake ()
	{
		base.Awake ();
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
				parentNode.ChildTerminated (this, true);
		}
}
