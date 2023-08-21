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
        playerData.Load();
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
