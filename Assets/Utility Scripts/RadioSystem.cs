using UnityEngine;

public class RadioSystem : MonoBehaviour {
	[SerializeField]
    [Tooltip ("Clip to be played when the player calls the Helicopter")]
	private AudioClip initialCall = null;

	[SerializeField]
    [Tooltip ("Clip to be played when the Helicopter responds to the call.")]
	private AudioClip callReply = null;

	[SerializeField]
    [Tooltip ("Clip to be played when the Helicopter Spawns.")]
	private AudioClip seeFlare = null;

	[SerializeField]
    [Tooltip ("Clip to be played when the Helicopter Arives at the LZ.")]
	private AudioClip arrivedAtLZ = null;

	[SerializeField]
    [Tooltip ("Clip to be played when the player wins.")]
	private AudioClip victoryClip = null;

	private AudioSource audioSource = null;
	private LandingZone landingZone = null;
	private LevelManager levelManager = null;

	void Start ()
    {
		audioSource = GetComponent<AudioSource> ();
        audioSource.volume = PlayerPrefsManager.GetDialougeVolume();
		levelManager = GetComponent<LevelManager> ();
	}

	void OnMakeInitialHeliCall ()
    {
		audioSource.clip = initialCall;
		audioSource.Play ();
		Invoke ("OnCallReply", initialCall.length + 1f);
	}

	void OnCallReply ()
    {
		audioSource.clip = callReply;
		audioSource.Play ();
		BroadcastMessage ("OnDispatchHelicopter");
		landingZone = GameObject.FindObjectOfType<LandingZone> ();
		landingZone.OnVictoryObservers += OnVictory;
	}

	void OnHelicopterLanded ()
    {
		audioSource.clip = arrivedAtLZ;
		audioSource.Play ();
	}

	void OnHelicopterSpawned ()
    {
		audioSource.clip = seeFlare;
		audioSource.Play ();
	}

	void OnVictory ()
    {
		audioSource.clip = victoryClip;
		audioSource.Play ();
		Invoke ("FinishLevel", victoryClip.length);
	}

	void FinishLevel()
    {
		levelManager.LoadLevel (2);
	}
}
