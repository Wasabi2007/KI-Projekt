using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(UIPopupList))]
public class BehaviourTreeViewSelector : MonoBehaviour {

	public BehaviourTreeEditorHelper bteh;
	private UIPopupList list;

	public UIButton[] toDisable;
	public GameObject[] toDeactivate;

	private bool isEnabled = true;
	private BehaviourNode oldTree;

	void Awake(){
		list = GetComponent<UIPopupList>();

		if(list.isOpen || !isEnabled) return;
		CollectTrees ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick(){
		if(list.isOpen || !isEnabled) return;
		CollectTrees ();
	}

	void CollectTrees ()
	{
		list.items.Clear ();
		BehaviourTree[] behaviourtrees = GameObject.FindObjectsOfType<BehaviourTree> ();
		list.items.Add ("None");
		foreach (BehaviourTree tree in behaviourtrees) {
			if (tree == bteh || tree.Owner == null || tree.TreeRoot == null)
				continue;
			list.items.Add (tree.gameObject.name);
		}
	}

	void DisableStuff(){
		foreach(UIButton button in toDisable){
			button.isEnabled = false;
		}
		foreach(GameObject go in toDeactivate){
			go.SetActive(false);
		}
	}

	void EnableStuff(){
		foreach(UIButton button in toDisable){
			button.isEnabled = true;
		}
		/*foreach(GameObject go in toDeactivate){
			go.SetActive(true);
		}*/
	}

	public void Disable(){
		isEnabled = false;

		list.Close();
		list.items.Clear();
		list.items.Add("None");
	}
	public void Enable(){
		isEnabled = true;

		if(list.isOpen) return;
		CollectTrees ();
	}

	public void Reset(){
		list.value = "None";
	}

	public void onChange(string value){
		if(!isEnabled) return;
		if(value.Equals("None")){
			EnableStuff();
			if(oldTree != null){
				bteh.VisTree(oldTree);
				oldTree = null;
			}

			if(bteh.TreeRoot == null && TreeVis.getTreeVis().TreeRoot != null){
				TreeVis.getTreeVis().DestroyTree();
				TreeVis.getTreeVis().TreeRoot = null;
			}
		}else{
			DisableStuff();
			oldTree = bteh.TreeRoot;
			GameObject go = GameObject.Find(value);
			bool failed = false;
			if(go != null){
				BehaviourTree tree = go.GetComponent<BehaviourTree>();
				if(tree != null && tree.TreeRoot != null){
					bteh.VisTree(tree.TreeRoot);
				} else { failed = true;}
			}else { failed = true;}

			if(failed){
				oldTree = null;
				EnableStuff();
			}
		}
	}
}
