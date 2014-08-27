using UnityEngine;
using System.Collections;

public class DistanceMoreThenTask : Task {

	public float distance = 1;
	public bool TerminateWith = true;
	
	//private float stime = 0;
	private TaskAttribute distanceTa;
	
	public override void Awake ()
	{
		base.Awake ();
		distanceTa = addAttribute ("Distance more than:",TaskAttributeType.FLOAT,distance.ToString(), "setDistance");
		info = "Distance more than:";
	}
	
	public void setDistance(Object obj){
		UIInput input = (UIInput)obj;
		distance = float.Parse(input.value);
		info = "Distance more than:"+ distance;
		distanceTa.Value = distance.ToString ();
	}
	

	public override void Activate ()
	{
		base.Activate ();
		GameObject[] targets;
		targets = GameObject.FindGameObjectsWithTag ("Robot");
		
		float dist = Owner.GetComponent<Robot> ().Sightdist;
		Vector3 position = transform.position;
		foreach (GameObject target in targets) {
			if (target == Owner || target != Owner.GetComponent<Robot> ().Target)
				continue;
			Vector3 diff = target.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance > dist * distance) {
				Debug.Log ("Next Target is more than" + distance + "away");	
			} else {
				TerminateWith = false;
				Debug.Log ("Next Target is not more than" + distance + "away");
			}
		}
	}
	
	public override void Deactivate ()
	{
		base.Deactivate ();
		info = "Distance less than:"+distance;
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
