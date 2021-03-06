﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomShuffle : BehaviourNode {
	protected List<LeafNode> leftTotry;
	//int childIndex = 0;
	
	public override void OnEnable ()
	{
		base.OnEnable ();
		info = "Random Shuffle";
	}
	
	public override void Activate ()
	{
		base.Activate ();
		leftTotry = new List<LeafNode>(childNodes);
		activateRandom ();
	}
	
	private void activateRandom(){
		int index = Random.Range (0, leftTotry.Count);
		leftTotry [index].Activate ();
		leftTotry.RemoveAt (index);
	}
	
	public override void ChildTerminated (BehaviourInterface child, bool result)
	{
		child.Deactivate ();
		if(!result){
			parentNode.ChildTerminated(this,false);
		}else{
			if(leftTotry.Count > 0){
				activateRandom();
			}else{
				parentNode.ChildTerminated(this,true);
			}
		}
	}
}
