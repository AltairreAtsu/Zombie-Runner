using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Pause : MonoBehaviour {

    public static bool gameIsPaused = false;

    [SerializeField]
    private GameObject pauseMenu = null;

    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            } else
            {
                PauseGame();
            }
        }
	}

    public void Resume()
    {
        pauseMenu.SetActive(false);
        AudioListener.pause = false;
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        AudioListener.pause = true;
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
}
