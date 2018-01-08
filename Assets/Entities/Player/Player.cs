using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	[SerializeField][Tooltip ("The Gun Gameobject with animator component.")]
	private Animator gunObjectAnimator = null;
	[SerializeField][Tooltip ("The Landing Zone Prefab to instantiate when the flare is dropped.")]
	private GameObject landingZonePrefab = null;
	[SerializeField][Tooltip ("The Total amount of times the player can be hit before they die.")]
	private int healthMax = 3;
	[SerializeField][Tooltip ("The Layer Number of the Terrain.")]
	private int layerNumber = 9; 
	[SerializeField][Tooltip ("The time that must pass after a hit before another can registered.")]
	private float invicabilityTime = 0.3f;
	[SerializeField][Tooltip ("How far off the ground the flare should be placed")]
	private float offset = 0.2f;


	private GameObject[] spawnPoints;
	private int healthCurrent;
	private float timeSinceHit = 0f;

	public delegate void OnPlayerHit();
	public OnPlayerHit playerHitObservers;

	void Start () {
		spawnPoints = GameObject.FindGameObjectsWithTag ("Spawn Point");
		healthCurrent = healthMax;
	}

	void Update(){
		timeSinceHit += Time.deltaTime;

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
		Ray ray = new Ray (transform.position, Vector3.down);
		RaycastHit rayHit;
		int layerMask = 1 << layerNumber;
		Physics.Raycast (ray, out rayHit, 10f, layerMask);

		Vector3 offsetPosition = transform.position + Vector3.down * (rayHit.distance - offset);
		Instantiate (landingZonePrefab, offsetPosition, transform.rotation);
	}

	public void Hit(int damage){
		if (timeSinceHit >= invicabilityTime) {
			healthCurrent -= damage;
			timeSinceHit = 0f;
			playerHitObservers ();

			if (healthCurrent <= 0) {
				Respawn ();
			}
		}
	}

	public void KillPlayer(){
		Hit (healthMax);
	}

	private void Respawn(){
		// Select Random Spawn point and Move to location
		GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

		transform.position = spawnPoint.transform.position;
		transform.rotation = Quaternion.identity;

		healthCurrent = healthMax;
	}

}
