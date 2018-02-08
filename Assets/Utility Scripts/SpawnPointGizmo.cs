using UnityEngine;

public class SpawnPointGizmo : MonoBehaviour {

	void  OnDrawGizmos()
    {
		Vector3 position = transform.position;
		position.y += 1;
		Gizmos.DrawWireCube (position, new Vector3(3, 3, 3));
	}
}
