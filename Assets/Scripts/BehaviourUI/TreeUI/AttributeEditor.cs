using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(UITable))]
public class AttributeEditor : MonoBehaviour {

	public AttributeUI StringPrefab;
	public AttributeUI FloatPrefab;
	public AttributeUI IntPrefab;
	public AttributeUI BoolPrefab;

	public TreeVis treevis;
	private NodeEditor oldSelectetNode;
	private UITable table;

	public void Awake(){
		table = GetComponent<UITable> ();
	}

	public void Update(){
		if (treevis.SelectedNode != oldSelectetNode) {
			oldSelectetNode = treevis.SelectedNode;	
			Clear();
			AnalyzeNode(treevis.SelectedNode);
		}

	}

	public void Clear(){
		transform.DestroyChildren ();
	}

	public void AnalyzeNode(NodeEditor Node){
		if (Node is TaskEditor) {
			TaskEditor te = (TaskEditor)Node;
			foreach(TaskAttribute ta in te.MyTask.attributes){
				spawnPrefab(ta);
			}
		}
		table.Reposition ();
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
