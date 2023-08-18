using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void GoToStart()
    {
        SceneManager.LoadScene("DemoScene");
    }

    public void GoToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
