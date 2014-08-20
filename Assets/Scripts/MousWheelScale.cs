using UnityEngine;
using System.Collections;

public class MousWheelScale : MonoBehaviour {

	public float DefaultScale = 1;
	public float MinScale = 0;
	public float MaxScale = 2;
	public float ScaleStepps = 0.001f;

	private float accScale = 1;


	// Use this for initialization
	void Start () {
		accScale = DefaultScale;
	}
	
	// Update is called once per frame
	void Update () {
		float scale = Input.GetAxis ("Mouse ScrollWheel");
		accScale += scale * ScaleStepps;
		accScale = Mathf.Clamp (accScale, MinScale, MaxScale);
		transform.localScale = Vector3.one * accScale;
	
	}
}
