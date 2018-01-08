using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour {

	[Tooltip ("How Many minutes pass per second")]
	public float timeScale = 60f;
	
	// Update is called once per frame
	void Update () {
		float angleThisFrame = Time.deltaTime / 360 * timeScale;
		transform.RotateAround (transform.position, Vector3.forward, angleThisFrame);
	}
}
