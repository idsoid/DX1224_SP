using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CombatManager : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private CombatData combatData;

    [SerializeField]
    private GameObject eventsys;

    [SerializeField]
    private Image enemySprite;

    [SerializeField]
    private GameObject attack;

    [SerializeField]
    private TMP_Text enemyText;


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
        enemyText.text = combatData.enemyData.GetName();
        //UpdateData();
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerTurn)
        {
            fTime_elapsed += Time.deltaTime;
            if(fTime_elapsed > 1f)
            {
                
                CheckDead();

                if (!enemyData.GetDead())
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
            }

            if (fTime_elapsed > 10f)
            {
                SceneManager.UnloadSceneAsync(scene);
                actionSelect.SetActive(true);
                playerTurn = true;
                eventsys.SetActive(true);

                //UpdateData();
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
                Attack();
                playerTurn = false;
                enemySelect.SetActive(false);
                break;
            case ACTION.SKILL:
                if (playerData.GetValue("hunger") > 5)
                {
                    playerData.AlterValue("hunger", -5);
                    enemyData.SetHealth(playerData.GetValue("attack") * 2);
                    Attack();
                    playerTurn = false;
                    enemySelect.SetActive(false);
                }
                break;
            case ACTION.HEAL:
                playerData.AlterValue("health", 10);
                playerTurn = false;
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
            playerData.loadOldPos = true;
            playerData.ReadyLoad = true;
            playerData.temphp = playerData.GetValue("health");
            playerData.temphunger = playerData.GetValue("hunger");
            playerData.Save();
            SceneManager.LoadScene(sceneName);
        }
    }

    //public void UpdateData()
    //{
    //    playertext.text = playerData.PrintSelf();
    //    enemytext.text = enemyData.GetName() + "\n" + System.Convert.ToString(enemyData.GetHealth());
    //}

    private void Attack()
    {
        attack.SetActive(true);
    }
}
