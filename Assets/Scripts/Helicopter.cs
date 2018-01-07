using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour {
	[SerializeField][Tooltip ("The time in minutes before the Helicopter Arrives.")]
	private float arrivalTime = 5f;
	[SerializeField][Tooltip ("Helicopter Move Speed")]
	private float speed = 10f;
	[SerializeField][Tooltip ("Speed at which the Helicopter Decends.")]
	private float landingSpeed = 10f;
	[SerializeField][Tooltip ("Layer number for the terrain.")]
	private int layerNumber = 9;
	[SerializeField][Tooltip ("How far off the ground the Helicopter should be when landed.")]
	private float offset = 1f;
	[SerializeField][Tooltip ("The Helicopter Mesh Object.")]
	private GameObject HelicopterMesh = null;


	private LandingZone landingZone;
	private bool called = false;
	private bool dispatched = false;

	public float passedTime { get; set; }

	// Use this for initialization
	private void Start () {

		passedTime = 0f;
		arrivalTime = arrivalTime * 60;
		HelicopterMesh.SetActive (false);
	}
	
	// Call Method
	public void OnDispatchHelicopter () {
		Debug.Log ("Helicopter Dispatched!");
		landingZone = GameObject.FindObjectOfType<LandingZone>();
		called = true;
	}

	private void Update(){
		if(called){
			passedTime += Time.deltaTime;
			if(passedTime > arrivalTime){
				Dispatch ();
				called = false;
				return;
			}
		}

		if(!dispatched){
			return;
		}

		float distanceX = Mathf.Abs ( transform.position.x - landingZone.transform.position.x );
		float distanceZ = Mathf.Abs ( transform.position.z - landingZone.transform.position.z );
		float totalDistance = distanceX + distanceZ;

		if (totalDistance > 5) {
			transform.Translate (Vector3.forward * speed * Time.deltaTime);
			return;
		}
			
		if(GetDistanceToGround() > 0 ){
			transform.Translate (Vector3.down * landingSpeed * Time.deltaTime);
			return;
		}

		// Helicopter Has Landed
		// TODO Play Helicopter Landed
		if(landingZone.winMode == false){
			SendMessageUpwards("OnHelicopterLanded");
			landingZone.winMode = true;
		}
	}

	private void Dispatch(){
		HelicopterMesh.SetActive (true);

		transform.LookAt(landingZone.gameObject.transform.position);
		transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);
		dispatched = true;

		// TODO play Helicopter Sees Flare
		SendMessageUpwards("OnHelicopterSpawned");
	}

	private float? GetDistanceToGround(){
		Ray ray = new Ray (transform.position, Vector3.down);
		RaycastHit rayHit;
		int layerMask = 1 << layerNumber;

		if(Physics.Raycast(ray, out rayHit, 300f, layerMask)){
			return rayHit.distance - offset;
		}

		return null;
	}

	public float GetArrivalTime(){
		return arrivalTime;
	}
}
