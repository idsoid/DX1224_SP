using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    [SerializeField] private ItemData item;

    public Button RemoveButton;
    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);

        Destroy(gameObject);
    }

    public void AddItem(ItemData newItem)
    {
        item = newItem;
    }

    public void UseCorrespondingItem()
    {
        Debug.Log("Use " + item.itemName);
        item.UseItem();
    }
}
