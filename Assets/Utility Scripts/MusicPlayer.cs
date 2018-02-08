using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
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
        SetVolumeFromPrefs();
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
            SetVolumeFromPrefs();
            audioSource.Play();
		}
	}

    public void SetVolumeFromPrefs()
    {
        if(audioSource != null)
         audioSource.volume = PlayerPrefsManager.GetMusicVolume();
    }

    public void SetVolume(float newVolume)
    {
        audioSource.volume = newVolume;
    }

    public void StopAndPlay(AudioClip clip)
    {
		audioSource.Stop();
		audioSource.clip = clip;
        SetVolumeFromPrefs();
        audioSource.Play();
	}

}
