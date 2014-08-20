using UnityEngine;
using System.Collections;

public interface BehaviourInterface  {

	bool IsActive {get; set;}
	GameObject Owner {get; set;}
	GameObject HirachiOwner {get;}
	BehaviourTree Tree {get; set;}
	void Activate ();
	void Deactivate ();
	void ChildTerminated (BehaviourInterface child,bool result);
	void ReCaptureChildsAndParents();
}
