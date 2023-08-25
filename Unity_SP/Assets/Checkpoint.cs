using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private GameObject saveDialogue;

    private bool bCanInteract;

    // Update is called once per frame
    void Update()
    {
        if(bCanInteract)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                SaveGame();
                saveDialogue.SetActive(true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("hi");
            bCanInteract = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("bye");
            bCanInteract = false;
        }
    }
    private void SaveGame()
    {
        Debug.Log("Successfully saved!");
        playerData.Save();
        
    }
}
