using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class TreeVis : MonoBehaviour {

	[System.Serializable]
	public class NodeVisBinding{
		public string Class;
		public GameObject Prefab;
	}

	[System.Serializable]
	public class NodeAddBinding{
		public string Display;
		public string Class;
	}

	public BehaviourNode TreeRoot; 
	public NodeVis TreeVisRoot; 

	public LinkVis linkPrefab;

	public float X_Spacing = 100;
	public float Y_Spacing = 100;


	public NodeVisBinding[] binds;
	public NodeAddBinding[] Classes;


	public NodeEditor SelectedNode;

	public bool recalcPos = false;

	private static TreeVis singleton;

	public static TreeVis getTreeVis(){
		if (singleton == null) {
			GameObject TreeSaveManagerGo = new GameObject();
			singleton = TreeSaveManagerGo.AddComponent<TreeVis>();
		}
		return singleton;
	}

	// Use this for initialization
	void Awake () {
		if (singleton == null) {
			singleton = this;
		} else {
			GameObject.Destroy(gameObject);
		}
	}
	void Start(){
		//LoadTree ();
	}

	public void LoadTree(){
		GameObject go = InstanceNodeGameobject (TreeRoot.Name);
		go.transform.parent = this.transform;
		go.transform.localScale = Vector3.one;
		go.layer = this.gameObject.layer;
		NodeVis nv = go.AddComponent<NodeVis>();
		nv.parent = null;
		nv.treeVis = this;
		TreeVisRoot = nv;
		nv.init(TreeRoot);
		nv.calculatePosition (0);
	}

	public void DestroyTree(){
		transform.DestroyChildren ();
		TreeVisRoot = null;

		transform.DestroyChildren ();
	}
	
	// Update is called once per frame
	void Update () {
		if (recalcPos) {
			recalcPos=false;
			TreeVisRoot.calculatePosition (0);	
		}
	}

	public void SelectedIndexUp(){
		if (SelectedNode != null)
			SelectedNode.indexUp ();
	}

	public void SelectedIndexDown(){
		if (SelectedNode != null)
			SelectedNode.indexDown();
	}

//	public void setSelectedNode{NodeEditor ne){
//			SelectedNode = ne;
//	}


	public GameObject InstanceNodeGameobject(string Class){
		foreach (NodeVisBinding bind in binds) {
			if(Regex.IsMatch(Class,bind.Class)){
				return (GameObject)GameObject.Instantiate(bind.Prefab);
			}
		}

		return GameObject.CreatePrimitive(PrimitiveType.Cube);
	}

	public string getClassName(string Allias){
		foreach (NodeAddBinding bind in Classes) {
			if(Regex.IsMatch(Allias,bind.Display)){
				return bind.Class;
			}
		}
		
		return "Selector";
	}
}
