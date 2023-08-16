using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text playertext;

    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private TMP_Text enemytext;

    public GameObject actionSelect;
    public GameObject enemySelect;

    private EnemyData enemyData;

    bool playerTurn = true;
    public enum ACTION
    {
        ATTACK,
        SKILL,
        HEAL,
        NONE
    }

    public ACTION action;

    float fTime_elapsed = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
        enemySelect.SetActive(false);
        action = ACTION.NONE;
        enemyData = ScriptableObject.CreateInstance<EnemyData>();
        enemyData.Load();
        enemyData.Init(100, 10, null, "coc");
    }

    // Update is called once per frame
    void Update()
    {
        playertext.text = playerData.PrintSelf();

        enemytext.text = enemyData.name + "\n" + System.Convert.ToString(enemyData.GetHealth());

        if(!playerTurn)
        {
            fTime_elapsed += Time.deltaTime;
            if(fTime_elapsed > 1f)
            {
                playerData.AlterValue("health", -10);
                actionSelect.SetActive(true);
                playerTurn = true;
            }
        }
    }

    public void OnAttackButtonClick()
    {
        action = ACTION.ATTACK;
        actionSelect.SetActive(false);
        enemySelect.SetActive(true);
    }

    public void OnSkillButtonClick()
    {
        action = ACTION.SKILL;
        actionSelect.SetActive(false);
        enemySelect.SetActive(true);
    }

    public void OnHealButtonClick()
    {
        action = ACTION.HEAL;
    }

    public void OnBackButtonClick()
    {
        action = ACTION.NONE;
        actionSelect.SetActive(true);
        enemySelect.SetActive(false);
    }

    public void TakeAction()
    {
        switch(action)
        {
            case ACTION.ATTACK:
                enemyData.SetHealth(playerData.GetValue("attack"));
                playerTurn = false;
                enemySelect.SetActive(false);
                if (enemyData.GetDead())
                {
                    SceneManager.LoadScene("andrewScene");
                }
                break;
            case ACTION.SKILL:
                enemyData.SetHealth(playerData.GetValue("attack") * 2);
                playerTurn = false;
                enemySelect.SetActive(false);
                if (enemyData.GetDead())
                {
                    SceneManager.LoadScene("andrewScene");
                }
                break;
            case ACTION.HEAL:
                break;
            case ACTION.NONE:
                break;
        }

        fTime_elapsed = 0f;
    }
}
