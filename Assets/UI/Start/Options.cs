using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour {

    [SerializeField]
    [Tooltip ("The Master Volume Control Slider")]
    private Slider MasterVolumeSlider = null;
    [SerializeField]
    [Tooltip ("The Music Volume Control Slider")]
    private Slider MusicVolumeSlider = null;
    [SerializeField]
    [Tooltip ("The Dialouge Volume Control Slider")]
    private Slider DialougeVolumeSlider = null;
    [SerializeField]
    [Tooltip ("The Sound Effects Volume Control Slider")]
    private Slider SFXVolumeSlider = null;

    private MusicPlayer musicPlayer = null;

    void Start ()
    {
        SetValuesFromPrefs();
        musicPlayer = GameObject.FindObjectOfType<MusicPlayer>();
        MusicVolumeSlider.onValueChanged.AddListener(UpdateMusicVolume);
	}

    public void UpdateMasterVolume()
    {
        AudioListener.volume = MasterVolumeSlider.value;
    }

    public void UpdateMusicVolume(float newValue)
    {
        musicPlayer.SetVolume(newValue);
    }

    public void SaveValues()
    {
        PlayerPrefsManager.SetMasterVolume(MasterVolumeSlider.value);
        PlayerPrefsManager.SetMusicVolume(MusicVolumeSlider.value);
        PlayerPrefsManager.SetDialougeVolume(DialougeVolumeSlider.value);
        PlayerPrefsManager.SetSFXVolume(SFXVolumeSlider.value);

        UpdateMasterVolume();
    }

    public void SetValuesFromPrefs()
    {
        MasterVolumeSlider.value = PlayerPrefsManager.GetMasterVolume();
        MusicVolumeSlider.value = PlayerPrefsManager.GetMusicVolume();
        DialougeVolumeSlider.value = PlayerPrefsManager.GetDialougeVolume();
        SFXVolumeSlider.value = PlayerPrefsManager.GetSFXVolume();
    }

    public void SetDefaultValues()
    {
        MasterVolumeSlider.value = PlayerPrefsManager.MASTER_VOLUME_DEFAULT;
        MusicVolumeSlider.value = PlayerPrefsManager.MUSIC_VOLUME_DEFUALT;
        DialougeVolumeSlider.value = PlayerPrefsManager.DIALOUGE_VOLUME_DEFUALT;
        SFXVolumeSlider.value = PlayerPrefsManager.SFX_VOLUME_DEFUALT;
    }
}
