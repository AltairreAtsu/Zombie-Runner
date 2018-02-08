using UnityEngine;

public static class PlayerPrefsManager {
    const string MASTER_VOLUME_KEY = "master_volume";
    const string MUSIC_VOLUME_KEY = "music_volume";
    const string DIALOUGE_VOLUME_KEY = "dialouge_volume";
    const string SFX_VOLUME_KEY = "sfx_volume";

    public const float MASTER_VOLUME_DEFAULT = 1.0f;
    public const float MUSIC_VOLUME_DEFUALT = 0.3f;
    public const float DIALOUGE_VOLUME_DEFUALT = 1.0f;
    public const float SFX_VOLUME_DEFUALT = 1.0f;

    #region MasterVolume
    public static void SetMasterVolume(float volume)
    {
        if (volume >= 0f && volume <= 1f)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Master Volume out of range!");
        }
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, MASTER_VOLUME_DEFAULT);
    }
    #endregion

    #region MusicVolume
    public static void SetMusicVolume(float volume)
    {
        if (volume >= 0f && volume <= 1f)
        {
            PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Music Volume out of range!");
        }
    }

    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, MUSIC_VOLUME_DEFUALT);
    }
    #endregion
    
    #region DialougeVolume
    public static void SetDialougeVolume(float volume)
    {
        if (volume >= 0f && volume <= 1f)
        {
            PlayerPrefs.SetFloat(DIALOUGE_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Dialouge Volume out of range!");
        }
    }

    public static float GetDialougeVolume()
    {
        return PlayerPrefs.GetFloat(DIALOUGE_VOLUME_KEY, DIALOUGE_VOLUME_DEFUALT);
    }
    #endregion

    #region SFXVolume
    public static void SetSFXVolume(float volume)
    {
        if (volume >= 0f && volume <= 1f)
        {
            PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("SFX Volume out of range!");
        }
    }

    public static float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat(SFX_VOLUME_KEY, SFX_VOLUME_DEFUALT);
    }
    #endregion
}
