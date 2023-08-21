using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    private enemydata _enemydata = new enemydata();

    public void Init(float health, float attack, Sprite enemySprite, string name)
    {
        _enemydata.health = health;
        _enemydata.attack = attack;
        _enemydata.enemySprite = enemySprite;
        _enemydata.name = name;
        _enemydata.isDead = false;
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
}

[System.Serializable]
public class enemydata
{
    public enum TYPE
    {
        KEEPER,
        CRAWLER,
        IMITATER,
        HOARDER,
        TOTAL
    }
    public float health;
    public float attack;
    public Sprite enemySprite;
    public string name;
    public bool isDead;
}