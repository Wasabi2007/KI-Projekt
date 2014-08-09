using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIWidget))]
public abstract class AttributeUI : MonoBehaviour {

	public UILabel attributeName;
	private UIWidget wiget;
	public int Width{get{return wiget.width;}}
	public int Height{get{return wiget.height;}}

	public virtual void Awake(){
		wiget = GetComponent<UIWidget> ();
	}

	public virtual void SetEventDelegate(TaskAttribute tAttribute){
		attributeName.text = tAttribute.AttributeName;
	}

}
