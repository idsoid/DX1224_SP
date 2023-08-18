using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemController : MonoBehaviour
{
    [SerializeField] private ItemData item;
    [SerializeField] private GameObject equippedIcon;
    [SerializeField] private GameObject itemCountDisplay;

    public Button RemoveButton;

    private TMP_Text itemCountText;

    private void Start()
    {
        itemCountText = itemCountDisplay.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (item.GetEquipped() != equippedIcon.activeSelf)
        {
            equippedIcon.SetActive(item.GetEquipped());
        }
        if (item.GetCount() >= 2)
        {
            itemCountDisplay.SetActive(true);
            itemCountText.text = "" + item.GetCount();
        }
        else
        {
            itemCountDisplay.SetActive(false);
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
