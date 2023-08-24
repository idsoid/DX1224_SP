using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class ItemData : ScriptableObject
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private bool equipped;
    [SerializeField] private int count;

    public int itemID;
    public string itemName;
    public Sprite itemSprite;

    private void OnEnable()
    {
        equipped = false;
        count = 0;
    }

    /*
    <========== Key Items ==========>
        1. Key_Yellow
        2. Key_Blue
        3. Key_Red
        4. Rusty_Key
    <========== Weapons ==========>
        20. Fireplace Poker +10 atk
        21. Candle Stand +5 atk
        22. Broken Table Leg +5 atk
        23. Kitchen Knife +20 atk
        24. Cast Iron Pan +10 atk
    <========== Charms ==========>
        30. Attack Charm +7 atk
        31. Speed Charm +5 spd
        32. Anti-hunger Charm 
        33. Heat Charm 
        34. Starvation Charm +7 atk if hunger < 50
    <========== Consumables ==========>
        50. Health Potion +20 hp
        51. Food +20 hunger
        52. Wood +60 burnTime
    */

    public void UseItem()
    {
        if (itemID == 20) // Fireplace Poker
        {
            if (!equipped)
            {
                equipped = true;
                playerData.AlterValue("attack", 10);
                if (playerData.equippedWeapon != this && playerData.equippedWeapon != null)
                {
                    playerData.equippedWeapon.UseItem();
                }
                playerData.equippedWeapon = this;
            }
            else
            {
                equipped = false;
                playerData.AlterValue("attack", -10);
                playerData.equippedWeapon = null;
            }
        }
        else if (itemID == 21) // Candle Stand
        {
            if (!equipped)
            {
                equipped = true;
                playerData.AlterValue("attack", 5);
                if (playerData.equippedWeapon != this && playerData.equippedWeapon != null)
                {
                    playerData.equippedWeapon.UseItem();
                }
                playerData.equippedWeapon = this;
            }
            else
            {
                equipped = false;
                playerData.AlterValue("attack", -5);
                playerData.equippedWeapon = null;
            }
        }
        else if (itemID == 22) // Broken Table Leg
        {
            if (!equipped)
            {
                equipped = true;
                playerData.AlterValue("attack", 5);
                if (playerData.equippedWeapon != this && playerData.equippedWeapon != null)
                {
                    playerData.equippedWeapon.UseItem();
                }
                playerData.equippedWeapon = this;
            }
            else
            {
                equipped = false;
                playerData.AlterValue("attack", -5);
                playerData.equippedWeapon = null;
            }
        }
        else if (itemID == 23) // Kitchen Knife
        {
            if (!equipped)
            {
                equipped = true;
                playerData.AlterValue("attack", 20);
                if (playerData.equippedWeapon != this && playerData.equippedWeapon != null)
                {
                    playerData.equippedWeapon.UseItem();
                }
                playerData.equippedWeapon = this;
            }
            else
            {
                equipped = false;
                playerData.AlterValue("attack", -20);
                playerData.equippedWeapon = null;
            }
        }
        else if (itemID == 24) // Cast Iron Pan
        {
            if (!equipped)
            {
                equipped = true;
                playerData.AlterValue("attack", 10);
                if (playerData.equippedWeapon != this && playerData.equippedWeapon != null)
                {
                    playerData.equippedWeapon.UseItem();
                }
                playerData.equippedWeapon = this;
            }
            else
            {
                equipped = false;
                playerData.AlterValue("attack", -10);
                playerData.equippedWeapon = null;
            }
        }
        else if (itemID == 30) // Attack Charm
        {
            if (!equipped)
            {
                equipped = true;
                playerData.AlterValue("attack", 7);
                if (playerData.equippedCharm != this && playerData.equippedCharm != null)
                {
                    playerData.equippedCharm.UseItem();
                }
                playerData.equippedCharm = this;
            }
            else
            {
                equipped = false;
                playerData.AlterValue("attack", -7);
                playerData.equippedCharm = null;
            }
        }
        else if (itemID == 31) // Speed Charm
        {
            if (!equipped)
            {
                equipped = true;
                playerData.AlterValue("speed", 5);
                if (playerData.equippedCharm != this && playerData.equippedCharm != null)
                {
                    playerData.equippedCharm.UseItem();
                }
                playerData.equippedCharm = this;
            }
            else
            {
                equipped = false;
                playerData.AlterValue("speed", -5);
                playerData.equippedCharm = null;
            }
        }
        else if (itemID == 32) // Anti-hunger Charm
        {
            if (!equipped)
            {
                equipped = true;
                playerData.AlterValue("hungerDecreaseMultiplier", -0.2f);
                if (playerData.equippedCharm != this && playerData.equippedCharm != null)
                {
                    playerData.equippedCharm.UseItem();
                }
                playerData.equippedCharm = this;
            }
            else
            {
                equipped = false;
                playerData.AlterValue("hungerDecreaseMultiplier", 0.2f);
                playerData.equippedCharm = null;
            }
        }
        else if (itemID == 33) // Heat Charm
        {
            if (!equipped)
            {
                equipped = true;
                playerData.AlterValue("tempDecreaseMultiplier", -2.5f);
                if (playerData.equippedCharm != this && playerData.equippedCharm != null)
                {
                    playerData.equippedCharm.UseItem();
                }
                playerData.equippedCharm = this;
            }
            else
            {
                equipped = false;
                playerData.AlterValue("tempDecreaseMultiplier", 2.5f);
                playerData.equippedCharm = null;
            }
        }
        else if (itemID == 34) // Starvation Charm
        {
            if (!equipped)
            {
                equipped = true;
                playerData.AlterValue("hungerDecreaseMultiplier", 0.25f);
                if (playerData.equippedCharm != this && playerData.equippedCharm != null)
                {
                    playerData.equippedCharm.UseItem();
                }
                playerData.equippedCharm = this;
            }
            else
            {
                equipped = false;
                playerData.AlterValue("hungerDecreaseMultiplier", -0.25f);
                playerData.equippedCharm = null;
            }
        }
        else if (itemID == 50) // Health Potion
        {
            if (count > 0)
            {
                playerData.AlterValue("health", 35);
                count--;
            }
            else
                Debug.Log("No health potion to consume.");
        }
        else if (itemID == 51) // Food
        {
            if (count > 0)
            {
                playerData.AlterValue("hunger", 35);
                count--;
            }
            else
                Debug.Log("No food to consume.");
        }
        else if (itemID == 52) // Wood
        {
            if (count > 0)
            {
                count--;
                if (count == 0)
                {
                    InventoryManager.Instance.Remove(this);
                }
            }
            else
                Debug.Log("No wood to burn.");
            
        }
    }

    public int GetID()
    {
        return itemID;
    }

    public int GetCount()
    {
        return count;
    }

    public void SetCount(int value)
    {
        count = value;
    }
    public void AlterCount(int added)
    {
        count += added;
    }

    public bool GetEquipped()
    {
        return equipped;
    }
}
