using UnityEngine;
using System.Collections;

public class AttributeInputUI : AttributeUI {

	public UIInput InputField;

	public override void SetEventDelegate (TaskAttribute tAttribute)
	{
		base.SetEventDelegate (tAttribute);
		tAttribute.eventDelegate.parameters [0].obj = InputField;
		InputField.value = tAttribute.Value;
		InputField.onSubmit.Add (tAttribute.eventDelegate);
	}
}
