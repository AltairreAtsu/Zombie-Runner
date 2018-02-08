using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    [SerializeField]
    [Tooltip ("The Total amount of times the player can be hit before they die.")]
	private float healthMax = 3;

    [SerializeField]
    [Tooltip("The time that must pass after a hit before another can registered.")]
    private float invicabilityTime = 0.3f;

    [Space]

    [SerializeField]
    [Tooltip("The Landing Zone Prefab to instantiate when the flare is dropped.")]
    private GameObject landingZonePrefab = null;

    [SerializeField]
    [Tooltip("The Layer Number of the Terrain.")]
    private int layerNumber = 9;

    [SerializeField]
    [Tooltip ("How far off the ground the flare should be placed")]
	private float offset = 0.2f;

    [Space]

    [SerializeField]
    [Tooltip ("The priority on the Audiosource to be used for the player hurt and die sounds.")]
    private int audioSourcePriority = 126;

    [SerializeField]
    [Tooltip ("Store all sound effects to be played on player hurt here.")]
    private AudioClip[] hurtSounds = null;

    [SerializeField]
    [Tooltip ("Store all sound effects to be played on player death here.")]
    private AudioClip[] dieSounds = null;

	private float healthCurrent;
	private float timeSinceHit = 0f;

    private AudioSource audioSource = null;
    private Animator gunAnimator = null;
    private GameObject[] spawnPoints = null;

    public delegate void OnPlayerHit();
	public OnPlayerHit playerHitObservers;
#endregion 

    void Start ()
    {
		spawnPoints = GameObject.FindGameObjectsWithTag ("Spawn Point");
        gunAnimator = transform.GetComponentInChildren<Animator>();

        foreach (AudioSource source in GetComponents<AudioSource>())
        {
            source.volume = PlayerPrefsManager.GetSFXVolume();
            if (source.priority == audioSourcePriority)
            {
                audioSource = source;
            }
        }

        healthCurrent = healthMax;
        Respawn();
	}

	void Update()
    {
		timeSinceHit += Time.deltaTime;

		if(Input.GetAxis("Fire1") > 0.5f)
			gunAnimator.SetBool ("Firing", true);
        else
			gunAnimator.SetBool ("Firing", false);
	}

	private void OnFindClearArea()
    {
		Invoke("DropFlare", 3f);

	}

	private void DropFlare()
    {
		Ray ray = new Ray (transform.position, Vector3.down);
		RaycastHit rayHit;
		int layerMask = 1 << layerNumber;
		Physics.Raycast (ray, out rayHit, 10f, layerMask);

		Vector3 offsetPosition = transform.position + Vector3.down * (rayHit.distance - offset);
		Instantiate (landingZonePrefab, offsetPosition, transform.rotation);
	}

	public void Hit(float damage)
    {
		if (timeSinceHit >= invicabilityTime)
        {
			healthCurrent -= damage;
			timeSinceHit = 0f;
			playerHitObservers ();

            if (DoDeathCheck())
                return;

            audioSource.clip = hurtSounds[Random.Range(0, hurtSounds.Length)];
            audioSource.Play();
        }
	}

    private bool DoDeathCheck()
    {
        if (healthCurrent <= 0)
        {
            audioSource.clip = dieSounds[Random.Range(0, dieSounds.Length)];
            audioSource.Play();
            Respawn();
            return true;
        }
        return false;
    }

	public void KillPlayer()
    {
		Hit (healthMax);
	}

	private void Respawn()
    {
		GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

		transform.position = spawnPoint.transform.position;
		transform.rotation = Quaternion.identity;

		healthCurrent = healthMax;
	}

    public float getHealthPercentage()
    {
        return healthCurrent / healthMax;
    }

}
