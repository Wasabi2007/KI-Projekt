﻿using UnityEngine;
using System.Collections;

public class HPMoreThanTask : Task {

	public float health = 0;
	public bool TerminateWith = true;
	
	//private float stime = 0;
	private TaskAttribute healthTa;
	
	public override void Awake ()
	{
		base.Awake ();
		healthTa = addAttribute ("HP more than:",TaskAttributeType.FLOAT,health.ToString(), "setHP");
	}
	
	public void setHP(Object obj){
		UIInput input = (UIInput)obj;
		health = float.Parse(input.value);
		info = "HP more than:"+ health;
		healthTa.Value = health.ToString ();
	}
	
	public override void Start ()
	{
		base.Start ();
		info = "HP more than:"+ health;
	}
	
	public override void Activate ()
	{
		base.Activate ();
		//Debug.Log ("HP is" + Owner.GetComponent<Robot>().HP + "compared to " + health);
		if (Owner.GetComponent<Robot>().HP > health) 
		{
				Debug.Log ("HP is more than" + health);	
		} else {
				TerminateWith = false;
				Debug.Log ("HP is not more than" + health);
		}
	}
	
	public override void Deactivate ()
	{
		base.Deactivate ();
		info = "HP more than:" + health;
	}

	// Update is called once per frame
	public override void Update () {
		/*info = "Wait:"+((this.stime + WaitTime)-Time.time)+" sec";
		if (Time.time > this.stime + WaitTime) {
			parentNode.ChildTerminated (this,TerminateWith);
		}*/
		parentNode.ChildTerminated (this,TerminateWith);
	}
}
