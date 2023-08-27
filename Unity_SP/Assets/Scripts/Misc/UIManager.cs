using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject inventory;
    private bool inventoryOpen;

    [SerializeField]
    private InventoryManager inventoryManager;

    private bool isPaused = false;
    private bool isInvent = false;
    private bool isMenu = false;
    private bool isOptions = false;

    public GameObject pauseMenuUI;
    //public GameObject HUD;
    public GameObject optionsUI;
    //public GameObject inventoryUI;
    //public GameObject tabButtons;

    [SerializeField]
    AudioHandler audioHandler;


    // Start is called before the first frame update
    void Start()
    {
        inventoryOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetButtonDown("Inventory"))
        //{
        //    ToggleInventory();
        //}

        if (Input.GetButtonDown("Inventory"))
        {
            if (isPaused)
            {
                if (inventoryOpen)
                {
                    ToggleInventory();
                }
                Resume();
            }
            else
            {
                //tabButtons.SetActive(true);
                Inventory();
            }
        }
    }

    private void ToggleInventory()
    {
        inventoryOpen = !inventoryOpen;
        inventory.SetActive(inventoryOpen);
        if (inventoryOpen)
        {
            isPaused = true;
            Time.timeScale = 0f;
            isInvent = true;
            isMenu = false;
            isOptions = false;
            //tabButtons.SetActive(true);
            inventoryManager.ListItems();
        }
        else
        {
            Time.timeScale = 1f;
            isPaused = false;
            //if (!isOptions || !isMenu || !isInvent)
            //{
            //    tabButtons.SetActive(false);
            //}
            inventoryManager.Clear();
        }
    }

    public void Resume()
    {
        //tabButtons.SetActive(false);
        pauseMenuUI.SetActive(false);
        optionsUI.SetActive(false);
        //inventoryUI.SetActive(false);
        //HUD.SetActive(true);
        Time.timeScale = 1f;
        isInvent = false;
        isPaused = false;
        isMenu = false;
        isOptions = false;
    }

    public void Pause()
    {
        if (!isMenu)
        {
            //tabButtons.SetActive(true);
            pauseMenuUI.SetActive(true);
            //HUD.SetActive(false);
            optionsUI.SetActive(false);
            //inventoryUI.SetActive(false);
            inventoryOpen = true;
            if (inventory.activeSelf)
            {
                ToggleInventory();
            }
            Time.timeScale = 0f;
            isPaused = true;
            isInvent = false;
            isMenu = true;
            isOptions = false;
        }
    }

    public void Options()
    {
        if (!isOptions)
        {
            //tabButtons.SetActive(true);
            pauseMenuUI.SetActive(false);
            optionsUI.SetActive(true);
            //inventoryUI.SetActive(false);
            inventoryOpen = true;
            if (inventory.activeSelf)
            {
                ToggleInventory();
            }
            Time.timeScale = 0f;
            isPaused = true;
            isInvent = false;
            isMenu = false;
            isOptions = true;
        }
    }

    public void Inventory()
    {
        if (!isInvent)
        {

            audioHandler.playAudio("inventory");
            //tabButtons.SetActive(true);
            isInvent = true;
            isMenu = false;
            isOptions = false;
            pauseMenuUI.SetActive(false);
            optionsUI.SetActive(false);
            inventoryOpen = false;
            Time.timeScale = 0f;
            isPaused = true;
            if (!inventory.activeSelf)
            {
                ToggleInventory();
            }
        }
    }

    public void Quit()
    {
        Time.timeScale = 1f;
    }
}

