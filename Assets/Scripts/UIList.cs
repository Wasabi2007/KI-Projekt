using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(UITable))]
public class UIList : MonoBehaviour {

	public UIListItem ItemPrefab;
	public string selected = "";

	public UITable table;

	private int count = 0;
	private bool needReposition = true;

	void Awake(){
		table = GetComponent<UITable> ();
	}

	public void InitList(){
		count = 0;
		needReposition = true;
		table.transform.DestroyChildren ();
		table.Reposition ();
		List<string> trees = TreeSaveManager.getTreeSaveManager ().savedTrees;
		foreach (string tree in trees) {
			GameObject go = (GameObject)GameObject.Instantiate(ItemPrefab.gameObject);
			go.layer = gameObject.layer;
			go.transform.parent = gameObject.transform;
			go.transform.localScale = Vector3.one;
			UIListItem li = go.GetComponent<UIListItem>();
			li.lable.text = tree;
			li.list = this;
		}

		table.Reposition ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		count++;
		if (needReposition && count > 2) {
			needReposition = false;
			table.Reposition();		
		}
	}
}
