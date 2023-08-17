using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


public class UpdateHUD : MonoBehaviour
{
    public Image healthBar;
    public Image staminaBar;
    public Image hungerBar;
    public GameObject frost1;
    public GameObject frost2;
    public GameObject frost3;
    public GameObject frost4;

    public PlayerData playerData;

    float lerpSpeed;

    float quintTemp;


    // Update is called once per frame
    void Update()
    {
        quintTemp = playerData.GetValue("maxTemperature") / 4;

        if (playerData.GetValue("temperature") < quintTemp)
        {
            if (frost4.activeSelf)
            {
                //frost4.SetActive(false);
            }
            else if (!frost4.activeSelf)
            {
                frost4.SetActive(true);
                frost3.SetActive(false);
            }
        }
        else if (playerData.GetValue("temperature") < quintTemp * 2)
        {
            if (frost3.activeSelf)
            {
                //frost3.SetActive(false);
            }
            else if (!frost3.activeSelf)
            {
                frost3.SetActive(true);
                frost2.SetActive(false);
            }
        }
        else if (playerData.GetValue("temperature") < quintTemp * 3)
        {
            if (frost2.activeSelf)
            {
                //frost2.SetActive(false);
            }
            else if (!frost2.activeSelf)
            {
                frost2.SetActive(true);
                frost1.SetActive(false);
            }
        }
        else if (playerData.GetValue("temperature") < quintTemp * 4)
        {
            if (frost1.activeSelf)
            {
                //frost1.SetActive(false);
            }
            else if (!frost1.activeSelf)
            {
                frost1.SetActive(true);
            }
        }
        else
        {
            frost1.SetActive(false);
            frost2.SetActive(false);
            frost3.SetActive(false);
            frost4.SetActive(false);
        }

        lerpSpeed = 3f * Time.deltaTime;

        setBars();
    }

    private void setBars()
    {
        healthBar.fillAmount = playerData.GetValue("health") / playerData.GetValue("maxHealth");
        staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, playerData.GetValue("stamina") / playerData.GetValue("maxStamina"), lerpSpeed);
        hungerBar.fillAmount = Mathf.Lerp(hungerBar.fillAmount, playerData.GetValue("hunger") / playerData.GetValue("maxHunger"), lerpSpeed);
    }
}