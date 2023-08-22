using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    private playerData _playerData = new playerData();

    public Vector3 temppos;
    public bool loadOldPos;
    public bool safe;
    public bool isCold;
    public ItemData equippedWeapon, equippedCharm;
    public List<ItemData> inventory = new List<ItemData>();

    public void Init()
    {
        equippedWeapon = equippedCharm = null;
        isCold = false;
        safe = false;
        _playerData.health = 100f;
        _playerData.attack = 25f;
        _playerData.speed = 10f;
        _playerData.stamina = 80f;
        _playerData.temperature = 100f;
        _playerData.hunger = 100f;
        _playerData.isAlive = true;
        _playerData.tempDecreaseMultiplier = 10f;
        _playerData.hungerDecreaseMultiplier = 0.5f;
        _playerData.redUnlocked = _playerData.yellowUnlocked = _playerData.blueUnlocked = false;
        _playerData.maxHealth = 100f;
        _playerData.maxStamina = 80f;
        _playerData.maxTemperature = 100f;
        _playerData.maxHunger = 100f;

        _playerData.masterVol = -20f;
        _playerData.sfxVol = -20f;
        _playerData.bgmVol = -20f;
    }

    public void SavePos(Vector3 pos)
    {
        Save();
        loadOldPos= true;
        temppos = pos;
    }

    public void AlterValue(string x, float value)
    {
        switch(x)
        {
            case "health":
                _playerData.health += value;
                if(_playerData.health <= 0)
                {
                    _playerData.isAlive = false;
                    _playerData.health = 0;
                    SceneManager.LoadScene("LoseScene");
                }
                else if (_playerData.health > 100)
                {
                    _playerData.health = 100;
                }
                break;

            case "attack":
                _playerData.attack += value;
                break;

            case "hunger":
                _playerData.hunger += value;
                break;

            case "stamina":
                _playerData.stamina += value;
                break;

            case "temperature":
                _playerData.temperature += value;
                if (_playerData.temperature > 100)
                {
                    _playerData.temperature = 100;
                }
                else if (_playerData.temperature < 0)
                {
                    _playerData.temperature = 0;
                }

                break;
            case "speed":
                _playerData.speed += value;
                break;

            case "tempDecreaseMultiplier":
                _playerData.tempDecreaseMultiplier += value;
                break;
            case "hungerDecreaseMultiplier":
                _playerData.hungerDecreaseMultiplier += value;
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
                _playerData.health = value;
                break;
            case "attack":
                _playerData.attack = value;
                break;
            case "hunger":
                _playerData.hunger = value;
                break;
            case "stamina":
                _playerData.stamina = value;
                break;
            case "temperature":
                _playerData.temperature = value;
                break;
            case "speed":
                _playerData.speed = value;
                break;
            case "tempDecreaseMultiplier":
                _playerData.tempDecreaseMultiplier = value;
                break;
            case "hungerDecreaseMultiplier":
                _playerData.hungerDecreaseMultiplier = value;
                break;
            case "masterVol":
                _playerData.masterVol = value;
                break;
            case "sfxVol":
                _playerData.sfxVol = value;
                break;
            case "bgmVol":
                _playerData.bgmVol = value;
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
                return _playerData.health;
            case "attack":
                return _playerData.attack;
            case "hunger":
                return _playerData.hunger;
            case "stamina":
                return _playerData.stamina;
            case "temperature":
                return _playerData.temperature;
            case "speed":
                return _playerData.speed;
            case "tempDecreaseMultiplier":
                return _playerData.tempDecreaseMultiplier;
            case "hungerDecreaseMultiplier":
                return _playerData.hungerDecreaseMultiplier;
            case "maxHealth":
                return _playerData.maxHealth;
            case "maxStamina":
                return _playerData.maxStamina;
            case "maxTemperature":
                return _playerData.maxTemperature;
            case "maxHunger":
                return _playerData.maxHunger;
            case "masterVol":
                return _playerData.masterVol;
            case "sfxVol":
                return _playerData.sfxVol;
            case "bgmVol":
                return _playerData.bgmVol;
            default:
                return -1;
        }
    }

    public string PrintSelf()
    {
        return _playerData.health + "\n" + _playerData.stamina + "\n" + _playerData.temperature + "\n" + _playerData.hunger + "\n" + _playerData.attack + "\n" + _playerData.speed;
    }

    public void Unlock(string col)
    {
        switch(col)
        {
            case "red":
                _playerData.redUnlocked = true;
                break;
            case "yellow":
                _playerData.yellowUnlocked = true;
                break;
            case "blue":
                _playerData.blueUnlocked = true;
                break;
        }
    }

    public bool GetUnlocked(string col)
    {
        switch(col)
        {
            case "red":
                return _playerData.redUnlocked;
            case "yellow":
                return _playerData.yellowUnlocked;
            case "blue":
                return _playerData.blueUnlocked;
            default:
                return false;
        }
    }

    public bool CheckUnlocked()
    {
        return _playerData.redUnlocked && _playerData.blueUnlocked && _playerData.yellowUnlocked;
    }


    public void Save()
    {
        _playerData.inventory = inventory;
        string s = JsonUtility.ToJson(_playerData);
        FileManager.WriteToFile("playerdata.json", s);
    }

    public void Load()
    {
        string s;
        FileManager.LoadFromFile("playerdata.json", out s);
        JsonUtility.FromJsonOverwrite(s, _playerData);
        inventory = _playerData.inventory;
        InventoryManager.Instance.LoadInventory();
    }

    public void SetInventory(List<ItemData> inv)
    {
        _playerData.inventory = inv;
    }

    public List<ItemData> GetInventory()
    {
        return _playerData.inventory;
    }
}

[System.Serializable]
public class playerData
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
    public List<ItemData> inventory;
    
    public float maxHealth, maxStamina, maxTemperature, maxHunger;

    public bool redUnlocked, blueUnlocked, yellowUnlocked;

    public float masterVol;
    public float sfxVol;
    public float bgmVol;

}
