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

	public BehaviourNode TreeRoot; 
	public NodeVis TreeVisRoot; 

	public LinkVis linkPrefab;

	public float X_Spacing = 100;
	public float Y_Spacing = 100;


	public NodeVisBinding[] binds;


	// Use this for initialization
	void Start () {
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
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject InstanceNodeGameobject(string Class){
		foreach (NodeVisBinding bind in binds) {
			if(Regex.IsMatch(Class,bind.Class)){
				return (GameObject)GameObject.Instantiate(bind.Prefab);
			}
		}

		return GameObject.CreatePrimitive(PrimitiveType.Cube);
	}
}
