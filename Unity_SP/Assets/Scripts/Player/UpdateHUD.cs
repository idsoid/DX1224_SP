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
    public GameObject temperatureIndicator;

    public playerData pData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //temperatureIndicator.Find("frost1").gameObject.SetActive(true);
    }

    private void setBars()
    {
        //healthBar.fillAmount = pData.health / 
    }
}
