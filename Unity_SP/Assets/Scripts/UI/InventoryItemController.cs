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
    [SerializeField] private AudioHandler sfxHandler;

    public Button RemoveButton;

    private TMP_Text itemCountText;

    private void Start()
    {
        itemCountText = itemCountDisplay.GetComponent<TMP_Text>();

        if (item.GetCount() >= 2)
        {
            itemCountDisplay.SetActive(true);
            itemCountText.text = "" + item.GetCount();
        }
    }

    private void Update()
    {
        if (item.GetEquipped() != equippedIcon.activeSelf)
        {
            equippedIcon.SetActive(item.GetEquipped());
            sfxHandler.playAudio("equip");
        }

        // Wood fix
        if (item.itemID == 52)
        {
            if (item.GetCount() <= 0)
            {
                RemoveItem();
            }
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
        if (item.GetID() != 52)
            item.UseItem();
        if (item.GetCount() <= 0)
        {
            RemoveItem();
            InventoryManager.Instance.InventoryItems.Remove(this);
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

        if (item.GetID() == 50)
            sfxHandler.playAudio("drink");
        else if (item.GetID() == 51)
            sfxHandler.playAudio("eat");
    }

    public void SetAudioHandler(AudioHandler newAudioHandler)
    {
        sfxHandler = newAudioHandler;
    }
}
