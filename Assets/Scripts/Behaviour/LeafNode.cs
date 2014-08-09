using UnityEngine;
using System.Collections;

public interface LeafNode : BehaviourInterface,IndexManager{
	ParentNode parentNode{ get; set; }
	string Name{ get; }
	string Info{ get; }
}
