using UnityEngine;
using System.Collections;

public class ToggelText : MonoBehaviour {

	public UILabel label;
	[Multiline]
	public string[] text;

	private int index = 0;
	public void Start(){
		label.text = text [index];
	}

	public void toggle(){
		index = (index+1) % text.Length;
		label.text = text [index];
	}
}
