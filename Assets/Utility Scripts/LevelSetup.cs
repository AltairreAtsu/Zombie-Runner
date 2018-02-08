using UnityEngine;

public class LevelSetup : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The Prefabed Music Player to spawn if none is detected.")]
    private MusicPlayer musicPlayerPrefab = null;

	void Start ()
    {
        if (GameObject.FindObjectOfType<MusicPlayer>() == null)
            Instantiate(musicPlayerPrefab);
        Destroy(gameObject);
	}
	
}
