using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool isOptions = false;

    public GameObject pauseMenuUI;
    public GameObject HUD;
    public GameObject optionsUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused && !isOptions)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        HUD.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        HUD.SetActive(false);
        optionsUI.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
        isOptions = false;
    }

    public void Options()
    {
        pauseMenuUI.SetActive(false);
        optionsUI.SetActive(true);
        isOptions = true;
        isPaused = false;
    }

    public void Quit()
    {
        Time.timeScale = 1f;
    }
}
