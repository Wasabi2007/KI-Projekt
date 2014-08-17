using UnityEngine;
using System.Collections;

public class BehaviourNodeEditor : NodeEditor {

	public BehaviourNode BNode;

	public bool addTestClass = false;
	public string ClassName = "Debug Out";

	public void AddNode(string Allias){
		GameObject go = new GameObject (Allias);
		LeafNode lNode = (LeafNode)go.AddComponent (NV.treeVis.getClassName(Allias));
		lNode.parentNode = BNode;
		BNode.childNodes.Add (lNode);
		go.transform.parent = BNode.transform;

		NV.AddChild (lNode);
		NV.NeedSizeRecalced = true;
		NV.treeVis.TreeVisRoot.calculatePosition (0);

	}

	// Use this for initialization
	public override void Start () {
		base.Start ();
		if (NV != null&& NV.node is BehaviourNode) {
			BNode = (BehaviourNode)NV.node;
		}

	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update ();
		if (Node == null) {
			if (NV != null&& NV.node is BehaviourNode) {
				BNode = (BehaviourNode)NV.node;
			}		
		}

		if (addTestClass) {
			addTestClass = false;
			AddNode(ClassName);
		}
	}
}
