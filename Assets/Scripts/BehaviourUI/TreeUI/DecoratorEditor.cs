using UnityEngine;
using System.Collections;

public class DecoratorEditor : BehaviourNodeEditor {

	public UILabel infoLable;

	public override void Start ()
	{
		base.Start ();
	}

	public override void Update ()
	{
		base.Update ();

		infoLable.text = NV.node.Info;
	}
}
