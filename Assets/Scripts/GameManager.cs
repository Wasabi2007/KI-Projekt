using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public interface IReset{
		void Reset();
	}

	public List<IReset> ObjectsToReset = new List<IReset>();

	private static GameManager singleton;

	public static GameManager getGameManager(){
		if(singleton == null){
			GameObject gamemanagerobject = new GameObject("GameManager");
			singleton = gamemanagerobject.AddComponent<GameManager>();
		}
		return singleton;
	}

	void Awake(){
		if(singleton == null){
			singleton = this;
		}
		else{
			GameObject.Destroy(gameObject);
		}
	}

	public void addResetter(IReset r){
		ObjectsToReset.Add(r);
	}

	public void removeResetter(IReset r){
		if(ObjectsToReset.Contains(r))
			ObjectsToReset.Remove(r);
	}

	public void ResetObjects(){
		foreach(IReset r in ObjectsToReset){
			if(r != null)
				r.Reset();
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
