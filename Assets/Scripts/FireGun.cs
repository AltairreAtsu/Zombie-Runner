using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : MonoBehaviour {

	private void Fire (){
		Vector3 fwd = transform.TransformDirection (Vector3.forward);
		RaycastHit hit;
		int layerMask = 1 << 8;

		if(Physics.Raycast(transform.position, fwd, out hit, 20f, layerMask)){
			ZombieLogic logic = hit.collider.gameObject.GetComponent<ZombieLogic> ();
			if(logic != null){
				logic.Hit (1);
			}
		}
	}
}
