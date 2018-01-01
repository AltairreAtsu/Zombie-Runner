using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointGizmo : MonoBehaviour {

	void  OnDrawGizmos(){
		Gizmos.DrawWireCube (transform.position, new Vector3(3, 3, 3));
	}
}
