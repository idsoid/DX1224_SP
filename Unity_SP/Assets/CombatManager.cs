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

    [SerializeField]
    private CombatData combatData;

    [SerializeField]
    private GameObject eventsys;

    [SerializeField]
    private Image enemySprite;


    public GameObject actionSelect;
    public GameObject enemySelect;

    private EnemyData enemyData;

    bool playerTurn = true;

    [SerializeField]
    private string sceneName;
    public enum ACTION
    {
        ATTACK,
        SKILL,
        HEAL,
        NONE
    }

    public ACTION action;

    float fTime_elapsed = 0f;

    //multiscene
    public string bhSceneName;
    private PhysicsScene2D bhPhysicsScene;
    private Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        enemySelect.SetActive(false);
        action = ACTION.NONE;
        enemyData = combatData.enemyData;
        enemySprite.sprite = combatData.enemyData.GetSprite();

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
                if (!scene.isLoaded)
                {
                    eventsys.SetActive(false);
                    LoadSceneParameters param = new LoadSceneParameters(LoadSceneMode.Additive, LocalPhysicsMode.Physics2D);
                    scene = SceneManager.LoadScene(bhSceneName, param);
                    bhPhysicsScene = scene.GetPhysicsScene2D();
                }
                if (bhPhysicsScene != null)
                {
                    bhPhysicsScene.Simulate(Time.deltaTime);
                }
            }
            if (fTime_elapsed > 15f)
            {
                SceneManager.UnloadSceneAsync(scene);
                actionSelect.SetActive(true);
                playerTurn = true;
                eventsys.SetActive(true);
            }
            //playerData.AlterValue("health", -10);
            //actionSelect.SetActive(true);
            //playerTurn = true;
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
                CheckDead();
                break;
            case ACTION.SKILL:
                playerData.AlterValue("hunger", -20);
                enemyData.SetHealth(playerData.GetValue("attack") * 2);
                playerTurn = false;
                enemySelect.SetActive(false);
                CheckDead();
                break;
            case ACTION.HEAL:
                break;
            case ACTION.NONE:
                break;
        }

        fTime_elapsed = 0f;
    }

    public void CheckDead()
    {
        if (enemyData.GetDead())
        {
            combatData.enemyData = enemyData;
            playerData.Save();
            SceneManager.LoadScene(sceneName);
        }
    }
}
