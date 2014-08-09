using UnityEngine;
using System.Collections;

public class TaskEditor : NodeEditor {

	public UILabel infoDisplay;
	public Task MyTask;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		if (NV != null&& NV.node is Task) {
			MyTask = (Task)NV.node;
		}
		updateinfo ();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update ();
		if (MyTask == null) {
			if (NV != null&& NV.node is Task) {
				MyTask = (Task)NV.node;
			}		
		}
		updateinfo ();
	}

	void updateinfo(){
		if (infoDisplay != null && MyTask != null) {
			infoDisplay.text = MyTask.Info;		
		}
	}
}
