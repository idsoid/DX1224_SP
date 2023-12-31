using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class PlayerInfo : ScriptableObject
{
    public float health;
    public float stamina;
    public float temperature;
    public float hunger;
    public float attack;
    public float speed;
    public bool isAlive;
    public float tempDecreaseMultiplier;
    public float hungerDecreaseMultiplier;
    public float candleBurnTime;
    public float fireplaceBurnTime;

    public bool hasSaved;
    public bool tutorialFinished;
    public List<int> inventory;
    public float maxHealth, maxStamina, maxTemperature, maxHunger;

    public bool redUnlocked, blueUnlocked, yellowUnlocked;

    public List<bool> pickeds;

    public List<bool> locksOpened;

    public bool GetTutFinished()
    {
        return tutorialFinished;
    }
    public void SetTutFinished(bool x)
    {
        tutorialFinished = x;
    }
    public void AlterValue(string x, float value)
    {
        switch (x)
        {
            case "health":
                health += value;
                if(health > maxHealth)
                {
                    health = maxHealth;
                }
                break;

            case "attack":
                attack += value;
                break;

            case "hunger":
                hunger += value;
                if(hunger < 0)
                {
                    hunger = 0;
                }
                else if (hunger > maxHunger)
                {
                    hunger = maxHunger;
                }
                break;

            case "stamina":
                stamina += value;
                if(stamina > maxStamina)
                {
                    stamina = maxStamina;
                }
                break;

            case "temperature":
                temperature += value;
                if (temperature > 100)
                {
                    temperature = 100;
                }
                else if (temperature < 0)
                {
                    temperature = 0;
                }

                break;
            case "speed":
                speed += value;
                break;

            case "tempDecreaseMultiplier":
                tempDecreaseMultiplier += value;
                break;
            case "hungerDecreaseMultiplier":
                hungerDecreaseMultiplier += value;
                break;

            case "candleBurnTime":
                candleBurnTime += value;
                break;
            case "fireplaceBurnTime":
                fireplaceBurnTime += value;
                break;
            default:
                break;
        }
    }

    public void SetValue(string x, float value)
    {
        switch (x)
        {
            case "health":
                health = value;
                break;
            case "attack":
                attack = value;
                break;
            case "hunger":
                hunger = value;
                break;
            case "stamina":
                stamina = value;
                break;
            case "temperature":
                temperature = value;
                break;
            case "speed":
                speed = value;
                break;
            case "tempDecreaseMultiplier":
                tempDecreaseMultiplier = value;
                break;
            case "hungerDecreaseMultiplier":
                hungerDecreaseMultiplier = value;
                break;
            case "candleBurnTime":
                candleBurnTime = value;
                break;
            case "fireplaceBurnTime":
                fireplaceBurnTime = value;
                break;
            default:
                break;
        }
    }
    public float GetValue(string x)
    {
        switch (x)
        {
            case "health":
                return health;
            case "attack":
                return attack;
            case "hunger":
                return hunger;
            case "stamina":
                return stamina;
            case "temperature":
                return temperature;
            case "speed":
                return speed;
            case "tempDecreaseMultiplier":
                return tempDecreaseMultiplier;
            case "hungerDecreaseMultiplier":
                return hungerDecreaseMultiplier;
            case "maxHealth":
                return maxHealth;
            case "maxStamina":
                return maxStamina;
            case "maxTemperature":
                return maxTemperature;
            case "maxHunger":
                return maxHunger;
            case "candleBurnTime":
                return candleBurnTime;
            case "fireplaceBurnTime":
                return fireplaceBurnTime;
            default:
                return -1;
        }
    }

    public bool CheckUnlocked()
    {
        return redUnlocked && blueUnlocked && yellowUnlocked;
    }

    public bool GetUnlocked(string col)
    {
        switch (col)
        {
            case "red":
                return redUnlocked;
            case "yellow":
                return yellowUnlocked;
            case "blue":
                return blueUnlocked;
            default:
                return false;
        }
    }

    public void ToggleSaved()
    {
        hasSaved = true;
    }

    public bool GetSaved()
    {
        return hasSaved;
    }
    public void Init()
    {
        hasSaved = false;
        health = 100f;
        attack = 25f;
        speed = 1f;
        stamina = 80f;
        temperature = 100f;
        hunger = 50f;
        isAlive = true;
        tempDecreaseMultiplier = 2f;
        hungerDecreaseMultiplier = 0.1f;
        redUnlocked = yellowUnlocked = blueUnlocked = false;
        maxHealth = 100f;
        maxStamina = 80f;
        maxTemperature = 100f;
        maxHunger = 100f;
        candleBurnTime = 0f;
        pickeds.Clear();
        inventory.Clear();
        fireplaceBurnTime = 0f;
        tutorialFinished = false;
    }

    public void Unlock(string col)
    {
        switch (col)
        {
            case "red":
                redUnlocked = true;
                break;
            case "yellow":
                yellowUnlocked = true;
                break;
            case "blue":
                blueUnlocked = true;
                break;
        }
    }

    public List<int> GetInventory()
    {
        return inventory;
    }
}
