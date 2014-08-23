using UnityEngine;
using System.Collections;

public class StopAttackingTask : Task {

	// Use this for initialization
	void Start () {
	
	}

	public override void Activate ()
	{
		base.Activate ();
		//Owner.GetComponent<Robot> ().pastBattleStatus = Owner.GetComponent<Robot> ().myBattleStatus;
		//Wenn vorher auch schon ein Attackstatus war, wird durch StopAttcking der Roboter auf NotInBattleVersetzt
		if (Owner.GetComponent<Robot> ().pastBattleStatus == BattleStatus.AttackingFar)
						Owner.GetComponent<Robot> ().pastBattleStatus = BattleStatus.NotInBattle;

		Owner.GetComponent<Robot> ().myBattleStatus = Owner.GetComponent<Robot> ().pastBattleStatus;
		
	}

	// Update is called once per frame
	void Update () {
	
	}
}
