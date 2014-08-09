using UnityEngine;
using System.Collections;

public class ChildNodeUI : MonoBehaviour {

	public UILabel NodeName;
	public UIButton DelButton;
	public NodeEditor node;
	public AttributeEditor ae;

	public void delNode(){
		node.remove ();
		ae.UpdateNodes ();
	}
}
