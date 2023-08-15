using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    private playerData _playerData = new playerData();
    public void Init()
    {
        _playerData.health = 100;
        _playerData.attack = 25;
        _playerData.speed = 10;
        _playerData.stamina = 80;
        _playerData.temperature = 100;
        _playerData.hunger = 100;
    }

    public void AlterValue(string x, int value)
    {
        switch(x)
        {
            case "health":
                _playerData.health += value;
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
                break;
            case "speed":
                _playerData.speed += value;
                break;
            default:
                break;
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
    public int health;
    public int stamina;
    public int temperature;
    public int hunger;
    public int attack;
    public int speed;
}
