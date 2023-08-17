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

    
    // Start is called before the first frame update
    void Start()
    {
        inventoryOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }

    private void ToggleInventory()
    {
        inventoryOpen = !inventoryOpen;
        inventory.SetActive(inventoryOpen);
        if (inventoryOpen)
        {
            inventoryManager.ListItems();
        }
        else
        {
            inventoryManager.Clear();
        }
    }
}
