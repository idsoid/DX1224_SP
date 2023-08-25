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
        if (healthBar.gameObject != null)
            healthBar.fillAmount = playerData.GetValue("health") / playerData.GetValue("maxHealth");
        if (hungerBar.gameObject != null)
            hungerBar.fillAmount = playerData.GetValue("hunger") / playerData.GetValue("maxHunger");
        if(enemyBar.gameObject != null)
            enemyBar.fillAmount = combatData.enemyData.GetHealth() / enemyMaxHealth;
    }
}
