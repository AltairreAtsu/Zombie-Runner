using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour {

	[SerializeField][Tooltip ("The Amount of FOV to remove from the camera to create a zoom effect.")]
	private float zoomLevel = 10;
	private Camera eyeCamera;
	private float baseFOV;

	[SerializeField]
	private Animator gunAnimator;

	// Use this for initialization
	void Start () {
		eyeCamera = gameObject.GetComponent<Camera> ();
		baseFOV = eyeCamera.fieldOfView;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Fire2") > 0.5f){
			eyeCamera.fieldOfView = baseFOV - zoomLevel;

			if ( !gunAnimator.GetBool("Zoomed") ){
				gunAnimator.SetBool ("Zoomed", true);
			}
				
		} else {
			eyeCamera.fieldOfView = baseFOV;

			if ( gunAnimator.GetBool("Zoomed") ){
				gunAnimator.SetBool ("Zoomed", false);
			}
		}
	}
}
