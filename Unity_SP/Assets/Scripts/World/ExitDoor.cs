using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private GameObject closeSprite;
    [SerializeField] private GameObject openSprite;
    [SerializeField] private GameObject lockPanel;
    [SerializeField] private GameObject yellowLock;
    [SerializeField] private GameObject blueLock;
    [SerializeField] private GameObject redLock;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private AudioHandler sfxHandler;

    private bool closed;
    private bool playerInteract;
    private bool panelOpen;
    private BoxCollider2D boxCol;

    private void Start()
    {
        closed = true;
        playerInteract = false;
        panelOpen = false;
        boxCol = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (playerInteract)
        {
            if (Input.GetButtonDown("Interact"))
            {
                panelOpen = !panelOpen;
                lockPanel.SetActive(panelOpen);

                if (!playerData.GetUnlocked("yellow"))
                {
                    yellowLock.SetActive(true);
                }
                else
                {
                    yellowLock.SetActive(false);
                }

                if (!playerData.GetUnlocked("blue"))
                {
                    blueLock.SetActive(true);
                }
                else
                {
                    blueLock.SetActive(false);
                }

                if (!playerData.GetUnlocked("red"))
                {
                    redLock.SetActive(true);
                }
                else
                {
                    redLock.SetActive(false);
                }
            }
        }
    }

    public void UnlockYellow()
    {
        if (inventoryManager.GetItemByID(1) != null)
        {
            playerData.Unlock("yellow");
            yellowLock.SetActive(false);
        }

        if (playerData.CheckUnlocked())
        {
            closed = false;
            ToggleClose(false);
        }
    }

    public void UnlockBlue()
    {
        if (inventoryManager.GetItemByID(2) != null)
        {
            playerData.Unlock("blue");
            blueLock.SetActive(false);
        }

        if (playerData.CheckUnlocked())
        {
            closed = false;
            ToggleClose(false);
        }
    }

    public void UnlockRed()
    {
        if (inventoryManager.GetItemByID(3) != null)
        {
            playerData.Unlock("red");
            redLock.SetActive(false);
        }

        if (playerData.CheckUnlocked())
        {
            closed = false;
            ToggleClose(false);
        }
    }

    public void ToggleClose(bool x)
    {
        closed = x;
        if (x)
        {
            closeSprite.SetActive(true);
            openSprite.SetActive(false);
            boxCol.enabled = true;
        }
        else
        {
            closeSprite.SetActive(false);
            openSprite.SetActive(true);
            boxCol.enabled = false;
            sfxHandler.playAudio("exitDoorOpen");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInteract = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInteract = false;
            if (lockPanel.activeSelf)
            {
                lockPanel.SetActive(false);
            }
        }
    }
}
