using UnityEngine;
using System.Collections;

public abstract class Task : MonoBehaviour, LeafNode , BehaviourInterface {
	public bool isActive = false;
	
	public bool IsActive {get{return isActive;} set{ isActive = value; }}
	public ParentNode parentNode {get; set;}
	public string Name{ get { return this.GetType().Name; }}
	public int Index{ get { return transform.GetSiblingIndex (); } set { transform.SetSiblingIndex (value); } }

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

	// Update is called once per frame
	public virtual void Update () {
	
	}
}
