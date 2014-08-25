using UnityEngine;
using System.Collections;

public class BehaviourTree : MonoBehaviour,TreeSaveManager.TreeChangeObserver {

	public GameObject Owner;
	public BehaviourNode TreeRoot;
	public bool StartTree = false;
	public static bool StartAll = false;
	public string lastLoadedTree = "";

	// Use this for initialization
	void Start () {
		if(TreeRoot!=null)
			TreeRoot.Tree = this;
	}

	public void StartBehaviour(){
		TreeRoot.IsActive = false;
		TreeRoot.enabled = false;
		TreeRoot.IsActive = true;
		TreeRoot.enabled = true;
	}

	// Update is called once per frame
	void Update () {
		if (StartTree || StartAll) {
			StartTree = false;
			StartBehaviour();
		}

	}
	void LateUpdate(){
		if (StartAll) {
			StartAll = false;
		}
	}

	public void StartAllTrees(){
		StartAll = true;
	}

	public virtual void LoadTree(string FileName){
		if (FileName.Length == 0)
			return;

		if (TreeRoot != null) {
			GameObject.DestroyImmediate (TreeRoot.gameObject);
			TreeRoot=null;
		}
		
		LevelSerializer.LoadObjectTreeFromFile(FileName,loaded);
		lastLoadedTree = FileName;
		TreeSaveManager.getTreeSaveManager ().AddObserver (FileName, this);

		//LevelSerializer.Collect ();
		//LevelSerializer.LoadObjectTreeFromFile("Pah.dat");
	}

	public virtual void updateTree(){
		LoadTree (lastLoadedTree);
	}

	private void loaded(LevelLoader obj){
		//obj.DontDelete = true;
		/*foreach(EmptyObjectIdentifier eoi in obj.rootObject.GetAllComponentsInChildren<EmptyObjectIdentifier>()){
			GameObject.Destroy(eoi);
		}*/
		GameObject go = (GameObject)GameObject.Instantiate (obj.rootObject);
		//obj.rootObject.SetActive (false);
		TreeRoot = go.GetComponent<BehaviourNode> ();
		TreeRoot.Tree = this;
		TreeRoot.ReCaptureChildsAndParents ();
	}

}
