using UnityEngine;
using System.Collections;

public class UIListItem : MonoBehaviour {

	public UILabel lable;
	public UIList list;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Selected(){
		list.selected = lable.text;
	}
}
