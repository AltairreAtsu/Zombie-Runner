using UnityEngine;
using TMPro;

public class KillPopUp : MonoBehaviour {

	[SerializeField][Tooltip ("The Speed at which the popup rises.")]
	private float moveSpeed = 1f;
	[SerializeField][Tooltip ("The time in which the popup remains visible on the screen.")]
	private float fadeTime = 1f;

	private TextMeshProUGUI text;
    private AudioSource audioSource;
    private float currentTime = 0f;

	void Start () {
		text = GetComponent<TextMeshProUGUI>();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsManager.GetSFXVolume();
	}

	void Update () {
		currentTime += Time.deltaTime;
		transform.position += new Vector3 (0f, moveSpeed * Time.deltaTime, 0f);

		text.alpha = Mathf.Lerp (1f, 0f, currentTime / fadeTime);

		if(currentTime > fadeTime){
			Destroy (gameObject);
		}
	}
}
