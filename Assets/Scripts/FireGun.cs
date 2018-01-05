using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour {

	[SerializeField][Tooltip ("The Layer ID to use for collisions")]
	private int layerNumber = 8;
	[SerializeField][Tooltip ("The maximum distance a Raycast from the gun can travel.")]
	private float maxDistance = 500f;
	[SerializeField][Tooltip ("The Main Camera for the scne.")]
	private Camera mainCamera = null;
	[SerializeField][Tooltip ("The Damage each fired shot does.")]
	private int damage = 1;

	private void Fire (){
		Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
		RaycastHit rayHit;
		int layerMask = 1 << layerNumber;

		if (Physics.Raycast(ray, out rayHit, maxDistance, layerMask)){
			ZombieLogic logic = rayHit.collider.gameObject.GetComponent <ZombieLogic>();

			if(logic != null){
				logic.Hit (damage);
			}
		}
	}


}
