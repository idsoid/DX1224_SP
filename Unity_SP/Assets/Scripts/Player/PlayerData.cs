using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    private playerData _playerData = new playerData();

    public Vector3 temppos;
    public bool loadOldPos;
    public bool safe;

    public void Init()
    {
        _playerData.health = 100f;
        _playerData.attack = 25f;
        _playerData.speed = 10f;
        _playerData.stamina = 80f;
        _playerData.temperature = 100f;
        _playerData.hunger = 100f;
        _playerData.isAlive = true;
        _playerData.tempDecreaseMultiplier = 10f;
        _playerData.hungerDecreaseMultiplier = 0.5f;
    }

    public void SavePos(Vector3 pos)
    {
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
            default:
                return -1;
        }
    }

    public string PrintSelf()
    {
        return _playerData.health + "\n" + _playerData.stamina + "\n" + _playerData.temperature + "\n" + _playerData.hunger + "\n" + _playerData.attack + "\n" + _playerData.speed;
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
}
