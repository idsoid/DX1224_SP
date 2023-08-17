using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private GameObject closeSprite;
    [SerializeField] private GameObject openSprite;
    [SerializeField] private GameObject lockPanel;

    private bool closed;
    private bool playerInteract;

    private void Start()
    {
        closed = true;
        playerInteract = false;
    }

    public void ToggleClose(bool x)
    {
        closed = x;
        if (x)
        {
            closeSprite.SetActive(true);
            openSprite.SetActive(false);
        }
        else
        {
            closeSprite.SetActive(false);
            openSprite.SetActive(true);
        }

        if (playerInteract)
        {
            if (Input.GetButtonDown("Interact"))
            {
                lockPanel.SetActive(true);
            }
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
        }
    }
}
