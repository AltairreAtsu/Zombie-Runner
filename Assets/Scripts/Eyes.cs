using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour {

	[Tooltip ("The Amount of FOV to remove from the camera to create a zoom effect.")]
	public float zoomLevel = 10;

	private Camera eyeCamera;
	private float baseFOV;

	// Use this for initialization
	void Start () {
		eyeCamera = gameObject.GetComponent<Camera> ();
		baseFOV = eyeCamera.fieldOfView;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Zoom") == 1){
			eyeCamera.fieldOfView = baseFOV - zoomLevel;

		} else {
			eyeCamera.fieldOfView = baseFOV;
		}
	}
}
