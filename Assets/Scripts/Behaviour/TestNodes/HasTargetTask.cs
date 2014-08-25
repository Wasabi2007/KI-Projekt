using UnityEngine;
using System.Collections;

public class HasTargetTask : Task {

	public bool TerminateWith = true;

	// Use this for initialization
	void Start () {
		
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

		/*if (Owner.GetComponent<Robot> ().Target == null) {	
						Debug.Log ("Robot has no Target");
						TerminateWith = false;
				}

		if(TerminateWith == true)
			Debug.Log ("Robot has a Target");*/
		
		parentNode.ChildTerminated (this, TerminateWith);
	}
}
