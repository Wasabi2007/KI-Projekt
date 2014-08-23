using UnityEngine;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System;

[ExecuteInEditMode]
public class ConditionTask : MonoBehaviour {

	public string Class = "Robot";
	public string[] Values;

	public int SelectetValue = 0;
	public Condition comp;
	public String CompareValue;

	public bool Refresh = true;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Refresh) {
			Refresh = false;	

			Type type = Type.GetType(Class);
			Values = new string[type.GetFields().Length];
			int index = 0;
			foreach(FieldInfo pi in type.GetFields()){
				Debug.Log (pi.Name);
				Values[index++] = pi.Name;
			}			

		}
	
	}
}
