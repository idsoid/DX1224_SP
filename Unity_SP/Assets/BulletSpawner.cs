using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BulletSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private CombatData combatData; 

    private float fTime_elapsed;
    // Start is called before the first frame update
    void Start()
    {
        fTime_elapsed = 0f;

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
                fTime_elapsed += Time.deltaTime;
                if (fTime_elapsed > 1f)
                {
                    Bullet1();
                    fTime_elapsed = 0f;
                }
                break;
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
