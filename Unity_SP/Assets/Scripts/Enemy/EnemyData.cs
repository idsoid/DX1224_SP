using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    private enemydata _enemydata = new enemydata();

    public void Init(float health, float attack, Sprite enemySprite, string name, string cat)
    {
        _enemydata.health = health;
        _enemydata.attack = attack;
        _enemydata.enemySprite = enemySprite;
        _enemydata.name = name;
        _enemydata.isDead = false;
        switch (cat)
        {
            case "KEEPER":
                _enemydata.type = enemydata.CATS.KEEPER;
                break;
            case "CRAWLER":
                _enemydata.type = enemydata.CATS.CRAWLER;
                break;
            case "IMITATER":
                _enemydata.type = enemydata.CATS.IMITATER;
                break;
            case "HOARDER":
                _enemydata.type = enemydata.CATS.HOARDER;
                break;
            case "BOSS":
                _enemydata.type = enemydata.CATS.BOSS;
                break;
            default:
                break;
        }
    }
    public bool GetDead()
    {
        return _enemydata.isDead;
    }
    public void SetDead(bool state)
    {
        _enemydata.isDead = state;
    }
    public float GetHealth()
    {
        return _enemydata.health;
    }
    public void SetHealth(float damage)
    {
        _enemydata.health -= damage;
        if (_enemydata.health <= 0.0f)
        {
            SetDead(true);
        }
    }
    public void ResetHealth(float health)
    {
        _enemydata.health = health;
    }
    public enemydata.CATS GetEnemyType()
    {
        return _enemydata.type;
    }
    public Sprite GetSprite()
    {
        return _enemydata.enemySprite;
    }
    public string GetName()
    {
        return _enemydata.name;
    }
}

[System.Serializable]
public class enemydata
{
    public enum CATS
    {
        KEEPER,
        CRAWLER,
        IMITATER,
        HOARDER,
        BOSS
    }
    public CATS type;
    public float health;
    public float attack;
    public Sprite enemySprite;
    public string name;
    public bool isDead;
    //public Vector3 oldPos;
}