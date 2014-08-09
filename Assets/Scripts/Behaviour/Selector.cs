﻿using UnityEngine;
using System.Collections;

public class Selector : BehaviourNode {

	int childIndex = 0;

	public override void OnEnable ()
	{
		base.OnEnable ();
		this.info = "Selector";
	}

	public override void Activate ()
	{
		base.Activate ();
		childIndex = 0;
		if (childNodes.Count > 0) {
			childIndex++;
			childNodes[childIndex-1].Activate();
		}
	}
	

	public override void ChildTerminated (BehaviourInterface child,bool result)
	{
		child.Deactivate ();
		if (childIndex >= childNodes.Count) {
			if(!isRoot){
				parentNode.ChildTerminated(this,false);
			}else{
				//Debug.Log(gameObject);
				Deactivate();
			}
			return;
		}
		
		if (!result) {
			childNodes [childIndex].Activate ();
			childIndex++;
		} else {
			if(!isRoot){
				parentNode.ChildTerminated(this,true);
			}else{
				Deactivate();
			}
		}
		
	}
}
