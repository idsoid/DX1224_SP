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
    [SerializeField] private PlayerData playerData;

    private bool lit = false;
    private bool playerInteract = false;

    private void Start()
    {
        ToggleLit(true);
    }
    private void Update()
    {
        if (lit)
        {
            if (playerData.GetValue("fireplaceBurnTime") > 0f)
            {
                playerData.AlterValue("fireplaceBurnTime",-1f * Time.deltaTime);
            }
            else if (playerData.GetValue("fireplaceBurnTime") < 0f)
            {
                playerData.SetValue("fireplaceBurnTime",0f);
            }
            else if (playerData.GetValue("fireplaceBurnTime") == 0f)
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
        safeZone.SetActive(lit);
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
        playerData.AlterValue("fireplaceBurnTime",120);
        if (!lit)
        {
            ToggleLit(true);
        }

        sfxHandler.playAudio("fireStoked");
    }

    public bool GetLit()
    {
        return lit;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInteract = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInteract = false;
        }
    }
}
