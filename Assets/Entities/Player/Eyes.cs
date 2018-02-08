using UnityEngine;

public class Eyes : MonoBehaviour
{

	[SerializeField]
    [Tooltip ("The Amount of FOV to remove from the camera to create a zoom effect.")]
	private float zoomLevel = 10;

    private Animator gunAnimator = null;
    private Camera eyeCamera;

	private float baseFOV;



	void Start ()
    {
        Transform player = GameObject.FindWithTag("Player").transform;
        gunAnimator = player.GetComponentInChildren<Animator>();

        eyeCamera = Camera.main;
		baseFOV = eyeCamera.fieldOfView;
	}
	
	void Update ()
    {
		if(Input.GetAxis("Fire2") > 0.5f && !gunAnimator.GetBool("Reloading"))
        {
			eyeCamera.fieldOfView = baseFOV - zoomLevel;

			if ( !gunAnimator.GetBool("Zoomed") )
				gunAnimator.SetBool ("Zoomed", true);
				
		} else
        {
			eyeCamera.fieldOfView = baseFOV;

			if ( gunAnimator.GetBool("Zoomed") )
				gunAnimator.SetBool ("Zoomed", false);
		}
	}
}
