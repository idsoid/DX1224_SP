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
    public float GetHealth()
    {
        return _enemydata.health;
    }
    public void SetHealth(float damage)
    {
        _enemydata.health -= damage;
        if (_enemydata.health <= 0.0f)
        {
            _enemydata.isDead = true;
        }
    }
    public void Save()
    {
        string s = JsonUtility.ToJson(_enemydata);
        FileManager.WriteToFile("temp.json", s);
    }
    public void Load()
    {
        if (FileManager.LoadFromFile("temp.json", out string s))
        {
            JsonUtility.FromJsonOverwrite(s, this);
        }
    }
}

[System.Serializable]
public class enemydata
{
    public float health;
    public float attack;
    public Sprite enemySprite;
    public string name;
    public bool isDead;
}