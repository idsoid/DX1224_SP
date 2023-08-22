using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

public class AbyssCrawler : MonoBehaviour
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
    private float speed = 300f;
    private float nextWaypointDistance = 1f;
    private Transform scaryAlert;
    private float raycastDistance = 5.0f;
    private float rezTime;

    public bool lightOn = false;
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
        CHASE,
        FLEE
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Flashlight"))
        {
            lightOn = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Flashlight"))
        {
            lightOn = false;
        }
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
<<<<<<< Updated upstream
        scaryAlert = target;
        enemyData.Init(50, 10, enemySprite.GetComponent<Sprite>(), gameObject.name, "CRAWLER");
        Physics2D.IgnoreLayerCollision(2, 8);
        rezTime = 0.0f;
=======
        furthest = null;
        enemyData.Init(50, 10, enemySprite.GetComponent<SpriteRenderer>().sprite, gameObject.name, "CRAWLER");
>>>>>>> Stashed changes
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
            rezTime = 30.0f;
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
                enemyData.SetDead(false);
                isDead = false;
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
        Vector2 moveDir = rb.velocity.normalized;
        Vector2 origin = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, moveDir, raycastDistance);
        if (hit.collider != null && !hit.collider.gameObject.CompareTag("Enemy"))
        {
            Debug.DrawLine(origin, hit.point, Color.red);
        }
        else
        {
            Debug.DrawRay(origin, moveDir * raycastDistance, Color.green);
        }

        switch (currentState)
        {
            case State.IDLE:
                totalTime += Time.deltaTime;
                scaryAlert = objWaypoints[targetIndex].GetComponent<Transform>();
                if (totalTime >= 1.0f)
                {
                    targetIndex++;
                    targetIndex %= objWaypoints.Count;
                    currentState = State.PATROL;
                }
                else if (lightOn)
                {
                    rb.velocity = Vector3.zero;
                    speed *= 5;
                    currentState = State.FLEE;
                }
                break;
            case State.PATROL:
                target = objWaypoints[targetIndex].GetComponent<Transform>();
                if (Vector3.Distance(target.position, transform.position) <= 0.5f)
                {
                    totalTime = 0.0f;
                    currentState = State.IDLE;
                }
                else if (Vector3.Distance(player.transform.position, transform.position) <= 5.0f && hit.collider == null)
                {
                    currentState = State.CHASE;
                }
                else if (lightOn)
                {
                    rb.velocity = Vector3.zero;
                    speed *= 5;
                    currentState = State.FLEE;
                }
                break;
            case State.CHASE:           
                target = player.transform;
                if (Vector3.Distance(player.transform.position, transform.position) >= 5.0f)
                {
                    totalTime = 0.0f;
                    currentState = State.IDLE;
                }
                else if (lightOn)
                {
                    rb.velocity = Vector3.zero;
                    speed *= 5;
                    currentState = State.FLEE;
                }
                break;
            case State.FLEE:
                target = scaryAlert;
                if (Vector3.Distance(player.transform.position, transform.position) >= 1.0f && !lightOn)
                {
                    speed /= 5;
                    totalTime = 0.0f;
                    currentState = State.IDLE;
                }
                break;
            default:
                break;
        }
    }
}
