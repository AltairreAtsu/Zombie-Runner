using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingZone : MonoBehaviour {

	public delegate void OnVictory();
	public OnVictory OnVictoryObservers;

	public bool winMode { get; set;}

	// Use this for initialization
	void Start () {
		winMode = false;
	}
	
	void OnTriggerEnter(Collider coll){
		if(coll.tag == "Player" && winMode){
			OnVictoryObservers ();
		}
	}
}
