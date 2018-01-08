using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerVoice : MonoBehaviour {
	[SerializeField][Tooltip ("Introduction Clip to be played at game start.")]
	private AudioClip whatHappened;
	[SerializeField][Tooltip ("Clip to be played when the player finds a good landing area for the helicopter.")]
	private AudioClip goodLandingArea;

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		audioSource.clip = whatHappened;
		audioSource.Play ();
	}
	
	void OnFindClearArea (){
		Debug.Log (name + " OnFindClearArea");
		audioSource.clip = goodLandingArea;
		audioSource.Play ();
		Invoke ("CallHeli", goodLandingArea.length + 1f);
	}

	void CallHeli (){
		SendMessageUpwards ("OnMakeInitialHeliCall");
	}
}
