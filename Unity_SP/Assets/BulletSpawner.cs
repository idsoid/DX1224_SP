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

    private bool startHell;
    private bool firstSpawn;
    private float fTime_Elapsed;

    public HorizontalSet spawnedHorizontalSet;
    public VerticalSet spawnedVerticalSet;
    private float safeTime;
    private bool danger;
    private Vector2 safeCenter;

    // Start is called before the first frame update
    void Start()
    {
        startHell = false;
        firstSpawn = true;
        fTime_Elapsed = 0f;

        switch (combatData.enemyData.GetEnemyType())
        {
            case enemydata.CATS.KEEPER:
                break;
            case enemydata.CATS.CRAWLER:
                break;
            case enemydata.CATS.IMITATER:
                break;
            case enemydata.CATS.HOARDER:
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (combatData.enemyData.GetEnemyType()) // combatData.enemyData.GetEnemyType()
        {
            case enemydata.CATS.KEEPER:
                if (startHell && firstSpawn)
                {
                    GameObject spawnedBullet;
                    spawnedBullet = Instantiate(rotatingSquare, new Vector2(player.transform.position.x, player.transform.position.y), Quaternion.identity);
                    spawnedBullet.GetComponent<RotatingSquare>().SetTarget(player);
                    firstSpawn = false;
                }
                break;
            case enemydata.CATS.CRAWLER:
                if (startHell && firstSpawn)
                {
                    GameObject spawnedBullet;
                    int opp = 1;

                    for (int i = 0; i < 2; i++)
                    {
                        if (i == 1)
                            opp = -1;
                        spawnedBullet = Instantiate(zigZagbullet, new Vector2(7f * opp, 2.5f * opp), Quaternion.identity);
                        spawnedBullet.GetComponent<ZigZagBullet>().SetSpeed(4f);
                        spawnedBullet = Instantiate(zigZagbullet, new Vector2(9f * opp, 2.3f * opp), Quaternion.identity);
                        spawnedBullet.GetComponent<ZigZagBullet>().SetSpeed(3f);
                    }
                    firstSpawn = false;
                }
                break;
            case enemydata.CATS.IMITATER:
                if (startHell && firstSpawn)
                {
                    Instantiate(longBar, new Vector2(8.1f, 0f), Quaternion.identity);
                    firstSpawn = false;
                }
                break;
            case enemydata.CATS.HOARDER:
                if (startHell && firstSpawn)
                {
                    GameObject spawnedBullet;
                    spawnedBullet = Instantiate(verticalSet, new Vector2(0f, Random.Range(-2.4f, 2.4f)), Quaternion.identity);
                    spawnedVerticalSet = spawnedBullet.GetComponent<VerticalSet>();
                    spawnedBullet = Instantiate(horizontalSet, new Vector2(Random.Range(-5.2f, 5.2f), 0f), Quaternion.identity);
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

                            Instantiate(dangerZone, new Vector2(safeCenter.x + 6.25f, 0f), Quaternion.identity);
                            Instantiate(dangerZone, new Vector2(safeCenter.x - 6.25f, 0f), Quaternion.identity);
                            Instantiate(dangerZone, new Vector2(0f, safeCenter.y + 6.25f), Quaternion.identity);
                            Instantiate(dangerZone, new Vector2(0f, safeCenter.y - 6.25f), Quaternion.identity);

                            Destroy(spawnedHorizontalSet.gameObject);
                            Destroy(spawnedVerticalSet.gameObject);
                        }
                    }
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

    private void Bullet1()
    {
        GameObject bullet = Instantiate(projectile, new Vector3(Random.Range(-5, 5), Random.Range(-5, 5)), Quaternion.identity);
        SceneManager.MoveGameObjectToScene(bullet,SceneManager.GetSceneByName("BulletHellScene"));
        Vector3 dir = (player.transform.position - bullet.transform.position).normalized * 50f;
        bullet.GetComponent<Rigidbody2D>().AddForce(dir, ForceMode2D.Impulse);
    }
}
