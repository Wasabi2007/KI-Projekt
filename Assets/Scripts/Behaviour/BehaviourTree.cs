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
			SaveTree();
		}
		if (Load) {
			Load = false;
			LoadTree();
		}
	}

	public void SaveTree(){
		LevelSerializer.SaveObjectTreeToFile ("Pah.dat", TreeRoot.gameObject);
	}

	public void LoadTree(){
		LevelSerializer.LoadObjectTreeFromFile("Pah.dat");

	}
}
