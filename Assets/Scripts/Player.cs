﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	[SerializeField][Tooltip ("The Gun Gameobject with animator component.")]
	private Animator gunObjectAnimator = null;
	[SerializeField][Tooltip ("The Landing Zone Prefab to instantiate when the flare is dropped.")]
	private GameObject landingZonePrefab = null;
	[SerializeField][Tooltip ("The Total amount of times the player can be hit before they die.")]
	private int healthMax = 3;
	[SerializeField][Tooltip ("The time that must pass after a hit before another can registered.")]
	private float invicabilityTime = 0.3f;

	private GameObject[] spawnPoints;
	private int healthCurrent;
	private float timeSinceHit = 0f;
	private bool respawn = false;

	public delegate void OnPlayerHit();
	public OnPlayerHit playerHitObservers;

	// Use this for initialization
	void Start () {
		spawnPoints = GameObject.FindGameObjectsWithTag ("Spawn Point");
		healthCurrent = healthMax;
	}

	void Update(){
		timeSinceHit += Time.deltaTime;

		if(respawn){
			Respawn ();
			respawn = false;
		}

		if(Input.GetAxis("Fire1") > 0.5f){
			gunObjectAnimator.SetBool ("Firing", true);
		} else {
			gunObjectAnimator.SetBool ("Firing", false);
		}
	}


	private void OnFindClearArea(){
		// Drop A flare
		Invoke("DropFlare", 3f);

	}

	private void DropFlare(){
		Instantiate (landingZonePrefab, transform.position, transform.rotation);
	}

	public void Hit(int damage){
		if (timeSinceHit >= invicabilityTime) {
			healthCurrent -= damage;
			timeSinceHit = 0f;
			playerHitObservers ();

			if (healthCurrent <= 0) {
				Die ();
			}
		}
	}

	private void Die(){
		respawn = true;
		// TODO Play Death Sound Clip
		Debug.Log ("Respawning the player!");
	}

	private void Respawn(){
		// Select Random Spawn point and Move to location
		GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

		transform.position = spawnPoint.transform.position;
		transform.rotation = Quaternion.identity;

		healthCurrent = healthMax;
	}

}
