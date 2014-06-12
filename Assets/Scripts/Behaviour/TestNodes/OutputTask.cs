using UnityEngine;
using System.Collections;

public class OutputTask : Task {

	public string Message = "";

	public override void Activate ()
	{
		base.Activate ();
	}

	public override void Deactivate ()
	{
		base.Deactivate ();
	}

	public override void Update(){
		base.Update ();
		Debug.Log (Message);
		parentNode.ChildTerminated (this,true);
	}
}
