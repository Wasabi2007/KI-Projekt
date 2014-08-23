using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[SerializeAll]
public abstract class Task : MonoBehaviour, LeafNode , BehaviourInterface {
	[DoNotSerialize]
	private bool isActive = false;

	[DoNotSerialize]
	public bool IsActive {get{return isActive;} set{ isActive = value; }}
	[DoNotSerialize]
	public ParentNode parentNode {get; set;}
	[DoNotSerialize]
	public string Name{ get { return this.GetType().Name; }}
	[DoNotSerialize]
	public int Index{ 
		get { 	return transform.GetSiblingIndex (); } 
		set { 	parentNode.childNodes.Remove(this); 
			parentNode.childNodes.Insert(Mathf.Clamp(value,0,parentNode.childNodes.Count),this); 
			transform.SetSiblingIndex (value);
		} 
	}
	[DoNotSerialize]
	private BehaviourTree tree;
	[DoNotSerialize]
	public BehaviourTree Tree {get{ return tree; } set{tree = value;}}
	[DoNotSerialize]
	public GameObject Owner{get{ return (tree!= null?tree.Owner:null); } set{tree.Owner = value;}}
	[DoNotSerialize]
	public GameObject HirachiOwner {get{ return gameObject; }}

	protected string info = "TaskNode";
	public string Info{ get{return info;} }

	[DoNotSerialize]
	public List<TaskAttribute> attributes = new List<TaskAttribute>(); 

	public virtual void Awake(){
		StoreInformation SI = gameObject.AddMissingComponent<StoreInformation> ();

		if(transform.parent != null)
			parentNode = transform.parent.GetComponent<BehaviourNode> ();
		//if (LevelSerializer.IsDeserializing)
		//	return;
	}

	public void ReCaptureChildsAndParents(){
		if(transform.parent != null)
			parentNode = transform.parent.GetComponent<BehaviourNode> ();
	}

	// Use this for initialization
	public virtual void Start () {
		
		parentNode = transform.parent.GetComponent<BehaviourNode> ();
		
		if (isActive) {
			Activate ();	
		} else {
			this.enabled = false;
		}
	}


	public virtual TaskAttribute addAttribute(string attributeName,TaskAttributeType t,string defaultValue, string functionName){
		TaskAttribute ta = new TaskAttribute (attributeName, t, defaultValue, this, functionName);
		attributes.Add (ta);
		return ta;
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
