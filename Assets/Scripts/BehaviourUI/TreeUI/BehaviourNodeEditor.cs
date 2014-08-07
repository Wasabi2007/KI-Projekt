using UnityEngine;
using System.Collections;

public class BehaviourNodeEditor : NodeEditor {

	public BehaviourNode Node;

	public bool addTestClass = false;
	public string ClassName = "Debug Out";

	public void AddNode(string Allias){
		GameObject go = new GameObject (Allias);
		LeafNode lNode = (LeafNode)go.AddComponent (NV.treeVis.getClassName(Allias));
		go.transform.parent = Node.transform;

		NV.AddChild (lNode);
		NV.treeVis.TreeVisRoot.calculatePosition (0);
	}

	// Use this for initialization
	public override void Start () {
		base.Start ();
		if (NV != null&& NV.node is BehaviourNode) {
			Node = (BehaviourNode)NV.node;
		}

	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update ();
		if (Node == null) {
			if (NV != null&& NV.node is BehaviourNode) {
				Node = (BehaviourNode)NV.node;
			}		
		}

		if (addTestClass) {
			addTestClass = false;
			AddNode(ClassName);
		}
	}
}
