using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralButtons : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    [SerializeField]
    private PlayerData playerData;

    public void OnLoadSaveClick()
    {
        Time.timeScale = 1f;
        playerData.ReadyLoad = true;
        playerData.SetValue("health", 100);
        playerData.SetValue("hunger", 100);
        playerData.Save();
        SceneManager.LoadScene(sceneName);
    }

    public void OnLoadButtonClick()
    {
        Time.timeScale = 1f;
        playerData.ReadyLoad = true;
        SceneManager.LoadScene(sceneName);
    }
    public void OnSaveClick()
    {
        if (playerData.safe)
        {
            playerData.Save();
            Debug.Log("Progress saved!");
        }
    }

    public void BackToTitle()
    {
        Time.timeScale = 1f;
        playerData.ReadyLoad = true;
        SceneManager.LoadScene("TitleScene");
    }
}
