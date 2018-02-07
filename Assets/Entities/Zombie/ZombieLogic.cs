using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ZombieLogic : MonoBehaviour {
	[SerializeField]
    [Tooltip ("Range from which Zombies can hit the player.")]
	private float attackDistance = 2.5f;

    [SerializeField]
    [Tooltip ("Delay from playing the attack sound to damaging the player.")]
    private float attackDelay = 1f;

    [SerializeField]
    [Tooltip("Damage done by zombie attacks.")]
    private float damage = 1f;

    [Space]

	[SerializeField]
    [Tooltip ("Number of hits a zombie can take before dying.")]
	private float health = 2;

	[SerializeField]
    [Tooltip ("Time after a hit before another hit can be registered.")]
	private float invicabilityTime = 0.3f;

    [Space]

    [SerializeField]
    [Tooltip ("Store the refrence to the Hip bone here.")]
    private Transform hipTransform = null;

    [Space]

    [SerializeField]
    [Tooltip ("Sound Effects to be played when the zombie starts attacking.")]
    private AudioClip[] attackSounds = null;

	private AudioSource audioSource = null;
	private Player Player = null;
    private bool attacking = false;
	private float timeSinceHit = 0f;

	public delegate void OnZombieKilled(Transform zombiePostion);
	public static OnZombieKilled OnZombieKilledObservers = null;

	private void Start () {
		Player = GameObject.FindObjectOfType<Player> ();
		audioSource = GetComponent<AudioSource> ();

		gameObject.GetComponent<AICharacterControl> ().target = Player.transform;
	}

	private void Update ()
    {
		timeSinceHit += Time.deltaTime;
		if(DistanceToPlayer() <= attackDistance && !attacking)
        {
            // TODO Play Attack Animation
            attacking = true;
            audioSource.clip = attackSounds[Random.Range(0, attackSounds.Length)];
            audioSource.Play();

            float delay = attackDelay;

            if(audioSource.clip.length > attackDelay)
            {
                delay = attackDelay + (audioSource.clip.length - attackDelay);
            }

            Invoke("Attack", delay);
		}
	}

    private void Attack()
    {
        attacking = false;
        if (DistanceToPlayer() <= attackDistance)
        {
            Player.Hit(damage);
        }
    }

	public void Hit (float damage) {
		if (timeSinceHit >= invicabilityTime) {
			health -= damage;
			timeSinceHit = 0f;

			if (health <= 0) {
				// TODO Play Death Animation
				OnZombieKilledObservers(hipTransform);
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
