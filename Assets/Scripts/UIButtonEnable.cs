using UnityEngine;
using System.Collections;

public class UIButtonEnable : MonoBehaviour {

	public UIButton Target;
	public bool state = true;
	
	void OnClick () { if (Target != null) Target.isEnabled = state; }	
}
