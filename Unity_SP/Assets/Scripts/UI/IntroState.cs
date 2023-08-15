using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

public class IntroState : MonoBehaviour
{
    public static IntroState instance;
    Animator ar;
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

    void Start()
    {
        ar = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.anyKey)
        {
            ar.SetBool("boom", true);
            
        }
    }

    public void StartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
