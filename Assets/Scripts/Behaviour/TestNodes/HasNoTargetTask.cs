using UnityEngine;
using System.Collections;

public class HasNoTargetTask : Task {
	
	public bool TerminateWith = true;
	
	// Use this for initialization
	void Start () {
		
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

		/*if (Owner.GetComponent<Robot> ().Target != null) {	
			Debug.Log ("Robot has a Target");
			TerminateWith = false;
		}

		if(TerminateWith == true)
			Debug.Log ("Robot has no Target");*/
		
		parentNode.ChildTerminated (this, TerminateWith);
	}
}
