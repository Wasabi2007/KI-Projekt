using UnityEngine;
using System.Collections;

public class StartAttacking : Task {

	// Use this for initialization
	public override void Awake ()
	{
		base.Awake ();
		info = "Start Attacking";
	}

	public override void Activate ()
	{
		base.Activate ();
		Owner.GetComponent<Robot> ().pastBattleStatus = Owner.GetComponent<Robot> ().myBattleStatus;
		Owner.GetComponent<Robot> ().myBattleStatus = BattleStatus.AttackingFar;
		
	}

	// Update is called once per frame
	void Update () {
				base.Update ();
				Debug.Log ("Robot is in Attackmode");

				parentNode.ChildTerminated (this, true);
		}
}
