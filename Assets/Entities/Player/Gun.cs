using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The maximum distance a Raycast from the gun can travel.")]
    private float maxDistance = 500f;

    [Space]

    [SerializeField]
    [Tooltip("The Damage each fired shot does.")]
    private int damage = 1;

    [SerializeField]
    [Tooltip("Maximum number of shots before the player has to reload.")]
    private int maxAmmo = 20;

    [Space]

    [SerializeField]
    [Tooltip("Sound to be played when the player fires the gun.")]
    private AudioClip fireGunSund = null;

    [SerializeField]
    [Tooltip("Sound to be played when the player reloads the gun.")]
    private AudioClip reloadSound = null;

    private int currentAmmo = 20;
    private AudioSource audioSource = null;
    private Animator gunAnimator = null;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsManager.GetSFXVolume();
        gunAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetAxis("Reload") > 0.5f
            && !gunAnimator.GetBool("Reloading")
            && currentAmmo != maxAmmo)
                StartReload();
    }

    private void Fire()
    {
        if (gunAnimator.GetBool("Reloading"))
            return;

        currentAmmo--;

        if (audioSource.clip != fireGunSund)
            audioSource.clip = fireGunSund;
        audioSource.Play();

        FireRay();

        if (currentAmmo <= 0)
           StartReload();
    }

    private void FireRay()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit rayHit;

        if (Physics.Raycast(ray, out rayHit, maxDistance))
        {
            ZombieLogic logic = rayHit.collider.gameObject.GetComponent<ZombieLogic>();

            if (logic != null)
            {
                logic.Hit(damage);
            }
        }
    }

    private void StartReload()
    {
        audioSource.clip = reloadSound;
        audioSource.Play();
        gunAnimator.SetBool("Reloading", true);
    }

    private void Reload()
    {
        currentAmmo = maxAmmo;
        gunAnimator.SetBool("Reloading", false);
    }

    public int getAmmo()
    {
        return currentAmmo;
    }
}
