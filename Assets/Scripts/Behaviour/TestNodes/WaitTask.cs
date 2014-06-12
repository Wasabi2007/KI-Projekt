using UnityEngine;
using System.Collections;

public class WaitTask : Task {

	public float WaitTime = 5;
	public bool TerminateWith = true;

	private float stime = 0;
	public override void Activate ()
	{
		base.Activate ();
		this.stime = Time.time;		
	}
	
	public override void Deactivate ()
	{
		base.Deactivate ();
	}
	// Update is called once per frame
	public override void Update () {
			if (Time.time > this.stime + WaitTime) {
				parentNode.ChildTerminated (this,TerminateWith);
			}
	}
}
