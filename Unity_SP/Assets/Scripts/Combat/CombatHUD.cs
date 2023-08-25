using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatHUD : MonoBehaviour
{
    public Image healthBar;
    public Image enemyBar;
    public Image hungerBar;
    private float enemyMaxHealth;

    [SerializeField] private PlayerData playerData;
    [SerializeField] private CombatData combatData;
    
    void Start()
    {
        enemyMaxHealth = combatData.enemyData.GetHealth();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        healthBar.fillAmount = playerData.GetValue("health") / playerData.GetValue("maxHealth");
        hungerBar.fillAmount = playerData.GetValue("hunger") / playerData.GetValue("maxHunger");
        enemyBar.fillAmount = combatData.enemyData.GetHealth() / enemyMaxHealth;
    }
}
