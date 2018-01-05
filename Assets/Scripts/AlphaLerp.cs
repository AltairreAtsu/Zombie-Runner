using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaLerp : MonoBehaviour {

	[SerializeField]
	private float maxAlpha = 1f;
	[SerializeField]
	private float minAlpha = 0f;
	[SerializeField]
	private float fadeTime = 1f;

	private float baseAlpha = 0f;
	private float passedTime = 0f;
	private Image image;

	private Player player;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image> ();
		player = GameObject.FindObjectOfType<Player> ();
		baseAlpha = image.color.a;

		player.playerHitObservers += Trigger;
		image.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		passedTime += Time.deltaTime;
		if (passedTime < fadeTime)
		{
			float newAlpha = Mathf.Lerp (maxAlpha, minAlpha, passedTime / fadeTime);
			image.color = new Color (image.color.r, image.color.g, image.color.b, newAlpha);
		} else {
			image.enabled = false;
		}


	}

	void Trigger()
	{
		image.enabled = true;
		image.color = new Color (image.color.r, image.color.g, image.color.b, maxAlpha);
		passedTime = 0f;
	}
}
