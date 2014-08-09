using UnityEngine;
using System.Collections;

public class AttributeToggelUI : AttributeUI {

	public UIToggle InputField;
	
	public override void SetEventDelegate (TaskAttribute tAttribute)
	{
		base.SetEventDelegate (tAttribute);
		tAttribute.eventDelegate.parameters [0].obj = InputField;
		InputField.value = bool.Parse(tAttribute.Value);
		InputField.onChange.Add (tAttribute.eventDelegate);
	}
}
