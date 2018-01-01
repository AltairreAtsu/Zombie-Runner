﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour {

	public AudioClip heliInitialCallResponse;

	private bool called = false;

	private Rigidbody heliRigidbody;

	// Use this for initialization
	private void Start () {
		heliRigidbody = gameObject.GetComponent<Rigidbody> ();
	}
	
	// Call Method
	public void OnDispatchHelicopter () {
		Debug.Log ("Helicopter Dispatched!");
		called = true;
		heliRigidbody.velocity = new Vector3 (0f, 0f, 50f);
	}
}
