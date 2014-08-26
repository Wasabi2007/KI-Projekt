using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NewChildNodeUI : MonoBehaviour {

	public UIPopupList ClassList;
	public UIInput SearchInput;
	public BehaviourNodeEditor Node;
	public AttributeEditor ae;

	public void initList(){
		if(ClassList.isOpen)
			ClassList.Close ();

		ClassList.items.Clear ();
		ClassList.items.Add("None");
		ClassList.items.AddRange (TreeVis.getTreeVis ().getFiltertClassListWithName (SearchInput.value));
	}
		
	public void addNode () {
		if (ClassList.value.Length > 0 && ClassList.value != "None") {
				Node.AddNode (ClassList.value);
				ae.UpdateNodes();
		}
	}
}
