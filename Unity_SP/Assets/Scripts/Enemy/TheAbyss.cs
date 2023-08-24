using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

public class TheAbyss : MonoBehaviour
{
    [SerializeField]
    private CombatData combatData;
    [SerializeField]
    private EnemyData enemyData;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private Transform enemySprite;
    public bool isDead = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerData.SavePos(player.transform.position);
            combatData.enemyData = enemyData;
            SceneManager.LoadScene("CombatScene");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (enemyData.GetSprite() == null)
        {
            enemyData.Init(300, 15, enemySprite.GetComponent<SpriteRenderer>().sprite, "The Abyss", "BOSS");
        }
        if (enemyData.GetDead())
        {
            gameObject.SetActive(false);
        }
    }
}
