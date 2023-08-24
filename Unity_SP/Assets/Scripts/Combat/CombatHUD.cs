using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatHUD : MonoBehaviour
{
    public Image healthBar;

    [SerializeField] private PlayerData playerData;

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = playerData.GetValue("health") / playerData.GetValue("maxHealth");
    }
}
