using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

public class AbyssCrawler : MonoBehaviour
{
    [SerializeField]
    private Transform eyes;
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
        eyes.gameObject.SetActive(false);
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        currentState = State.PATROL;
        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
        target = objWaypoints[0].GetComponent<Transform>();
        scaryAlert = objWaypoints[objWaypoints.Count-1].GetComponent<Transform>();
        Physics2D.IgnoreLayerCollision(2, 8);
        Physics2D.IgnoreLayerCollision(2, 9);
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), mapCol.GetComponent<Collider2D>());
        rezTime = 0.0f;
        if (enemyData.GetSprite() == null)
        {
            enemyData.Init(100, 10, enemySprite.GetComponent<SpriteRenderer>().sprite, "Abyss Crawler", "CRAWLER");
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
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
                rezTime = 0.0f;
                enemyData.SetDead(false);
                isDead = false;
                enemyData.ResetHealth(50);
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
            eyes.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            enemySprite.localScale = new Vector3(1f, 1f, 1f);
            eyes.localScale = new Vector3(1f, 1f, 1f);
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
        if (hit.collider != null && !hit.collider.gameObject.CompareTag("Player"))
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
                scaryAlert = objWaypoints[targetIndex].GetComponent<Transform>();
                if (totalTime >= 1.0f)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0f;
                    targetIndex++;
                    targetIndex %= objWaypoints.Count;
                    currentState = State.PATROL;
                }
                else if (lightOn)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0f;
                    speed *= 5;
                    currentState = State.FLEE;
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
                else if (Vector3.Distance(player.transform.position, transform.position) <= 4.0f && hit.collider.gameObject.CompareTag("Player"))
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0f;
                    eyes.gameObject.SetActive(true);
                    currentState = State.CHASE;
                }
                else if (lightOn)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0f;
                    speed *= 5;
                    currentState = State.FLEE;
                }
                break;
            case State.CHASE:
                eyes.gameObject.SetActive(true);
                target = player.transform;
                if (Vector3.Distance(player.transform.position, transform.position) >= 5.0f)
                {
                    eyes.gameObject.SetActive(false);
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0f;
                    totalTime = 0.0f;
                    currentState = State.PATROL;
                }
                else if (lightOn)
                {
                    eyes.gameObject.SetActive(false);
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0f;
                    speed *= 5;
                    currentState = State.FLEE;
                }
                break;
            case State.FLEE:
                target = scaryAlert;
                if (Vector3.Distance(target.transform.position, transform.position) <= 0.5f && !lightOn)
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
