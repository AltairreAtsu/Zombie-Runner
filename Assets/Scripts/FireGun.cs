using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour {

	[SerializeField][Tooltip ("The Layer ID to use for collisions")]
	private int layerNumber = 8;
	[SerializeField][Tooltip ("The maximum distance a Raycast from the gun can travel.")]
	private float maxDistance = 100f;

	private void Fire (){
		Vector3 fwd = transform.TransformDirection (Vector3.forward);
		RaycastHit hit;
		int layerMask = 1 << 8;

		if(Physics.Raycast(transform.position, fwd, out hit, maxDistance, layerMask)){
			ZombieLogic logic = hit.collider.gameObject.GetComponent<ZombieLogic> ();
			if(logic != null){
				logic.Hit (1);
			}
		}
	}
}
