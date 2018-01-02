using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ZombieLogic : MonoBehaviour {

	private Player Player;

	[SerializeField][Tooltip ("Range from which Zombies can hit the player.")]
	private float attackDistance = 2.5f;

	[SerializeField][Tooltip ("Number of hits a zombie can take before dying.")]
	private int health = 2;

	[SerializeField][Tooltip ("Time after a hit before another hit can be registered.")]
	private float invicabilityTime = 0.3f;
	private float timeSinceHit = 0f;

	void Start () {
		Player = GameObject.FindObjectOfType<Player> ();

		gameObject.GetComponent<AICharacterControl> ().target = Player.transform;
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceHit += Time.deltaTime;
		if(DistanceToPlayer() <= attackDistance){
			// Play Attack Animation
			Debug.Log ("Can Attack Player!");
			Player.Hit (1);
		}
	}

	public void Hit (int damage) {
		if (timeSinceHit >= invicabilityTime) {
			health -= damage;
			timeSinceHit = 0f;

			if (health <= 0) {
				// Play Death Animation
				Object.Destroy (gameObject);
			}
		}
	}

	private float DistanceToPlayer(){
		float distanceToPlayer;
		float xDistance = Mathf.Abs (Player.transform.position.x - transform.position.x);
		float zDistance = Mathf.Abs (Player.transform.position.z - transform.position.z);
		distanceToPlayer = xDistance + zDistance;

		return distanceToPlayer;
	}
}
