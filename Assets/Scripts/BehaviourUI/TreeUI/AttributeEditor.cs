﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(UITable))]
public class AttributeEditor : MonoBehaviour {

	public AttributeUI StringPrefab;
	public AttributeUI FloatPrefab;
	public AttributeUI IntPrefab;
	public AttributeUI BoolPrefab;

	public ChildNodeUI ChildDisplay;
	public NewChildNodeUI PopUplistPrefab;


	public TreeVis treevis;
	private NodeEditor oldSelectetNode;
	private UITable table;

	private bool delayedReposition = false;
	private int delayedCount = 0;

	public void Awake(){
		table = GetComponent<UITable> ();
	}

	public void Update(){

		if (delayedReposition) {
			delayedCount++;
			if(delayedCount > 2){
				delayedReposition = false;
				delayedCount = 0;
				table.Reposition ();
			}
		}
		if (treevis.SelectedNode != oldSelectetNode) {
			oldSelectetNode = treevis.SelectedNode;	
			UpdateNodes();
		}


	}

	public void Clear(){
		transform.DestroyChildren ();
	}

	public void UpdateNodes(){
		Clear();
		AnalyzeNode(treevis.SelectedNode);
	}

	public void AnalyzeNode(NodeEditor Node){
		if (Node is TaskEditor) {
			TaskEditor te = (TaskEditor)Node;
			foreach(TaskAttribute ta in te.MyTask.attributes){
				spawnPrefab(ta);
			}
		}else if(Node is BehaviourNodeEditor){
			BehaviourNodeEditor bne = (BehaviourNodeEditor)Node;

			foreach(NodeVis nv in bne.NV.childs){
				GameObject go = (GameObject)GameObject.Instantiate (ChildDisplay.gameObject);
				ChildNodeUI cnUI = go.GetComponent<ChildNodeUI>();
				go.transform.parent = this.transform;
				go.transform.localScale = Vector3.one;

				cnUI.node = nv.GetComponent<NodeEditor>();
				cnUI.NodeName.text = nv.node.Info;
				cnUI.ae = this;
			}
			{
				GameObject go = (GameObject)GameObject.Instantiate (PopUplistPrefab.gameObject);
				NewChildNodeUI ncnUI = go.GetComponent<NewChildNodeUI>();
				go.transform.parent = this.transform;
				go.transform.localScale = Vector3.one;
				ncnUI.Node = bne;
				ncnUI.ae = this;
				ncnUI.initList(treevis.Classes);
			}
		}
		delayedReposition = true;
	}

	private void spawnPrefab(TaskAttribute ta){
		switch (ta.AttributeType) {
		case TaskAttributeType.STRING:
			spawnPrefab(StringPrefab).SetEventDelegate(ta);
			break;
		case TaskAttributeType.FLOAT:
			spawnPrefab(FloatPrefab).SetEventDelegate(ta);
			break;
		case TaskAttributeType.INT:
			spawnPrefab(IntPrefab).SetEventDelegate(ta);
			break;
		case TaskAttributeType.BOOL:
			spawnPrefab(BoolPrefab).SetEventDelegate(ta);
			break;
		}
	}

	private AttributeUI spawnPrefab(AttributeUI UIElement){
		GameObject go = (GameObject)GameObject.Instantiate (UIElement.gameObject);
		AttributeUI aUI = go.GetComponent<AttributeUI>();
		go.transform.parent = this.transform;
		go.transform.localScale = Vector3.one;
		return aUI;
	}
}