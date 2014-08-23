using UnityEngine;
using System.Collections;

public class BehaviourTreeEditorHelper : BehaviourTree {
	public bool Save = false;
	public bool Load = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Save) {
			Save = false;
			SaveTree("Pah.dat");
		}
		if (Load) {
			Load = false;
			LoadTree("Pah.dat");
		}
	}

	public void NewTree(string RootClass){
		if (TreeRoot != null) {
			GameObject.DestroyImmediate (TreeRoot.gameObject);
			TreeVis.getTreeVis ().DestroyTree ();
			TreeRoot=null;
			TreeVis.getTreeVis ().TreeRoot = null;
		}

		GameObject root = new GameObject ("Root");
		TreeRoot = (BehaviourNode)root.AddComponent (RootClass);
		TreeRoot.Tree = this;
		TreeVis.getTreeVis ().TreeRoot = TreeRoot;
		TreeVis.getTreeVis ().LoadTree ();
	}

	public void SaveTree(string FileName){
		if (FileName.Length == 0)
			return;
		
		LevelSerializer.SaveObjectTreeToFile (FileName, TreeRoot.gameObject);
		TreeSaveManager.getTreeSaveManager ().savedTrees.Add (FileName);
		//Debug.Log ("Saved: " + FileName);
	}
	
	public override void LoadTree(string FileName){
		if (FileName.Length == 0)
			return;
		
		if (TreeRoot != null) {
			GameObject.DestroyImmediate (TreeRoot.gameObject);
			TreeVis.getTreeVis ().DestroyTree ();
			TreeRoot=null;
			TreeVis.getTreeVis ().TreeRoot = null;
		}
		
		LevelSerializer.LoadObjectTreeFromFile(FileName,loaded);
		lastLoadedTree = FileName;
		//LevelSerializer.Collect ();
		//LevelSerializer.LoadObjectTreeFromFile("Pah.dat");
	}
	
	private void loaded(LevelLoader obj){
		//obj.DontDelete = true;
		TreeRoot = obj.rootObject.GetComponent<BehaviourNode> ();
		TreeRoot.Tree = this;
		TreeRoot.ReCaptureChildsAndParents ();
		TreeVis.getTreeVis ().TreeRoot = TreeRoot;
		TreeVis.getTreeVis ().LoadTree ();
	}


}
