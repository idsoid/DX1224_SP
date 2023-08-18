using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    [SerializeField] private ItemData item;
    [SerializeField] private GameObject equippedIcon;

    public Button RemoveButton;

    private void Update()
    {
        if (item.GetEquipped() != equippedIcon.activeSelf)
        {
            equippedIcon.SetActive(item.GetEquipped());
        }
    }

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
        item.UseItem();
        if (item.GetCount() <= 0)
        {
            RemoveItem();
        }
    }
}
