using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

public class AbyssHoarder : MonoBehaviour
{
    [SerializeField]
    private GameObject mapCol;
    [SerializeField]
    private CombatData combatData;
    [SerializeField]
    private EnemyData enemyData;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private List<GameObject> objWaypoints = new();
    [SerializeField]
    private Transform enemySprite;
    private int targetIndex;
    private float totalTime;
    private float speed = 200f;
    private float nextWaypointDistance = 1f;
    private float rezTime;

    public bool isDead = false;

    Path path;
    private int currentWaypoint = 0;
    private Transform target;

    Seeker seeker;
    Rigidbody2D rb;

    private State currentState;
    public enum State
    {
        IDLE,
        PATROL,
        AGGRO
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.collider, mapCol.GetComponent<Collider2D>());
            playerData.SavePos(player.transform.position);
            combatData.enemyData = enemyData;
            SceneManager.LoadScene("CombatScene");
        }
        if (collision.gameObject.CompareTag("Walls"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        currentState = State.PATROL;
        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
        target = objWaypoints[0].GetComponent<Transform>();
        Physics2D.IgnoreLayerCollision(2, 8);
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), mapCol.GetComponent<Collider2D>());
        rezTime = 0.0f;
        if (enemyData.GetHealth() != 0.0f)
        {
            enemyData.Init(60, 20, enemySprite.GetComponent<SpriteRenderer>().sprite, "Abyss Hoarder", "HOARDER");
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //enemyData.SetDead(isDead);
        FSM();

        if (path == null || currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }
        Vector2 force = speed * Time.deltaTime * ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        if (enemyData.GetDead() && rezTime <= 0.0f)
        {
            rezTime = 10.0f;
            GetComponent<Collider2D>().enabled = false;
            enemySprite.gameObject.SetActive(false);
        }
        else if (!enemyData.GetDead())
        {
            rb.AddForce(force);
            GetComponent<Collider2D>().enabled = true;
            enemySprite.gameObject.SetActive(true);
        }

        if (rezTime > 0.0f)
        {
            rezTime -= Time.deltaTime;
            if (rezTime <= 0.0f)
            {
                rezTime = 0.0f;
                enemyData.SetDead(false);
                isDead = false;
                enemyData.ResetHealth(60);
            }
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        if (rb.velocity.x >= 0.01f)
        {
            enemySprite.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            enemySprite.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }
    private void FSM()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.transform.position);
        if (!hit.collider.gameObject.CompareTag("Player"))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);
        }
        else
        {
            Debug.DrawLine(transform.position, player.transform.position, Color.green);
        }

        switch (currentState)
        {
            case State.IDLE:
                totalTime += Time.deltaTime;
                if (totalTime >= 1.0f)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0f;
                    targetIndex++;
                    targetIndex %= objWaypoints.Count;
                    currentState = State.PATROL;
                }
                else if (player.GetComponent<PlayerController>().lightOn && Vector3.Distance(player.transform.position, transform.position) <= 20.0f)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0f;
                    speed = 300.0f;
                    currentState = State.AGGRO;
                }
                break;
            case State.PATROL:
                target = objWaypoints[targetIndex].GetComponent<Transform>();
                if (Vector3.Distance(target.position, transform.position) <= 0.5f)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0f;
                    totalTime = 0.0f;
                    currentState = State.IDLE;
                }
                else if (player.GetComponent<PlayerController>().lightOn && Vector3.Distance(player.transform.position, transform.position) <= 20.0f && hit.collider.gameObject.CompareTag("Player"))
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0f;
                    currentState = State.AGGRO;
                }
                break;
            case State.AGGRO:
                target = player.transform;
                if (!player.GetComponent<PlayerController>().lightOn)
                {
                    speed = 300.0f;
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0f;
                    totalTime = 0.0f;
                    currentState = State.IDLE;
                }
                break;
            default:
                break;
        }
    }
}
