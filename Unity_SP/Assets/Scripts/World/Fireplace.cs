using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireplace : MonoBehaviour
{
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject safeZone;
    [SerializeField] private Animator fireplaceAnim;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private GameObject fireLight;
    [SerializeField] private AudioHandler sfxHandler;

    private bool lit;
    private float burnTime;
    private bool playerInteract;

    private void Start()
    {
        lit = true;
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

            sfxHandler.playAudio("fireBurning");
        }

        if (playerInteract)
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (inventoryManager.GetItemByID(52) != null)
                {
                    var wood = inventoryManager.GetItemByID(52);
                    wood.UseItem();
                    AddFuel();
                }
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

            sfxHandler.playAudio("fireExtinguish");
        }
        else
        {
            fireplaceAnim.Play("fireplace_lit");
        }
        fireLight.SetActive(lit);
    }

    public void AddFuel()
    {
        burnTime += 120;
        if (!lit)
        {
            ToggleLit(true);
        }

        sfxHandler.playAudio("fireStoked");
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
