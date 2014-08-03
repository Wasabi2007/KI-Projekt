using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeVis : MonoBehaviour {
	
	public LeafNode node;

	public NodeVis parent;
	public List<NodeVis> childs;
	public TreeVis treeVis;
	public LinkVis linkToParent;

	public bool colaps = false;
	private bool oldColaps = false;
	private bool needSizeRecalced = true;

	private float childSizeBuffer = 0;


	// Use this for initialization
	void Start () {
	
	}

	public void toggleColaps(){
		colaps = !colaps;
	}

	public void init(LeafNode myNode){
		node = myNode;
		if (parent != null) {
			GameObject link = (GameObject)GameObject.Instantiate(treeVis.linkPrefab.gameObject);
			link.transform.parent = this.transform;
			link.transform.localScale = Vector3.one;
			link.transform.localPosition = Vector3.zero;
			linkToParent = link.GetComponent<LinkVis>();	
			linkToParent.setUpLine(Vector3.zero,parent.transform.position-transform.position);
		}

		childs = new List<NodeVis> ();
		if (myNode is ParentNode) {
			foreach(LeafNode LNode in ((ParentNode)myNode).childNodes){		
				GameObject go = treeVis.InstanceNodeGameobject (LNode.Name);			
				NodeVis nv = go.AddComponent<NodeVis>();
				go.transform.parent = this.transform;
				go.transform.localScale = Vector3.one;
				go.layer = this.gameObject.layer;
				nv.parent = this;
				nv.treeVis = treeVis;
				nv.init(LNode);
				childs.Add(nv);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (colaps != oldColaps) {
			needSizeRecalced = true;
			for(NodeVis p = parent; p != null; p = p.parent){
				p.needSizeRecalced = true;
			}
			foreach(NodeVis nVis in childs){
				nVis.gameObject.SetActive(!colaps);
			}
			treeVis.TreeVisRoot.calculatePosition(0);
			oldColaps = colaps;	
		}
	}

	public void calculatePosition(int index){


		float parentChildsize = (parent != null?parent.childSize():childSize());
		float neighborSize = 0;
		for (int neigbohr = 0; neigbohr < index; neigbohr++) {
			neighborSize += parent.childs[neigbohr].childSize();	
		}
		if (parent != null) {
						transform.localPosition = Vector3.down * treeVis.Y_Spacing + Vector3.left * (parentChildsize * 0.5f) + Vector3.right * neighborSize + Vector3.right * (childSize () * 0.5f);
			linkToParent.setUpLine (-Vector3.back*100, -transform.localPosition-Vector3.back*100);
				}
		if (!colaps) {
						int localIndex = 0;
						foreach (NodeVis nVis in childs) {
								nVis.calculatePosition (localIndex++);
						}
				}
	}

	public float childSize(){
		if (childSizeBuffer != 0 && !needSizeRecalced) {
			return childSizeBuffer;
		}
		childSizeBuffer = 0;
		needSizeRecalced = false;
		if (childs.Count <= 0 || colaps) {
				childSizeBuffer = treeVis.X_Spacing;
				return childSizeBuffer;
				} else {
					foreach(NodeVis nVis in childs){
						childSizeBuffer+=nVis.childSize();
					}
				return childSizeBuffer;
				}
	}
}
