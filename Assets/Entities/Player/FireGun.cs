using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour {
	[SerializeField][Tooltip ("The maximum distance a Raycast from the gun can travel.")]
	private float maxDistance = 500f;
	[SerializeField][Tooltip ("The Main Camera for the scne.")]
	private Camera mainCamera = null;
	[SerializeField][Tooltip ("The Damage each fired shot does.")]
	private int damage = 1;

	private void Fire (){
		Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
		RaycastHit rayHit;

		if (Physics.Raycast(ray, out rayHit, maxDistance)){
			ZombieLogic logic = rayHit.collider.gameObject.GetComponent <ZombieLogic>();

			if(logic != null){
				logic.Hit (damage);
			}
		}
	}


}
