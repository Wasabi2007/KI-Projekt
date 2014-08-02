using UnityEngine;
using System.Collections;

public class SendMessageTask : Task {

	public GameObject Target;
	public string MethodeName;
	public string Value;

	public override void Activate ()
	{
		base.Activate ();
		if (Target != null) {
			if(Value.Length > 0){
				Target.SendMessage(MethodeName,Value);
			} else{
				Target.SendMessage(MethodeName);
			}
		}
	}
}
