using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ParentNode : BehaviourInterface,IndexManager {
	List<LeafNode> childNodes{ get; set; }
}
