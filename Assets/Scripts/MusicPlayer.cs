using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
    [SerializeField]
    [Tooltip ("Music tracks to play on a per level basis.")]
    private AudioClip[] music;

	private AudioSource audioSource;

    private static MusicPlayer musicPlayer = null;

	private void Start ()
    {
        if (musicPlayer != null)
            Destroy(gameObject);

        musicPlayer = this;
		GameObject.DontDestroyOnLoad(gameObject);

		audioSource = gameObject.GetComponent<AudioSource>();

        audioSource.clip = music[SceneManager.GetActiveScene().buildIndex];
		audioSource.loop = true;
        audioSource.Play();

        SceneManager.sceneLoaded += OnSceneLoaded;

        foreach (AudioClip clip in music)
        {
			if(clip == null)
				Debug.LogWarning("Audio Clip missing from music Array!\n" + "All array entries must have values!");
		}
	}
	
	public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;

        if (music[buildIndex] != null && music[buildIndex] != audioSource.clip)
        {
			audioSource.Stop();
			audioSource.clip = music[buildIndex];
            audioSource.Play();
		}
	}

	public void StopAndPlay(AudioClip clip)
    {
		audioSource.Stop();
		audioSource.loop = false;
		audioSource.clip = clip;
		audioSource.Play();
	}

}
