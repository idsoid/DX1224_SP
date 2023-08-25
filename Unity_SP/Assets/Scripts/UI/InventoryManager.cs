using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private AudioHandler sfxHandler;

    public static InventoryManager Instance;
    public List<ItemData> Items;

    public List<ItemData> itemDatabase;
    public Transform ItemContent;
    public GameObject InventoryItem;

    [SerializeField]
    private PlayerData playerData;

    public Toggle EnableRemove;

    public List<InventoryItemController> InventoryItems;


    private void Awake()
    {
        Instance = this;
    }

    public void LoadInventory()
    {
        foreach (ItemData item in itemDatabase)
        {
            item.SetCount(0);
        }
        Items.Clear();
        List<int> things = playerData.GetInventory();
        for (int i = 0; i < things.Count; i++)
        {
            foreach (ItemData elem in itemDatabase)
            {
                if (things[i] == elem.GetID())
                {
                    Add(elem);
                }
            }
        }
    }


    public void Add(ItemData item)
    {
        foreach (var itemData in Items)
        {
            if (item.itemID == itemData.itemID)
            {
                itemData.AlterCount(1);
                return;
            }
        }

        Items.Add(item);
        item.AlterCount(1);

        playerData.inventory = Items;
    }

    public void Remove(ItemData item)
    {
        Items.Remove(item);

        playerData.inventory = Items;
    }

    public void ListItems()
    {
        foreach (var Item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            obj.GetComponentInChildren<Image>().sprite = Item.itemSprite;
            obj.GetComponentInChildren<TextMeshProUGUI>().text = Item.itemName;
            obj.GetComponent<InventoryItemController>().SetAudioHandler(sfxHandler);
            var removeButton = obj.GetComponentInChildren<Button>();

            if (EnableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }

        }
        
        SetInventoryItems();
        
    }

    

    public void SetInventoryItems()
    {
        InventoryItems.Clear();
        InventoryItemController[] temp = ItemContent.gameObject.GetComponentsInChildren<InventoryItemController>();
        for (int i = 0; i < Items.Count; i++)
        {
            //Debug.Log(i);
            temp[i].AddItem(Items[i]);
            InventoryItems.Add(temp[i]);
        }
    }
    
    public void Clear()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (InventoryItems[i] != null)
            {
                Destroy(InventoryItems[i].gameObject);
            }
        }
        //InventoryItems = null;
    }
    public void EnableItemsRemove()
    {
        if(EnableRemove.isOn)
        {
            foreach(Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }

    public ItemData GetItemByID(int id)
    {
        if (Items.Count == 0)
        {
            return null;
        }
        else
        {
            foreach(var item in Items)
            {
                if (item.itemID == id)
                {
                    return item;
                }
            }

            return null;
        }
    }
}
