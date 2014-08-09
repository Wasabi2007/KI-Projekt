using UnityEngine;
using System.Collections;

public class NodeNameUpdate : MonoBehaviour {

	public TreeVis treevis;
	public UILabel label;

	private string defaultString = "";

	// Use this for initialization
	void Start () {
		defaultString = label.text;
	}
	
	// Update is called once per frame
	void Update () {
		if (treevis.SelectedNode != null) {
						label.text = treevis.SelectedNode.NV.node.Info;
				} else {
						label.text = defaultString;
				}
	}
}
