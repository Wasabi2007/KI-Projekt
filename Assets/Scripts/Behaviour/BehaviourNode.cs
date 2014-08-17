using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[SerializeAll]
public abstract class BehaviourNode : MonoBehaviour,ParentNode,LeafNode {
	private bool isActive = false;

	public bool IsActive {get{return isActive;} set{ isActive = value; }}
	public List<LeafNode> childNodes {get; set;}
	public ParentNode parentNode { get; set;}
	public string Name{ get { return this.GetType().Name; }}
	public int Index{ 
		get { 	return transform.GetSiblingIndex (); } 
		set { 	parentNode.childNodes.Remove(this); 
				parentNode.childNodes.Insert(Mathf.Clamp(value,0,parentNode.childNodes.Count),this); 
				transform.SetSiblingIndex (value);
		} 
	}
	private BehaviourTree tree;
	public BehaviourTree Tree {get{ return tree; } set{tree = value; if(childNodes != null)foreach(LeafNode ln in childNodes){ln.Tree = value;}}}
	public GameObject Owner{get{ return (tree!= null?tree.Owner:null); } set{tree.Owner = value;}}
	public GameObject HirachiOwner {get{ return gameObject; }}
	protected string info = "BehaviourNode";
	public string Info{ get{return info;} }

	protected bool isRoot { get { return (parentNode == null || parentNode == this); } }

	public virtual void Awake () {
		gameObject.AddMissingComponent<StoreInformation> ();
		childNodes = new List<LeafNode>();
		for (int childCount = 0; childCount < transform.childCount; childCount++) {
			childNodes.AddRange(transform.GetChild(childCount).GetComponents<BehaviourNode>());
			childNodes.AddRange(transform.GetChild(childCount).GetComponents<Task>());
		}
		if(transform.parent != null)
			parentNode = transform.parent.GetComponent<BehaviourNode> ();
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
