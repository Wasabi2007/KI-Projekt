using UnityEngine;
using System.Collections;
using System;


public class TaskAttribute {

	public String AttributeName;
	public TaskAttributeType AttributeType; 
	public EventDelegate eventDelegate;
	public String Value;

	public TaskAttribute(String attributeName,TaskAttributeType attributeType,String startValue, MonoBehaviour reciver, String functionName){
		AttributeType = attributeType;
		AttributeName = attributeName;
		Value = startValue;
		eventDelegate = new EventDelegate (reciver, functionName);
		if (eventDelegate.parameters.Length != 1) {
			Debug.LogError(reciver+": Function needs 1 parameter to recive input.");
		}
	}


}

public enum TaskAttributeType{
	STRING,
	INT,
	FLOAT,
	BOOL
}
