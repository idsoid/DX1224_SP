using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletSpawner : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject player;
    [SerializeField]private CombatData combatData;
    [SerializeField] private float timeToStartHell;

    [Header("Bullet Hell Prefabs")]
    [SerializeField] GameObject rotatingSquare;
    [SerializeField] GameObject zigZagbullet;
    [SerializeField] GameObject longBar;
    [SerializeField] GameObject verticalSet;
    [SerializeField] GameObject horizontalSet;
    [SerializeField] GameObject dangerZone;
    [SerializeField] GameObject sweeper;
    [SerializeField] GameObject laserH;
    [SerializeField] GameObject laserV;
    [SerializeField] GameObject dangerLaserH;
    [SerializeField] GameObject dangerLaserV;
    [SerializeField] GameObject orange;
    [SerializeField] GameObject blue;

    private bool startHell;
    private bool firstSpawn;

    // =============== Hoarder Scene Variables ===============
    private HorizontalSet spawnedlaserH;
    private VerticalSet spawnedlaserV;
    // =======================================================

    // =============== Boss Scene Variables ===============
    private int randomizeAttack;
    private float sweeperRespawnTime;
    private HorizontalSet spawnedHorizontalSet;
    private VerticalSet spawnedVerticalSet;
    private float safeTime;
    private bool danger;
    private Vector2 safeCenter;
    private float barRespawnTime;
    // ====================================================

    // Start is called before the first frame update
    void Start()
    {
        startHell = false;
        firstSpawn = true;
        sweeperRespawnTime = 3f;
        barRespawnTime = 1f;

        if (combatData.enemyData.GetEnemyType() == EnemyData.CATS.BOSS)
            randomizeAttack = Random.Range(1, 4);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject spawnedBullet;
        switch (combatData.enemyData.GetEnemyType())
        {
            case EnemyData.CATS.KEEPER:
                if (startHell && firstSpawn)
                {
                    spawnedBullet = Instantiate(rotatingSquare, new Vector2(player.transform.position.x, player.transform.position.y), Quaternion.identity);
                    SceneManager.MoveGameObjectToScene(spawnedBullet, SceneManager.GetSceneByName("BulletHellScene"));
                    spawnedBullet.GetComponent<RotatingSquare>().SetTarget(player);
                    firstSpawn = false;
                }
                break;
            case EnemyData.CATS.CRAWLER:
                if (startHell && firstSpawn)
                {
                    int opp = 1;

                    for (int i = 0; i < 2; i++)
                    {
                        if (i == 1)
                            opp = -1;
                        spawnedBullet = Instantiate(zigZagbullet, new Vector2(7f * opp, 2.5f * opp), Quaternion.identity);
                        SceneManager.MoveGameObjectToScene(spawnedBullet, SceneManager.GetSceneByName("BulletHellScene"));
                        spawnedBullet.GetComponent<ZigZagBullet>().SetSpeed(4f);
                        spawnedBullet = Instantiate(zigZagbullet, new Vector2(9f * opp, 2.3f * opp), Quaternion.identity);
                        SceneManager.MoveGameObjectToScene(spawnedBullet, SceneManager.GetSceneByName("BulletHellScene"));
                        spawnedBullet.GetComponent<ZigZagBullet>().SetSpeed(3f);
                    }
                    firstSpawn = false;
                }
                break;
            case EnemyData.CATS.IMITATER:
                if (startHell && firstSpawn)
                {
                    spawnedBullet = Instantiate(longBar, new Vector2(8.1f, 0f), Quaternion.identity);
                    SceneManager.MoveGameObjectToScene(spawnedBullet, SceneManager.GetSceneByName("BulletHellScene"));
                    firstSpawn = false;
                }
                break;
            case EnemyData.CATS.HOARDER:
                if (startHell && firstSpawn)
                {
                    spawnedBullet = Instantiate(laserV, new Vector2(0f, Random.Range(-2.4f, 2.4f)), Quaternion.identity);
                    SceneManager.MoveGameObjectToScene(spawnedBullet, SceneManager.GetSceneByName("BulletHellScene"));
                    spawnedlaserV = spawnedBullet.GetComponent<VerticalSet>();
                    spawnedBullet = Instantiate(laserH, new Vector2(Random.Range(-5.2f, 5.2f), 0f), Quaternion.identity);
                    SceneManager.MoveGameObjectToScene(spawnedBullet, SceneManager.GetSceneByName("BulletHellScene"));
                    spawnedlaserH = spawnedBullet.GetComponent<HorizontalSet>();
                    safeTime = 1.5f;
                    danger = false;
                    firstSpawn = false;
                }

                if (!firstSpawn)
                {
                    if (spawnedlaserH.GetSet() &&
                    spawnedlaserV.GetSet())
                    {
                        if (safeTime > 0f)
                            safeTime -= Time.deltaTime;
                        else if (safeTime < 0f)
                        {
                            safeCenter = new Vector2(spawnedlaserH.GetComponent<HorizontalSet>().GetHorizontal(),
                                spawnedlaserV.GetComponent<VerticalSet>().GetVertical());

                            danger = true;
                            safeTime = 0f;
                        }

                        if (danger)
                        {
                            danger = false;

                            spawnedBullet = Instantiate(dangerLaserH, spawnedlaserH.transform.position, Quaternion.identity);
                            SceneManager.MoveGameObjectToScene(spawnedBullet, SceneManager.GetSceneByName("BulletHellScene"));
                            spawnedBullet = Instantiate(dangerLaserV, spawnedlaserV.transform.position, Quaternion.identity);
                            SceneManager.MoveGameObjectToScene(spawnedBullet, SceneManager.GetSceneByName("BulletHellScene"));

                            Destroy(spawnedlaserH.gameObject);
                            Destroy(spawnedlaserV.gameObject);
                        }
                    }
                }
                break;
            case EnemyData.CATS.BOSS:
                switch (randomizeAttack)
                {
                    case 1:
                        if (startHell && firstSpawn)
                        {
                            spawnedBullet = Instantiate(sweeper, player.transform.position, Quaternion.identity);
                            spawnedBullet.transform.right = (player.transform.position - new Vector3(0f, 0f)).normalized;
                            SceneManager.MoveGameObjectToScene(spawnedBullet, SceneManager.GetSceneByName("BulletHellScene"));
                            firstSpawn = false;
                        }

                        if (!firstSpawn)
                        {
                            if (sweeperRespawnTime > 0)
                                sweeperRespawnTime -= Time.deltaTime;
                            else if (sweeperRespawnTime < 0)
                            {
                                spawnedBullet = Instantiate(sweeper, player.transform.position, Quaternion.identity);
                                spawnedBullet.transform.right = (new Vector3(0f, 0f) - player.transform.position).normalized;
                                SceneManager.MoveGameObjectToScene(spawnedBullet, SceneManager.GetSceneByName("BulletHellScene"));
                                sweeperRespawnTime = 3f;
                            }
                        }
                        break;
                    case 2:
                        if (startHell && firstSpawn)
                        {
                            spawnedBullet = Instantiate(verticalSet, new Vector2(0f, Random.Range(-2.4f, 2.4f)), Quaternion.identity);
                            SceneManager.MoveGameObjectToScene(spawnedBullet, SceneManager.GetSceneByName("BulletHellScene"));
                            spawnedVerticalSet = spawnedBullet.GetComponent<VerticalSet>();
                            spawnedBullet = Instantiate(horizontalSet, new Vector2(Random.Range(-5.2f, 5.2f), 0f), Quaternion.identity);
                            SceneManager.MoveGameObjectToScene(spawnedBullet, SceneManager.GetSceneByName("BulletHellScene"));
                            spawnedHorizontalSet = spawnedBullet.GetComponent<HorizontalSet>();
                            safeTime = 1.5f;
                            danger = false;
                            firstSpawn = false;
                        }

                        if (!firstSpawn)
                        {
                            if (spawnedHorizontalSet.GetSet() &&
                            spawnedVerticalSet.GetSet())
                            {
                                if (safeTime > 0f)
                                    safeTime -= Time.deltaTime;
                                else if (safeTime < 0f)
                                {
                                    safeCenter = new Vector2(spawnedHorizontalSet.GetComponent<HorizontalSet>().GetHorizontal(),
                                        spawnedVerticalSet.GetComponent<VerticalSet>().GetVertical());

                                    danger = true;
                                    safeTime = 0f;
                                }

                                if (danger)
                                {
                                    danger = false;

                                    spawnedBullet = Instantiate(dangerZone, new Vector2(safeCenter.x + 6.25f, 0f), Quaternion.identity);
                                    SceneManager.MoveGameObjectToScene(spawnedBullet, SceneManager.GetSceneByName("BulletHellScene"));
                                    spawnedBullet = Instantiate(dangerZone, new Vector2(safeCenter.x - 6.25f, 0f), Quaternion.identity);
                                    SceneManager.MoveGameObjectToScene(spawnedBullet, SceneManager.GetSceneByName("BulletHellScene"));
                                    spawnedBullet = Instantiate(dangerZone, new Vector2(0f, safeCenter.y + 6.25f), Quaternion.identity);
                                    SceneManager.MoveGameObjectToScene(spawnedBullet, SceneManager.GetSceneByName("BulletHellScene"));
                                    spawnedBullet = Instantiate(dangerZone, new Vector2(0f, safeCenter.y - 6.25f), Quaternion.identity);
                                    SceneManager.MoveGameObjectToScene(spawnedBullet, SceneManager.GetSceneByName("BulletHellScene"));

                                    Destroy(spawnedHorizontalSet.gameObject);
                                    Destroy(spawnedVerticalSet.gameObject);
                                }
                            }
                        }
                        break;
                    case 3:
                        if (startHell && firstSpawn)
                        {
                            int randomizeColor = Random.Range(1, 3);

                            if (randomizeColor == 1)
                                spawnedBullet = Instantiate(orange, new Vector3(8f, 0f, 0f), Quaternion.identity);
                            else
                                spawnedBullet = Instantiate(blue, new Vector3(8f, 0f, 0f), Quaternion.identity);

                            SceneManager.MoveGameObjectToScene(spawnedBullet, SceneManager.GetSceneByName("BulletHellScene"));
                            firstSpawn = false;
                        }

                        if (!firstSpawn)
                        {
                            if (barRespawnTime > 0f)
                                barRespawnTime -= Time.deltaTime;
                            else if (barRespawnTime < 0f)
                            {
                                int randomizeColor = Random.Range(1, 3);

                                if (randomizeColor == 1)
                                    spawnedBullet = Instantiate(orange, new Vector3(8f, 0f, 0f), Quaternion.identity);
                                else
                                    spawnedBullet = Instantiate(blue, new Vector3(8f, 0f, 0f), Quaternion.identity);

                                SceneManager.MoveGameObjectToScene(spawnedBullet, SceneManager.GetSceneByName("BulletHellScene"));
                                barRespawnTime = 1f;
                            }
                        }
                        break;
                }
                break;
            default:
                Debug.Log("Error: Invalid enemy type");
                break;
        }

        if (timeToStartHell > 0)
            timeToStartHell -= Time.deltaTime;
        else if (timeToStartHell < 0)
        {
            startHell = true;
            timeToStartHell = 0;
        }
    }
}
