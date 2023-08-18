using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<ItemData> Items = new List<ItemData>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public Toggle EnableRemove;

    public InventoryItemController[] InventoryItems;

    private void Awake()
    {
        Instance = this;
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
    }

    public void Remove(ItemData item)
    {
        Items.Remove(item);
    }

    public void ListItems()
    {
        foreach (var Item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            obj.GetComponentInChildren<Image>().sprite = Item.itemSprite;
            obj.GetComponentInChildren<TextMeshProUGUI>().text = Item.itemName;
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
        InventoryItems = ItemContent.gameObject.GetComponentsInChildren<InventoryItemController>();
        for (int i = 0; i < Items.Count; i++)
        {
            Debug.Log(i);
            InventoryItems[i].AddItem(Items[i]);
        }
    }
    
    public void Clear()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            Destroy(InventoryItems[i].gameObject);
        }

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
