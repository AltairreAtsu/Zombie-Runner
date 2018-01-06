using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour {

	[SerializeField][Tooltip ("Sound Effect to played when the Helicopter responds to player call.")]
	private AudioClip heliInitialCallResponse;

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

	private void Update(){
		if(transform.position.z > -100){
			// Helicopter close to playspace
			// Anounce Helicopter Arrival
			// Move towards LZ
		}
	}
}
