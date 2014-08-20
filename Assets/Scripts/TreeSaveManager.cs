using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(StoreInformation))]
[SerializeAll]
public class TreeSaveManager : MonoBehaviour {

	public List<string> savedTrees = new List<string>();

	private static TreeSaveManager singleton;

	public static TreeSaveManager getTreeSaveManager(){
		if (singleton == null) {
			GameObject TreeSaveManagerGo = new GameObject();
			singleton = TreeSaveManagerGo.AddComponent<TreeSaveManager>();
		}
		return singleton;
	}

	public static void deletSingleton(){
		if (singleton != null) {
			GameObject.Destroy(singleton.gameObject);
			singleton = null;
		}
	}

	public void AddSave(string name){
		if (!savedTrees.Contains (name)) {
			savedTrees.Add(name);
		}
	}

	// Use this for initialization
	void Awake () {
		if (singleton == null) {
						singleton = this;
				} else {
						GameObject.Destroy(gameObject);
				}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
