using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<ItemData> Items = new List<ItemData>();

    private void Awake()
    {
        Instance = this;
    }

    public void Add(ItemData item)
    {
        Items.Add(item);
    }

    public void Remove(ItemData item)
    {
        Items.Remove(item);
    }
}
