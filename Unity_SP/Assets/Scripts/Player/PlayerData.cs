using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private WorldState worldState;

    [SerializeField]
    private List<ItemData> itemsList;

    [SerializeField]
    private PlayerInfo _playerData;

    public Vector3 temppos;
    public float temphp;
    public float temphunger;
    public bool loadOldPos;
    public bool safe;
    public bool isCold;
    public ItemData equippedWeapon, equippedCharm;
    public List<ItemData> inventory;
    public bool ReadyLoad = false;
    public bool canMove = true;

    public void NewGame()
    {
        SceneManager.LoadScene("DemoScene");
    }
    public void Init()
    {
        equippedWeapon = equippedCharm = null;
        isCold = false;
        safe = false;
        _playerData.Init();
        inventory.Clear();
        worldState.altered = false;
        canMove = true;
    }

    public void SavePos(Vector3 pos)
    {
        //Save();
        loadOldPos= true;
        temppos = pos;
    }

    public void AlterValue(string x, float value)
    {
        _playerData.AlterValue(x, value);
        if (x == "string")
        {
            if (_playerData.GetValue("health") <= 0)
            {
                _playerData.isAlive = false;
                loadOldPos = false;
                SceneManager.LoadScene("LoseScene");

            }
            else if (_playerData.GetValue("health") > 100)
            {
                _playerData.SetValue("health",100);
            }
        }
    }

    public void SetValue(string x, float value)
    {
        _playerData.SetValue(x, value);
    }
    public float GetValue(string x)
    {
       return _playerData.GetValue(x);
    }

    public void Unlock(string col)
    {
        _playerData.Unlock(col);
    }

    public bool GetUnlocked(string col)
    {
        return _playerData.GetUnlocked(col);
    }

    public bool CheckUnlocked()
    {
        return _playerData.CheckUnlocked();
    }


    public void Save()
    {
        inventory = InventoryManager.Instance.Items;
        _playerData.pickeds = worldState.pickedUp;

        _playerData.inventory.Clear();
        foreach (ItemData item in inventory)
        {
            for(int i = 0; i < item.GetCount(); i++)
            {
                _playerData.inventory.Add(item.GetID());
            }
        }

        _playerData.locksOpened = worldState.locksOpened;

        string s = JsonUtility.ToJson(_playerData);
        FileManager.WriteToFile("playerdata.json", s);
    }

    public void Load()
    {
        string s;
        FileManager.LoadFromFile("playerdata.json", out s);
        JsonUtility.FromJsonOverwrite(s, _playerData);

        
        worldState.pickedUp = _playerData.pickeds;
        worldState.locksOpened = _playerData.locksOpened;
        worldState.altered = true;

    }

    
    public List<int> GetInventory()
    {
        return _playerData.GetInventory();
    }
}

//[System.Serializable]
//public class playerinfo
//{
//    public float health;
//    public float stamina;
//    public float temperature;
//    public float hunger;
//    public float attack;
//    public float speed;
//    public bool isAlive;
//    public float tempDecreaseMultiplier;
//    public float hungerDecreaseMultiplier;
//    //public List<ItemData> inventory;
//    public List<int> inventory;
//    public float maxHealth, maxStamina, maxTemperature, maxHunger;

//    public bool redUnlocked, blueUnlocked, yellowUnlocked;

//    public List<bool> pickeds;

//    public List<bool> locksOpened;
//}
