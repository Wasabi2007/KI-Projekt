using UnityEngine;
using System.Collections;

public class WaitTask : Task {

	public float WaitTime = 5;
	public bool TerminateWith = true;

	private float stime = 0;
	private TaskAttribute waitTimeTa;

	public override void Awake ()
	{
		base.Awake ();
		waitTimeTa = addAttribute ("WaitTime",TaskAttributeType.FLOAT,WaitTime.ToString(), "setWaitTime");
	}

	public void setWaitTime(Object obj){
		UIInput input = (UIInput)obj;
		WaitTime = float.Parse(input.value);
		info = "Wait:"+WaitTime+" sec";
		waitTimeTa.Value = WaitTime.ToString ();
	}

	public override void Start ()
	{
		base.Start ();
		info = "Wait:"+WaitTime+" sec";
	}

	public override void Activate ()
	{
		base.Activate ();
		this.stime = Time.time;		
		info = "Wait:"+WaitTime+" sec";
	}
	
	public override void Deactivate ()
	{
		base.Deactivate ();
		info = "Wait:"+WaitTime+" sec";
	}
	// Update is called once per frame
	public override void Update () {
		Robot MrRoboto = Owner.GetComponent<Robot> ();
		info = "Wait:"+((this.stime + WaitTime)-Time.time)+" sec";
			if (Time.time > this.stime + WaitTime) {
				parentNode.ChildTerminated (this,TerminateWith);
			}
	}
}
