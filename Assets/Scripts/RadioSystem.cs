using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioSystem : MonoBehaviour {
	[SerializeField][Tooltip ("Clip to be played when the player calls the Helicopter")]
	private AudioClip initialCall;
	[SerializeField][Tooltip ("Clip to be played when the Helicopter responds to the call.")]
	private AudioClip callReply;

	private AudioSource audioSource;

	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}

	void OnMakeInitialHeliCall () {
		audioSource.clip = initialCall;
		audioSource.Play ();
		Invoke ("OnCallReply", initialCall.length + 1f);
	}

	void OnCallReply () {
		audioSource.clip = callReply;
		audioSource.Play ();
		BroadcastMessage ("OnDispatchHelicopter");
	}
}
