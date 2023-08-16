using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireplace : MonoBehaviour
{
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject safeZone;
    [SerializeField] private Animator fireplaceAnim;
    [SerializeField] private InventoryManager inventoryManager;

    private bool lit;
    public float burnTime;
    private bool playerInteract;

    private void Start()
    {
        lit = true;
        burnTime = 180f;
        playerInteract = false;
    }

    private void Update()
    {
        if (lit)
        {
            if (burnTime > 0f)
            {
                burnTime -= 1f * Time.deltaTime;
            }
            else if (burnTime < 0f)
            {
                burnTime = 0f;
            }
            else if (burnTime == 0f)
            {
                ToggleLit(false);
            }
        }

        if (playerInteract)
        {
            if (Input.GetButtonDown("Interact"))
            {
                inventoryManager.BurnWood();
                AddFuel();
            }
        }
    }

    private void ToggleLit(bool x)
    {
        lit = x;
        fire.SetActive(lit);
        safeZone?.SetActive(lit);
        if (!x)
        {
            fireplaceAnim.Play("fireplace_unlit");
        }
        else
        {
            fireplaceAnim.Play("fireplace_lit");
        }
    }

    public void AddFuel()
    {
        burnTime += 60;
        if (!lit)
        {
            ToggleLit(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInteract = true;
            Debug.Log("Player interacting");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInteract = false;
            Debug.Log("Player no longer interacting");
        }
    }
}
