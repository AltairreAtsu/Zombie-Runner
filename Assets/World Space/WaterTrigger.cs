using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrigger : MonoBehaviour {
	void OnTriggerEnter (Collider coll) {
		if(coll.gameObject.tag == "Player"){
			coll.GetComponent<Player> ().KillPlayer ();
		}
	}

}
