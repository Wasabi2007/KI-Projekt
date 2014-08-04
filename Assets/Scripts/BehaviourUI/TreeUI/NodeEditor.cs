using UnityEngine;
using System.Collections;

public class NodeEditor : MonoBehaviour {
	
	public int index;
	public IndexManager Node;

	public bool indexUpToggle = false;
	public bool indexDownToggle = false;

	void indexUp(){
		Node.Index = Node.Index - 1;
	}

	void indexDown(){
		Node.Index = Node.Index + 1;
	}
	// Use this for initialization
	void Start () {
		Node = GetComponent<BehaviourNode> ();
		if (Node == null) {
			Node = GetComponent<Task> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (indexUpToggle) {
			indexUpToggle = false;
			indexUp();
		}
		if (indexDownToggle) {
			indexDownToggle = false;
			indexDown();
		}

	}
}
