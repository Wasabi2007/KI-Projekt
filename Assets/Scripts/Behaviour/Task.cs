using UnityEngine;
using System.Collections;

public abstract class Task : MonoBehaviour, LeafNode , BehaviourInterface {
	private bool isActive = false;
	
	public bool IsActive {get{return isActive;} set{ isActive = value; }}
	public ParentNode parentNode {get; set;}
	public string Name{ get { return this.GetType().Name; }}
	public int Index{ 
		get { 	return transform.GetSiblingIndex (); } 
		set { 	parentNode.childNodes.Remove(this); 
			parentNode.childNodes.Insert(Mathf.Clamp(value,0,parentNode.childNodes.Count),this); 
			transform.SetSiblingIndex (value);
		} 
	}
	private BehaviourTree tree;
	public BehaviourTree Tree {get{ return tree; } set{tree = value;}}
	public GameObject Owner{get{ return (tree!= null?tree.Owner:null); } set{tree.Owner = value;}}
	public string info;

	// Use this for initialization
	public virtual void Start () {
		
		parentNode = transform.parent.GetComponent<BehaviourNode> ();

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
		this.enabled = false;
	}
	public virtual void ChildTerminated (BehaviourInterface child,bool result){}

	public virtual void Update(){
		}
}
