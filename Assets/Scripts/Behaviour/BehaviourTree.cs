using UnityEngine;
using System.Collections;

public class BehaviourTree : MonoBehaviour {

	public GameObject Owner;
	public BehaviourNode TreeRoot;
	public bool StartTree = false;

	// Use this for initialization
	void Start () {
		TreeRoot.Tree = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (StartTree) {
			StartTree = false;
			TreeRoot.IsActive = true;
			TreeRoot.enabled = true;
		}
	
	}
}
