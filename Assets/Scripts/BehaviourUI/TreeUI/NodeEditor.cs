using UnityEngine;
using System.Collections;

public class NodeEditor : MonoBehaviour {
	
	public int index;
	public IndexManager Node;
	public NodeVis NV;

	public bool indexUpToggle = false;
	public bool indexDownToggle = false;
	public bool removeIt = false;

	virtual public void indexUp(){
		if(Node.Index - 1 >= 0)
			transform.SetSiblingIndex (transform.GetSiblingIndex () - 1);

		Node.Index = Node.Index - 1;

		index = Node.Index;
		NV.parent.childs.Remove (NV);
		NV.parent.childs.Insert (index,NV);
		NV.parent.calculatePosition (0);
	}

	virtual public void indexDown(){
		if(Node.Index + 1 <= NV.parent.childs.Count)
			transform.SetSiblingIndex (transform.GetSiblingIndex () + 1);

		Node.Index = Node.Index + 1;

		index = Node.Index;
		NV.parent.childs.Remove (NV);
		NV.parent.childs.Insert (index,NV);
		NV.parent.calculatePosition (0);
	}

	virtual public void remove(){
		NV.parent.childs.Remove (NV);
		NV.node.parentNode.childNodes.Remove (NV.node);
		NV.NeedSizeRecalced = true;
		NV.treeVis.TreeVisRoot.calculatePosition (0);
		if (NV.treeVis.SelectedNode == this) {
			NV.treeVis.SelectedNode = null;
		}
		GameObject.Destroy (NV.node.HirachiOwner);
		GameObject.Destroy (NV.gameObject);
	}

	virtual public void toggleColaps(){
		if (NV != null)
		NV.toggleColaps ();
	}

	virtual public void selectThisNode(){
		if (NV != null)
		NV.treeVis.SelectedNode = this;
	}

	// Use this for initialization
	virtual public void Start () {
		NV = GetComponent<NodeVis> ();
		if (NV != null) {
			Node = NV.node;
				} else {
						Node = GetComponent<BehaviourNode> ();
						if (Node == null) {
								Node = GetComponent<Task> ();
						}
				}
		index = Node.Index;
	}
	
	// Update is called once per frame
	virtual public void Update () {
		if (NV == null) {
			NV = GetComponent<NodeVis> ();
			if(NV != null){
				Node = NV.node;
				index = Node.Index;
			}
		}
		if (removeIt) {
			removeIt = false;
			remove();
				}
		if (indexUpToggle) {
			indexUpToggle = false;
			indexUp();
		}
		if (indexDownToggle) {
			indexDownToggle = false;
			indexDown();
		}

	}
}
