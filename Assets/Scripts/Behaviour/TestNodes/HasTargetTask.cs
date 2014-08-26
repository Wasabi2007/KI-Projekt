using UnityEngine;
using System.Collections;

public class HasTargetTask : Task {

	public bool TerminateWith = true;

	// Use this for initialization

	public override void Awake ()
	{
		base.Awake ();
		info = "Robot has a target";
	}


	public override void Activate ()
	{
		base.Activate ();
		if(Owner.GetComponent<Robot> ().Target == null){	
			Debug.Log ("Robot has no Target");
			TerminateWith = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();

		parentNode.ChildTerminated (this, TerminateWith);
	}
}
