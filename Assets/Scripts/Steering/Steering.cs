using UnityEngine;
using System.Collections;

public abstract class Steering : MonoBehaviour {

	public Robot Owner;


	// Use this for initialization
	void Start () {
		if (!Owner) {
			Debug.LogError("No Owner");		
			this.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
