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
        Debug.Log("ADDED");
        Items.Add(item);
    }

    public void Remove(ItemData item)
    {
        Items.Remove(item);
    }

    public void ListItems()
    {
        foreach(Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach(var Item in Items)
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
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < Items.Count; i++)
        {
            InventoryItems[i].AddItem(Items[i]);
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

    public void BurnWood()
    {
        if (Items.Count == 0) return;
        else
        {
            foreach (var item in Items)
            {
                if (item.itemID == 52)
                {
                    Remove(item);
                    break;
                }
            }
        }
    }
}
