using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour {

	[SerializeField][Tooltip ("The Layer ID to use for collisions")]
	private int layerNumber = 8;
	[SerializeField][Tooltip ("The maximum distance a Raycast from the gun can travel.")]
	private float maxDistance = 500f;
	[SerializeField][Tooltip ("The Game GameObject representing the end of the barrel.")]
	private Transform endOfBarrel = null;
	[SerializeField][Tooltip ("The Damage each fired shot does.")]
	private int damage = 1;

	private Ray ray;

	private void Fire (){
		Vector3 fwd = endOfBarrel.transform.TransformDirection (Vector3.forward);
		RaycastHit hit;
		int layerMask = 1 << layerNumber;
		ray = new Ray (endOfBarrel.transform.position, fwd);

		if(Physics.Raycast(ray, out hit, maxDistance, layerMask)){
			ZombieLogic logic = hit.collider.gameObject.GetComponent<ZombieLogic> ();
			if(logic != null){
				logic.Hit (damage);
			}
		}

	}


}
