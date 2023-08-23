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

    private float fTime_elapsed;
    private bool startHell;
    private bool firstSpawn;

    // Start is called before the first frame update
    void Start()
    {
        fTime_elapsed = 0f;
        startHell = false;
        firstSpawn = true;

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
                    GameObject spawnedBullet = Instantiate(rotatingSquare, new Vector2(player.transform.position.x, player.transform.position.y), Quaternion.identity);
                    spawnedBullet.GetComponent<RotatingSquare>().SetTarget(player);
                    firstSpawn = false;
                }
                break;
            case enemydata.CATS.CRAWLER:
                CrawlerBH();
                break;
            case enemydata.CATS.IMITATER:
                ImitaterBH();
                break;
            case enemydata.CATS.HOARDER:
                HoarderBH();
                break;
            default:
                fTime_elapsed += Time.deltaTime;
                if (fTime_elapsed > 1f)
                {
                    Bullet1();
                    fTime_elapsed = 0f;
                }
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

    private void CrawlerBH()
    {

    }

    private void ImitaterBH()
    {

    }

    private void HoarderBH()
    {

    }
}
