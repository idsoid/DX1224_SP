using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    public enum CATS
    {
        KEEPER,
        CRAWLER,
        IMITATER,
        HOARDER,
        BOSS
    }
    private CATS type;
    private float health;
    private float attack;
    private Sprite enemySprite;
    private string enemyName;
    private bool isDead;

    public void Init(float newhealth, float newattack, Sprite eSprite, string eName, string eCat)
    {
        health = newhealth;
        attack = newattack;
        enemySprite = eSprite;
        enemyName = eName;
        isDead = false;
        switch (eCat)
        {
            case "KEEPER":
                type = CATS.KEEPER;
                break;
            case "CRAWLER":
                type = CATS.CRAWLER;
                break;
            case "IMITATER":
                type = CATS.IMITATER;
                break;
            case "HOARDER":
                type = CATS.HOARDER;
                break;
            case "BOSS":
                type = CATS.BOSS;
                break;
            default:
                break;
        }
    }
    public bool GetDead()
    {
        return isDead;
    }
    public void SetDead(bool state)
    {
        isDead = state;
    }
    public float GetHealth()
    {
        return health;
    }
    public void SetHealth(float damage)
    {
        health -= damage;
        if (health <= 0.0f)
        {
            SetDead(true);
        }
    }
    public void ResetHealth(float newhealth)
    {
        health = newhealth;
    }
    public CATS GetEnemyType()
    {
        return type;
    }
    public Sprite GetSprite()
    {
        return enemySprite;
    }
    public string GetName()
    {
        return enemyName;
    }
    public float GetAttack()
    {   
        return attack; 
    }
}