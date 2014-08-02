using UnityEngine;
using System.Collections;

public class ActivateGameObjectTask : Task {

	public GameObject Target;

	public override void Activate ()
	{
		base.Activate ();
		if(Target != null){
			Target.SetActive(true);
		}
		parentNode.ChildTerminated (this, true);
	}
}
