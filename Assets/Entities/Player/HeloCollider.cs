﻿using System.Collections.Generic;
using UnityEngine;

public class HeloCollider : MonoBehaviour
{
	private float startTime;
	private bool obstructed = false;
	private bool foundClearArea = false;
    private List<Collider> colliding;

    void Start ()
    {
		colliding = new List<Collider> (10);
	}

	void Update ()
    {
		if(Time.time - startTime > 1 && !obstructed && !foundClearArea && Time.realtimeSinceStartup > 10f)
        {
			SendMessageUpwards("OnFindClearArea");
			foundClearArea = true;
			gameObject.SetActive (false);
		}
	}

	void OnTriggerEnter(Collider coll)
    {
		if (coll.tag != "Player")
        {
			colliding.Add (coll);
			obstructed = true;
			startTime = Time.time;
		}
	}

	void OnTriggerExit(Collider coll)
    {
		if (coll.tag != "Player")
        {
			colliding.RemoveAt (colliding.FindIndex (x => x.Equals(coll) ));

			if(colliding.Count == 0)
            {
				obstructed = false;
				startTime = Time.time;
			}
		}
	}

}
