using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewChildNodeUI : MonoBehaviour {

	public UIPopupList ClassList;
	public BehaviourNodeEditor Node;
	public AttributeEditor ae;

	public void initList(TreeVis.NodeAddBinding[] nameList){
		ClassList.items.Add("None");
		foreach (TreeVis.NodeAddBinding bind in nameList) {
			ClassList.items.Add(bind.Display);	
		}
	}
		
	public void addNode () {
		if (ClassList.value.Length > 0 && ClassList.value != "None") {
				Node.AddNode (ClassList.value);
				ae.UpdateNodes();
		}
	}
}
