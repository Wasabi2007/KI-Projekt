using UnityEngine;
using System.Collections;

public class HasNoTargetTask : Task {
	
	public bool TerminateWith = true;
	
	// Use this for initialization

	public override void Awake ()
	{
		base.Awake ();
		info = "Robot has no target";
	}

	public override void Activate ()
	{
		base.Activate ();
		Debug.Log (IsActive);
		if (Owner.GetComponent<Robot> ().Target != null) {	
						Debug.Log ("Robot has a Target");
						TerminateWith = false;
				}
	}
	
	// Update is called once per frame
	void Update () {
		base.Update ();

		
		parentNode.ChildTerminated (this, TerminateWith);
	}
}
