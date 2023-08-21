using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

public class AbyssImitater : MonoBehaviour
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
    [SerializeField]
    private Transform mimicSprite;
    [SerializeField]
    private List<Sprite> itemSprites;
    private Animator ar;
    private int decider;
    private int targetIndex;
    private float totalTime;
    private float speed = 200f;
    private float nextWaypointDistance = 1f;

    private bool shook;
    private float attackTime;
    public bool lightOn = false;

    Path path;
    private int currentWaypoint = 0;
    private Transform target;

    Seeker seeker;
    Rigidbody2D rb;

    private State currentState;
    public enum State
    {
        MIMIC,
        TARGET,
        FREEZE,
        CHASE
    }
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
        ar = mimicSprite.GetComponent<Animator>();
        shook = false;
        mimicSprite.GetComponent<SpriteRenderer>().sprite = itemSprites[Random.Range(0, 4)];
        enemySprite.gameObject.SetActive(false);
        if (enemyData.GetDead())
        {
            gameObject.SetActive(false);
        }
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        currentState = State.MIMIC;
        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
        target = player.transform;
        enemyData.Init(40, 15, enemySprite.GetComponent<Sprite>(), gameObject.name);
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

        if (currentState == State.CHASE)
        {
            rb.AddForce(force);
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
        if (!lightOn && shook)
        {
            shook = false;
        }
        switch (currentState)
        {
            case State.MIMIC:
                if (lightOn && !shook)
                {
                    ar.SetTrigger("lightOn");
                    shook = true;
                }

                if (lightOn && shook)
                {
                    attackTime = 5.0f;
                    currentState = State.FREEZE;
                }
                else if (Vector3.Distance(player.transform.position, transform.position) <= 2.0f)
                {
                    mimicSprite.gameObject.SetActive(false);
                    enemySprite.gameObject.SetActive(true);
                    currentState = State.TARGET;
                }
                break;
            case State.TARGET:           
                target = player.transform;
                if (Vector3.Distance(player.transform.position, transform.position) >= 5.0f)
                {
                    mimicSprite.gameObject.SetActive(true);
                    enemySprite.gameObject.SetActive(false);
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0f;
                    currentState = State.MIMIC;
                }
                break;
            case State.FREEZE:
                if (lightOn)
                {
                    attackTime -= Time.deltaTime;
                    if (attackTime <= 0.0f && lightOn)
                    {
                        speed *= 10;
                        lightOn = false;
                        mimicSprite.gameObject.SetActive(false);
                        enemySprite.gameObject.SetActive(true);
                        currentState = State.CHASE;
                    }
                }
                else if (Vector3.Distance(player.transform.position, transform.position) <= 2.0f)
                {
                    mimicSprite.gameObject.SetActive(false);
                    enemySprite.gameObject.SetActive(true);
                    currentState = State.TARGET;
                }
                else
                {
                    totalTime = 0.0f;
                    currentState = State.MIMIC;
                }
                break;
            case State.CHASE:
                target = player.transform;
                break;
            default:
                break;
        }
    }
}
