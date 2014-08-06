using UnityEngine;
using System.Collections;

public class NodeEditor : MonoBehaviour {
	
	public int index;
	public IndexManager Node;
	public NodeVis NV;

	public bool indexUpToggle = false;
	public bool indexDownToggle = false;

	void indexUp(){
		if(Node.Index - 1 >= 0)
			transform.SetSiblingIndex (transform.GetSiblingIndex () - 1);

		Node.Index = Node.Index - 1;

		index = Node.Index;
		NV.parent.childs.Remove (NV);
		NV.parent.childs.Insert (index,NV);
		NV.parent.calculatePosition (0);
	}

	void indexDown(){
		if(Node.Index + 1 <= NV.parent.childs.Count)
			transform.SetSiblingIndex (transform.GetSiblingIndex () + 1);

		Node.Index = Node.Index + 1;

		index = Node.Index;
		NV.parent.childs.Remove (NV);
		NV.parent.childs.Insert (index,NV);
		NV.parent.calculatePosition (0);
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
			Node = NV.node;
			index = Node.Index;
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
