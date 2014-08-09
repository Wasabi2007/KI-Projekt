using UnityEngine;
using System.Collections;

public class IndexUpdate : MonoBehaviour {

	public TreeVis treeVis;
	public UILabel IndexDisplay;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (treeVis.SelectedNode != null) {
						IndexDisplay.text = treeVis.SelectedNode.index.ToString();
				} else {
						IndexDisplay.text = "";
				}
	}
}
