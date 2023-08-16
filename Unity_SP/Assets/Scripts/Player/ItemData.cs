using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    [SerializeField] private int itemID;
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private PlayerData playerData;

    private bool equipped;

    private void Start()
    {
        equipped = false;
    }

    /*
    <========== Key Items ==========>
        1. Key1
        2. Key2
        3. Key3
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
    */

    public void UseItem()
    {
        if (itemID == 20) // Fireplace Poker
        {
            if (!equipped)
            {
                equipped = true;
                playerData.AlterValue("attack", 10);
            }
            else
            {
                equipped = false;
                playerData.AlterValue("attack", -10);
            }
        }
        else if (itemID == 21) // Candle Stand
        {
            if (!equipped)
            {
                equipped = true;
                playerData.AlterValue("attack", 5);
            }
            else
            {
                equipped = false;
                playerData.AlterValue("attack", -5);
            }
        }
        else if (itemID == 22) // Broken Table Leg
        {
            if (!equipped)
            {
                equipped = true;
                playerData.AlterValue("attack", 5);
            }
            else
            {
                equipped = false;
                playerData.AlterValue("attack", -5);
            }
        }
        else if (itemID == 23) // Kitchen Knife
        {
            if (!equipped)
            {
                equipped = true;
                playerData.AlterValue("attack", 20);
            }
            else
            {
                equipped = false;
                playerData.AlterValue("attack", -20);
            }
        }
        else if (itemID == 24) // Cast Iron Pan
        {
            if (!equipped)
            {
                equipped = true;
                playerData.AlterValue("attack", 10);
            }
            else
            {
                equipped = false;
                playerData.AlterValue("attack", -10);
            }
        }
        else if (itemID == 30) // Attack Charm
        {
            if (!equipped)
            {
                equipped = true;
                playerData.AlterValue("attack", 7);
            }
            else
            {
                equipped = false;
                playerData.AlterValue("attack", -7);
            }
        }
        else if (itemID == 31) // Speed Charm
        {
            if (!equipped)
            {
                equipped = true;
                playerData.AlterValue("speed", 5);
            }
            else
            {
                equipped = false;
                playerData.AlterValue("speed", -5);
            }
        }
        else if (itemID == 32) // Anti-hunger Charm
        {
            if (!equipped)
            {
                equipped = true;
            }
            else
            {
                equipped = false;
            }
        }
        else if (itemID == 32) // Heat Charm
        {
            if (!equipped)
            {
                equipped = true;
            }
            else
            {
                equipped = false;
            }
        }
        else if (itemID == 32) // Starvation Charm
        {
            if (!equipped)
            {
                equipped = true;
            }
            else
            {
                equipped = false;
            }
        }
        else if (itemID == 50) // Health Potion
        {
            playerData.AlterValue("health", 20);
        }
        else if (itemID == 51) // Food
        {
            playerData.AlterValue("hunger", 20);
        }

    }
}
