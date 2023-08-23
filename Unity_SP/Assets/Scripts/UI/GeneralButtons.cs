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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLoadSaveClick()
    {
        Time.timeScale = 1f;
        playerData.ReadyLoad = true;
        playerData.SetValue("health", 100);
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
}
