using UnityEngine;
using System.Collections;

public class TreeSaveInit : MonoBehaviour {

	public string SavedFileName = "list";

	private static TreeSaveInit me;

	// Use this for initialization
	void Awake () {
		if (me != null) {
			GameObject.Destroy(this.gameObject);
			return;
		}
		me = this;
		TreeSaveManager.deletSingleton ();
		try{
			LevelSerializer.LoadObjectTreeFromFile(SavedFileName);
		}catch{
			TreeSaveManager.getTreeSaveManager();	
		}
	}

	void OnApplicationQuit(){
		LevelSerializer.SaveObjectTreeToFile(SavedFileName,TreeSaveManager.getTreeSaveManager().gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
