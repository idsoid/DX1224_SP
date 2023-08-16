using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class AbyssKeeper : MonoBehaviour
{

    [SerializeField]
    private EnemyData enemyData;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private List<GameObject> objWaypoints = new();
    [SerializeField]
    private Transform wanderSprite;
    [SerializeField]
    private Transform rushSprite;
    private int targetIndex;
    private float totalTime;
    private float speed = 200f;
    private float nextWaypointDistance = 1f;

    public bool lightOn = false;
    private float health;
    private float attack;
    private float attackTime;

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
        FREEZE,
        RUSH
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        currentState = State.PATROL;
        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
        target = objWaypoints[0].GetComponent<Transform>();
        attackTime = 0.0f;
        rushSprite.gameObject.SetActive(false);
        enemyData.Init(75, 30, wanderSprite.GetComponent<Sprite>(), gameObject.name);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemyData.GetDead())
        {
            gameObject.SetActive(false);
        }

        FSM();
        
        if (path == null || currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }
        Vector2 force = speed * Time.deltaTime * ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        if (lightOn)
        {
            rb.velocity = Vector3.zero;
        }
        else
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
            wanderSprite.localScale = new Vector3(-1f, 1f, 1f);
            rushSprite.eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            wanderSprite.localScale = new Vector3(1f, 1f, 1f);
            rushSprite.eulerAngles = new Vector3(0.0f, 0.0f, -90.0f);
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
        //RaycastHit2D hit;
        //if (true)
        //{
        //    hit = Physics2D.Raycast(transform.position, -Vector2.left);
        //}
        switch (currentState)
        {
            case State.IDLE:
                totalTime += Time.deltaTime;
                if (totalTime >= 1.0f)
                {
                    targetIndex++;
                    targetIndex %= objWaypoints.Count;
                    currentState = State.PATROL;
                }
                else if (lightOn)
                {
                    attackTime = 2.0f;
                    currentState = State.FREEZE;
                }
                break;
            case State.PATROL:
                target = objWaypoints[targetIndex].GetComponent<Transform>();
                if (Vector3.Distance(target.position, transform.position) <= 0.5f)
                {
                    totalTime = 0.0f;
                    currentState = State.IDLE;
                }
                else if (lightOn)
                {
                    attackTime = 2.0f;
                    currentState = State.FREEZE;
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
                        wanderSprite.gameObject.SetActive(false);
                        rushSprite.gameObject.SetActive(true);
                        currentState = State.RUSH;
                    }
                }
                else
                {
                    totalTime = 0.0f;
                    currentState = State.IDLE;
                }
                break;
            case State.RUSH:
                target = player.transform;
                if (Vector3.Distance(player.transform.position, transform.position) >= 10.0f)
                {
                    totalTime = 0.0f;
                    speed /= 10;
                    wanderSprite.gameObject.SetActive(true);
                    rushSprite.gameObject.SetActive(false);
                    currentState = State.IDLE;
                }
                break;
            default:
                break;
        }
    }
}
