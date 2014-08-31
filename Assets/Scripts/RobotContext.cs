using UnityEngine;
using System.Collections;

public class RobotContext : MonoBehaviour {

	public UIPopupList Kontext;

	private bool clicked = false;
	private GameObject currentClicked;
	private GameObject selectedRobot;
	// Use this for initialization
	void Start () {
		Kontext.items = TreeSaveManager.getTreeSaveManager().savedTrees;
		if (Kontext.items.Count > 0) {
						Kontext.value = Kontext.items [0];
				} else {
			Kontext.value = "";
		}

	}

	public void closeMenue(){
		bool notnew = false;
		if (selectedRobot != null) {
			BehaviourTree tree = selectedRobot.GetComponent<BehaviourTree> ();
			if (tree != null)
				if(Kontext.value.Equals(tree.lastLoadedTree))
					notnew = true;
				else
					tree.LoadTree (Kontext.value);
		}
		if (!notnew) {
			Kontext.Close ();
			NGUITools.SetActiveChildren (gameObject, false);
			currentClicked = null;
			clicked = false;
		}

	}
	
	// Update is called once per frame
	void Update () {

		if (UICamera.GetMouse (0).pressStarted) {
			currentClicked = UICamera.GetMouse (0).pressed;
			if(Kontext.gameObject.activeSelf){
				if (currentClicked != null && (currentClicked.transform.IsChildOf (transform) || currentClicked.CompareTag ("Robot")))
					NGUITools.SetActiveChildren (gameObject, true);
				else {
					Kontext.Close ();
					NGUITools.SetActiveChildren (gameObject, false);
				}
			}
		}

		if (UICamera.GetMouse (1).pressStarted) {
			currentClicked = UICamera.GetMouse (1).pressed;
			clicked = true;



			if(currentClicked != null && currentClicked.CompareTag ("Robot")){
				selectedRobot = currentClicked;

				BehaviourTree tree = selectedRobot.GetComponent<BehaviourTree> ();
				if (tree != null&& tree.lastLoadedTree.Length > 0){
					Kontext.value = tree.lastLoadedTree;
				}
			}

			if (currentClicked != null && (currentClicked.transform.IsChildOf (transform) || currentClicked.CompareTag ("Robot")))
				NGUITools.SetActiveChildren (gameObject, true);
			else {
				Kontext.Close ();
				NGUITools.SetActiveChildren (gameObject, false);
			}
		}


		if (currentClicked!= null && clicked && currentClicked.CompareTag("Robot")) {
         	transform.localPosition = NGUIMath.ScreenToPixels(UICamera.GetMouse (1).pos,UICamera.currentCamera.transform);
			NGUITools.SetActiveChildren(gameObject,true);
			clicked = false;
		}


	
	}
}
