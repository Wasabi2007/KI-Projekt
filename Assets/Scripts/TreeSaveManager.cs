using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(StoreInformation))]
[SerializeAll]
public class TreeSaveManager : MonoBehaviour {

	public interface TreeChangeObserver{
		void updateTree();
	}

	public List<string> savedTrees = new List<string>();

	[DoNotSerialize]
	Dictionary<string,List<TreeChangeObserver>> trees = new Dictionary<string, List<TreeChangeObserver>> ();

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

	public void SaveTree(string name,GameObject treeRoot){
		if (!savedTrees.Exists(delegate(string n){return name.Equals(n);})) {
			savedTrees.Add(name);
		}
		LevelSerializer.SaveObjectTreeToFile (name, treeRoot);
		if (trees.ContainsKey (name)) {
						TreeChangeObserver[] toUpdate = trees [name].ToArray ();
						foreach (TreeChangeObserver observer in  toUpdate) {
								observer.updateTree ();
						}
				}
	}

	public void AddObserver(string name, TreeChangeObserver observer){
		foreach (KeyValuePair<string,List<TreeChangeObserver>> kvp in trees) {
			if(kvp.Value.Contains(observer)){
				kvp.Value.Remove(observer);
			}
		}

		if (!trees.ContainsKey (name)) {
			trees.Add (name, new List<TreeChangeObserver> ());		
		} 
		if(!trees[name].Contains(observer)){
			trees [name].Add (observer);
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
