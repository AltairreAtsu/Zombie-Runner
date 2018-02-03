using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour {
	[SerializeField][Tooltip ("The maximum distance a Raycast from the gun can travel.")]
	private float maxDistance = 500f;
    [Space]
    [SerializeField][Tooltip ("The Damage each fired shot does.")]
	private int damage = 1;
    [SerializeField][Tooltip ("Maximum number of shots before the player has to reload.")]
    private int maxAmmo = 20;
    [SerializeField][Tooltip ("The time it takes to complete a reload")]
    private float reloadTime = 1f;
    [Space]
    [SerializeField][Tooltip("Sound to be played when the player fires the gun.")]
    private AudioClip fireGunSund = null;
    [SerializeField][Tooltip ("Sound to be played when the player reloads the gun.")]
    private AudioClip reloadSound = null;

    private int ammo = 20;
    private AudioSource audioSource = null;
    private Animator gunAnimator = null;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gunAnimator = GetComponent<Animator>();
    }

    private void Fire (){
        if (gunAnimator.GetBool("Reloading"))
            return;

        ammo--;

        if (ammo <= 0)
        {
            audioSource.clip = reloadSound;
            audioSource.Play();
            gunAnimator.SetBool("Reloading", true);
            Invoke("Reload", reloadTime);
            return;
        }

        if(audioSource.clip != fireGunSund)
            audioSource.clip = fireGunSund;

        audioSource.Play();

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
		RaycastHit rayHit;

		if (Physics.Raycast(ray, out rayHit, maxDistance)){
			ZombieLogic logic = rayHit.collider.gameObject.GetComponent <ZombieLogic>();

			if(logic != null){
				logic.Hit (damage);
			}
		}
	}

    private void Reload()
    {
        ammo = maxAmmo;
        gunAnimator.SetBool("Reloading", false);
    }

    public int getAmmo()
    {
        return ammo;
    }
}
