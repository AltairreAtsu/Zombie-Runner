using UnityEngine;
using UnityEngine.UI;

public class AlphaLerp : MonoBehaviour
{

	[SerializeField]
	private float maxAlpha = 1f;
	[SerializeField]
	private float minAlpha = 0f;
	[SerializeField]
	private float fadeTime = 1f;

	private float passedTime = 0f;
	private Image image;

	void Start ()
    {
		image = GetComponent<Image> ();

		image.enabled = false;
	}
	
	void Update ()
    {
		passedTime += Time.deltaTime;
		if (passedTime < fadeTime)
		{
			float newAlpha = Mathf.Lerp (maxAlpha, minAlpha, passedTime / fadeTime);
			image.color = new Color (image.color.r, image.color.g, image.color.b, newAlpha);
		} else {
			image.enabled = false;
		}


	}

	public void Trigger()
	{
		image.enabled = true;
		image.color = new Color (image.color.r, image.color.g, image.color.b, maxAlpha);
		passedTime = 0f;
	}
}
