using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;


public class UpdateHUD : MonoBehaviour
{
    [SerializeField] private AudioHandler sfxHandler;

    public Image healthBar;
    public Image staminaBar;
    public Image hungerBar;
    //public Image frostBar;
    public GameObject frost1;
    public GameObject frost2;
    public GameObject frost3;
    public GameObject frost4;

    [SerializeField]
    public Image candle;

    public ParticleSystem snowflake;

    public Image vignette;

    public PlayerData playerData;

    float lerpSpeed;

    float quintTemp;

    public TMP_Text stats;

    // Update is called once per frame
    void Update()
    {
        quintTemp = playerData.GetValue("maxTemperature") / 5;
        if (!playerData.safe)
        {
            if (frost1.activeSelf)
                sfxHandler.playAudio("frost");

            if (playerData.GetValue("temperature") < quintTemp)
            {
                if (!frost4.activeSelf)
                {
                    vignette.color = new UnityEngine.Color(vignette.color.r, vignette.color.g, vignette.color.b, 0.313725f);
                    frost4.SetActive(true);
                }
            }
            else if (playerData.GetValue("temperature") < quintTemp * 2)
            {
                if (!frost3.activeSelf)
                {
                    vignette.color = new UnityEngine.Color(vignette.color.r, vignette.color.g, vignette.color.b, 0.23529f);
                    frost3.SetActive(true);
                }
            }
            else if (playerData.GetValue("temperature") < quintTemp * 3)
            {
                if (!frost2.activeSelf)
                {
                    vignette.color = new UnityEngine.Color(vignette.color.r, vignette.color.g, vignette.color.b, 0.17647f);
                    frost2.SetActive(true);
                }
            }
            else if (playerData.GetValue("temperature") < quintTemp * 4)
            {
                if (!frost1.activeSelf)
                {
                    snowflake.gameObject.SetActive(true);
                    vignette.color = new UnityEngine.Color(vignette.color.r, vignette.color.g, vignette.color.b, 0.098039f);
                    frost1.SetActive(true);
                }
            }
        }

        else if(playerData.safe)
        {
            if(playerData.GetValue("temperature") > quintTemp)
            {
                if (frost4.activeSelf)
                {
                    vignette.color = new UnityEngine.Color(vignette.color.r, vignette.color.g, vignette.color.b, 0.23529f);
                    frost4.SetActive(false);
                }
                    
            }

            if (playerData.GetValue("temperature") > quintTemp * 2)
            {
                if (frost3.activeSelf)
                {
                    vignette.color = new UnityEngine.Color(vignette.color.r, vignette.color.g, vignette.color.b, 0.17647f);
                    frost3.SetActive(false);
                }
            }

            if (playerData.GetValue("temperature") > quintTemp * 3)
            {
                if (frost2.activeSelf)
                {
                    vignette.color = new UnityEngine.Color(vignette.color.r, vignette.color.g, vignette.color.b, 0.098039f);
                    frost2.SetActive(false);
                }
            }


            if(playerData.GetValue("temperature") == playerData.GetValue("maxTemperature"))
            {
                if (frost1.activeSelf)
                {
                    snowflake.gameObject.SetActive(false);
                    vignette.color = new UnityEngine.Color(vignette.color.r, vignette.color.g, vignette.color.b, 0f);
                    frost1.SetActive(false);
                }
            }
        }
        lerpSpeed = 3f * Time.deltaTime;

        setBars();

        stats.text = "Max Health: " + playerData.GetValue("maxHealth") + "\n Max Stamina: " + playerData.GetValue("maxStamina")
            + "\n Max Hunger: " + playerData.GetValue("maxHunger") + "\n Attack: " + playerData.GetValue("attack")
            + "\n Speed: " + playerData.GetValue("speed");
    }

    private void setBars()
    {
        healthBar.fillAmount = playerData.GetValue("health") / playerData.GetValue("maxHealth");
        staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, playerData.GetValue("stamina") / playerData.GetValue("maxStamina"), lerpSpeed);
        hungerBar.fillAmount = Mathf.Lerp(hungerBar.fillAmount, playerData.GetValue("hunger") / playerData.GetValue("maxHunger"), lerpSpeed);

        UnityEngine.Color color = candle.color;
        color.a = playerData.GetValue("candleBurnTime") / 180f;
        candle.color = color;
        //frostBar.fillAmount = Mathf.Lerp(frostBar.fillAmount, (playerData.GetValue("maxTemperature") - playerData.GetValue("temperature")) / playerData.GetValue("maxTemperature"), lerpSpeed);
    }
}
