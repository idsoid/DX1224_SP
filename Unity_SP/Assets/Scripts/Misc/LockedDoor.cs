using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [SerializeField]
    private int requiredLock;

    [SerializeField]
    private GameObject dialogueText;

    [SerializeField]
    private GameObject noKeyText;

    [SerializeField]
    private WorldState worldState;

    [SerializeField]
    private int lockIndex;

    [SerializeField] private AudioHandler sfxHandler;

    private bool canInteract;
    // Start is called before the first frame update
    void Start()
    {
        canInteract = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(canInteract)
        {
            if(Input.GetButtonDown("Interact"))
            {
                if(InventoryManager.Instance.GetItemByID(requiredLock) != null)
                {
                    worldState.locksOpened[lockIndex] = true;
                    sfxHandler.playAudio("unlockNoise");
                    dialogueText.SetActive(true);
                    Destroy(gameObject);
                }
                else
                {
                    if(!noKeyText.activeSelf)
                    {
                        noKeyText.SetActive(true);
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            canInteract = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canInteract = false;
        }
    }
}
