using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioSystem : MonoBehaviour {

	private AudioSource audioSource;

	public AudioClip initialCall;
	public AudioClip callReply;

	// Use this for initialization
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
