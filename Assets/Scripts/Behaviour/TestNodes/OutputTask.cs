using UnityEngine;
using System.Collections;

public class OutputTask : Task {

	public string Message = "";

	private TaskAttribute messageTa;



	public override void Awake ()
	{
		base.Awake ();
		messageTa = addAttribute ("Message",TaskAttributeType.STRING,Message, "setMessage");
		info = "Out:" + Message;
	}

	public void setMessage(Object obj){
		UIInput input = (UIInput)obj;
		Message = input.value;
		info = "Out:"+Message;
		messageTa.Value = Message;
	}

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
