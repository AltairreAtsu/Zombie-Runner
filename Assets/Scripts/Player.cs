using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	private GameObject[] spawnPoints;
	private bool respawn = false;

	public GameObject LandingZone;

	// Use this for initialization
	void Start () {
		spawnPoints = GameObject.FindGameObjectsWithTag ("Spawn Point");
	}

	void Update(){
		if(respawn){
			Respawn ();
			respawn = false;
		}
	}

	private void OnFindClearArea(){
		// Drop A flare
		Invoke("DropFlare", 3f);

	}

	private void DropFlare(){
		Debug.Log ("Dropped A flare!");
		Instantiate (LandingZone, transform.position, transform.rotation);
	}

	private void Respawn(){
		// Select Random Spawn point and Move to location
		GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

		transform.position = spawnPoint.transform.position;
		transform.rotation = Quaternion.identity;
	}

}
