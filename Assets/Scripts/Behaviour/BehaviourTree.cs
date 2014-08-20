using UnityEngine;
using System.Collections;

public class BehaviourTree : MonoBehaviour {

	public GameObject Owner;
	public BehaviourNode TreeRoot;
	public bool StartTree = false;
	public bool Save = false;
	public bool Load = false;

	// Use this for initialization
	void Start () {
		if(TreeRoot!=null)
			TreeRoot.Tree = this;
	}

	public void StartBehaviour(){
		TreeRoot.IsActive = true;
		TreeRoot.enabled = true;
	}

	// Update is called once per frame
	void Update () {
		if (StartTree) {
			StartTree = false;
			StartBehaviour();
		}
		if (Save) {
			Save = false;
			SaveTree("Pah.dat");
		}
		if (Load) {
			Load = false;
			LoadTree("Pah.dat");
		}
	}

	public void SaveTree(string FileName){
		if (FileName.Length == 0)
				return;

		LevelSerializer.SaveObjectTreeToFile (FileName, TreeRoot.gameObject);
		TreeSaveManager.getTreeSaveManager ().savedTrees.Add (FileName);
		//Debug.Log ("Saved: " + FileName);
	}

	public void LoadTree(string FileName){
		if (FileName.Length == 0)
			return;

		if (TreeRoot != null) {
			GameObject.DestroyImmediate (TreeRoot.gameObject);
			TreeVis.getTreeVis ().DestroyTree ();
			TreeRoot=null;
			TreeVis.getTreeVis ().TreeRoot = null;
		}

		LevelSerializer.LoadObjectTreeFromFile(FileName,loaded);

		//LevelSerializer.Collect ();
		//LevelSerializer.LoadObjectTreeFromFile("Pah.dat");
	}

	void loaded(LevelLoader obj){
		TreeRoot = obj.rootObject.GetComponent<BehaviourNode> ();
		TreeRoot.Tree = this;
		TreeRoot.ReCaptureChildsAndParents ();
		TreeVis.getTreeVis ().TreeRoot = TreeRoot;
		TreeVis.getTreeVis ().LoadTree ();
	}

}
