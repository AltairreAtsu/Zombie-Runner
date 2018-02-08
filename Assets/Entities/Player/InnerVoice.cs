using UnityEngine;

public class InnerVoice : MonoBehaviour {
	[SerializeField]
    [Tooltip ("Audio Clip to be played at game start.")]
	private AudioClip intro;

	[SerializeField]
    [Tooltip ("Audio Clip to be played when the player finds a good landing area for the helicopter.")]
	private AudioClip goodLandingArea;

	private AudioSource audioSource;

	void Start () {
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = intro;
		audioSource.Play ();
	}
	
	void OnFindClearArea (){
		audioSource.clip = goodLandingArea;
		audioSource.Play ();
		Invoke ("CallHeli", goodLandingArea.length + 1f);
	}

	void CallHeli (){
		SendMessageUpwards ("OnMakeInitialHeliCall");
	}
}
