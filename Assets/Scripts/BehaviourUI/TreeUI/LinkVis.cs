using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class LinkVis : MonoBehaviour {

	public LineRenderer lr;
	public ClippedModel cm;

	// Use this for initialization
	void Start () {
		if (lr == null)						
			lr = GetComponent<LineRenderer> ();

		if (cm == null)						
			cm = GetComponent<ClippedModel> ();
	}
	
	public void setUpLine(Vector3 begin, Vector3 end){//, Color color, float width, Material mat){
		/*lr.material = mat;
		lr.SetWidth (width, width);
		lr.SetColors (color, color);*/

		lr.SetPosition (0, begin);
		lr.SetPosition (1, end);

		//cm.Start ();
	}
}
