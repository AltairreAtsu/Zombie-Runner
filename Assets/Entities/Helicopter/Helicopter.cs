using UnityEngine;

public class Helicopter : MonoBehaviour
{
	[SerializeField]
    [Tooltip ("The time in minutes before the Helicopter Arrives.")]
	private float arrivalTime = 5f;

    [Space]

	[SerializeField]
    [Tooltip ("Helicopter Move Speed")]
	private float speed = 10f;

	[SerializeField]
    [Tooltip ("Speed at which the Helicopter Decends.")]
	private float landingSpeed = 10f;

	[SerializeField]
    [Tooltip ("Layer number for the terrain.")]
	private int layerNumber = 9;

	[SerializeField]
    [Tooltip ("How far off the ground the Helicopter should be when landed.")]
	private float offset = 1f;

    private AudioSource audioSource = null;
	private LandingZone landingZone;
    private ParticleSystem landingParticles = null;
    private GameObject helicopterMesh;

    private bool called = false;
	private bool dispatched = false;

	public float passedTime { get; set; }

	private void Start ()
    {
        foreach (MeshFilter meshFilter in transform.GetComponentsInChildren<MeshFilter>())
        {
            if (meshFilter.gameObject.name == "Helicopter Mesh")
            {
                helicopterMesh = meshFilter.gameObject;
            }
        }
        landingParticles = GetComponentInChildren<ParticleSystem>();
        audioSource = GetComponentInChildren<AudioSource>();
        audioSource.volume = PlayerPrefsManager.GetSFXVolume();

		passedTime = 0f;
		arrivalTime = arrivalTime * 60;
		helicopterMesh.SetActive (false);
	}
	
	public void OnDispatchHelicopter ()
    {
		landingZone = GameObject.FindObjectOfType<LandingZone>();
		called = true;
	}

	private void Update()
    {
        CheckDispatch();

		if(!dispatched || landingZone.winMode)
        {
			return;
		}

        if (MoveToLandingZone())
            Land();
    }

    private void CheckDispatch()
    {
        if (called)
        {
            passedTime += Time.deltaTime;
            if (passedTime > arrivalTime)
            {
                Dispatch();
                called = false;
            }
        }
    }

	private void Dispatch()
    {
		helicopterMesh.SetActive (true);

		transform.LookAt(landingZone.gameObject.transform.position);
		transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);
		dispatched = true;

		SendMessageUpwards("OnHelicopterSpawned");
	}

	private float? GetDistanceToGround()
    {
		Ray ray = new Ray (transform.position, Vector3.down);
		RaycastHit rayHit;
		int layerMask = 1 << layerNumber;

		if(Physics.Raycast(ray, out rayHit, 300f, layerMask))
        {
			return rayHit.distance - offset;
		}

		return null;
	}

    private bool MoveToLandingZone()
    {
        float distanceX = Mathf.Abs(transform.position.x - landingZone.transform.position.x);
        float distanceZ = Mathf.Abs(transform.position.z - landingZone.transform.position.z);
        float totalDistance = distanceX + distanceZ;

        if (totalDistance > 5)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            return false;
        }
        return true;
    }

    private void Land()
    {
        if (GetDistanceToGround() > 0)
        {
            transform.Translate(Vector3.down * landingSpeed * Time.deltaTime);
            return;
        }

        if (!landingZone.winMode)
        {
            landingParticles.Play();
            SendMessageUpwards("OnHelicopterLanded");
            landingZone.winMode = true;
        }
    }

	public float GetArrivalTime()
    {
		return arrivalTime;
	}
}
