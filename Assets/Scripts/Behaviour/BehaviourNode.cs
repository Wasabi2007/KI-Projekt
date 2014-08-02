using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BehaviourNode : MonoBehaviour,ParentNode,LeafNode {
	public bool isActive = false;

	public bool IsActive {get{return isActive;} set{ isActive = value; }}
	public List<LeafNode> childNodes {get; set;}
	public ParentNode parentNode { get; set;}

	protected bool isRoot = false;

	public virtual void Awake () {

	}

	// Use this for initialization
	public virtual void Start () {

		childNodes = new List<LeafNode>();
		for (int childCount = 0; childCount < transform.childCount; childCount++) {
			childNodes.AddRange(transform.GetChild(childCount).GetComponents<BehaviourNode>());
			childNodes.AddRange(transform.GetChild(childCount).GetComponents<Task>());
		}
		if(transform.parent != null)
			parentNode = transform.parent.GetComponent<BehaviourNode> ();

		if(parentNode == null || parentNode == this) {
			isRoot = true;	
		}
		//Debug.Log (gameObject+" parent "+parentNode);
		if (isActive) {
			Activate ();	
		} else {
			this.enabled = false;
		}
	}

	public virtual void Activate (){
		isActive = true;
		this.enabled = true;
	}
	public virtual void Deactivate (){
		isActive = false;
		foreach (LeafNode childNode in childNodes) {
			childNode.Deactivate();
		}
		this.enabled = false;
	}
	public abstract void ChildTerminated (BehaviourInterface child,bool result);
	
	// Update is called once per frame
	public virtual void Update () {

	}
}
