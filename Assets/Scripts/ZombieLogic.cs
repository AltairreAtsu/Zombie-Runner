using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ZombieLogic : MonoBehaviour {
	[SerializeField][Tooltip ("Range from which Zombies can hit the player.")]
	private float attackDistance = 2.5f;
	[SerializeField][Tooltip ("Number of hits a zombie can take before dying.")]
	private int health = 2;
	[SerializeField][Tooltip ("Time after a hit before another hit can be registered.")]
	private float invicabilityTime = 0.3f;
	[SerializeField][Tooltip ("Sound Clip to be played when the zombie dies.")]
	private AudioClip deathClip = null;

	private AudioSource audioSource = null;
	private Player Player;
	private float timeSinceHit = 0f;

	public delegate void OnZombieKilled(Transform zombiePostion);
	public static OnZombieKilled OnZombieKilledObservers;

	void Start () {
		Player = GameObject.FindObjectOfType<Player> ();
		audioSource = GetComponent<AudioSource> ();

		gameObject.GetComponent<AICharacterControl> ().target = Player.transform;
	}

	void Update () {
		timeSinceHit += Time.deltaTime;
		if(DistanceToPlayer() <= attackDistance){
			// TODO Play Attack Animation
			Player.Hit (1);
		}
	}

	public void Hit (int damage) {
		if (timeSinceHit >= invicabilityTime) {
			health -= damage;
			timeSinceHit = 0f;

			if (health <= 0) {
				// TODO Play Death Animation
				OnZombieKilledObservers(transform);
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
