using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[SerializeAll]
public abstract class BehaviourNode : MonoBehaviour,ParentNode,LeafNode,IControlSerialization {
	[DoNotSerialize]
	private bool isActive = false;

	[DoNotSerialize]
	public bool IsActive {get{return isActive;} set{ isActive = value; }}
	[DoNotSerialize]
	public List<LeafNode> childNodes {get; set;}
	[DoNotSerialize]
	public ParentNode parentNode { get; set;}
	[DoNotSerialize]
	public string Name{ get { return this.GetType().Name; }}
	[DoNotSerialize]
	public int Index{ 
		get { 	return (isRoot?0:transform.GetSiblingIndex ()); } 
		set {if(!isRoot){
				parentNode.childNodes.Remove(this); 
				parentNode.childNodes.Insert(Mathf.Clamp(value,0,parentNode.childNodes.Count),this); 
				transform.SetSiblingIndex (value);
				SaveIndex = value;
			}else{
				SaveIndex = 0;
			}
		} 
	}

	public int SaveIndex = 0;

	[DoNotSerialize]
	private BehaviourTree tree;
	[DoNotSerialize]
	public BehaviourTree Tree {get{ return tree; } set{tree = value; if(childNodes != null)foreach(LeafNode ln in childNodes){ln.Tree = value;}}}
	[DoNotSerialize]
	public GameObject Owner{get{ return (tree!= null?tree.Owner:null); } set{tree.Owner = value;}}
	[DoNotSerialize]
	public GameObject HirachiOwner {get{ return gameObject; }}
	protected string info = "BehaviourNode";
	public string Info{ get{return info;} }

	protected bool isRoot { get { return (parentNode == null || parentNode == this); } }

	public virtual void Awake () {
		//Debug.Log ("*gähn*");

		StoreInformation SI = gameObject.AddMissingComponent<StoreInformation> ();

		//if (LevelSerializer.IsDeserializing)
		//	return;
		childNodes = new List<LeafNode>();
		for (int childCount = 0; childCount < transform.childCount; childCount++) {
			childNodes.AddRange(transform.GetChild(childCount).GetComponents<BehaviourNode>());
			childNodes.AddRange(transform.GetChild(childCount).GetComponents<Task>());
		}
		if(transform.parent != null)
			parentNode = transform.parent.GetComponent<BehaviourNode> ();
	}

	public void ReCaptureChildsAndParents(){
		childNodes = new List<LeafNode>();
		for (int childCount = 0; childCount < transform.childCount; childCount++) {
			childNodes.AddRange(transform.GetChild(childCount).GetComponents<BehaviourNode>());
			childNodes.AddRange(transform.GetChild(childCount).GetComponents<Task>());
		}
		if(transform.parent != null)
			parentNode = transform.parent.GetComponent<BehaviourNode> ();

		foreach (BehaviourInterface bi in childNodes) {
			bi.ReCaptureChildsAndParents();
		}
	}

	public virtual void OnEnable() {
		if (isActive) {
			Activate ();	
		} else {
			this.enabled = false;
		}
	}

	// Use this for initialization
	public virtual void Start () {


		//Debug.Log (gameObject+" parent "+parentNode);
		/*if (isActive) {
			Activate ();	
		} else {
			this.enabled = false;
		}*/
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
	public virtual void Update(){

	}

	public bool ShouldSave ()
	{
		SaveIndex = Index;
		return true;
	}
	
	public virtual void OnDeserialized(){
		Index = SaveIndex;
		//Debug.Log ("Miep Index: "+SaveIndex);
	}
}
