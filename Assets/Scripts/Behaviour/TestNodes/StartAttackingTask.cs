using UnityEngine;
using System.Collections;

public class StartAttackingTask : Task {

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
		if (Owner.GetComponent<Robot> ().myBattleStatus == BattleStatus.AttackingFar) {
						parentNode.ChildTerminated (this, true);
				}
		else {
						parentNode.ChildTerminated (this, false);
				}
		}
}
