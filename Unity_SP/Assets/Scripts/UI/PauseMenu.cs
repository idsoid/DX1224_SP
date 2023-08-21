using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    //public static bool isPaused = false;

    public GameObject pauseMenuUI;
    //public GameObject HUD;
    public GameObject optionsUI;
    public GameObject inventoryUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //if (isPaused)
            //{
            //    Resume();
            //}
            //else
            //{
            //    Inventory();
            //}
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        optionsUI.SetActive(false);
        inventoryUI.SetActive(false);
        //HUD.SetActive(true);
        Time.timeScale = 1f;
        //isPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        //HUD.SetActive(false);
        optionsUI.SetActive(false);
        inventoryUI.SetActive(false);
        //isPaused = true;
    }

    public void Options()
    {
        pauseMenuUI.SetActive(false);
        optionsUI.SetActive(true);
        inventoryUI.SetActive(false);
        //isPaused = false;
        //isPaused = true;
    }

    public void Inventory()
    {
        pauseMenuUI.SetActive(false);
        optionsUI.SetActive(false);
        //inventoryUI.SetActive(true);
        Time.timeScale = 0f;
        //isPaused = true;
    }

    public void Quit()
    {
        Time.timeScale = 1f;
    }
}
