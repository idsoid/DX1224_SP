using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;
public class AbyssKeeper : MonoBehaviour
{
    [SerializeField]
    private Transform wanderEyes;
    [SerializeField]
    private Transform rushEyes;
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
    private Transform wanderSprite;
    [SerializeField]
    private Transform rushSprite;
    private int targetIndex;
    private float totalTime;
    private float speed = 200f;
    private float nextWaypointDistance = 1f;
    private float rezTime;

    public bool lightOn = false;
    public bool isDead = false;
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
        wanderEyes.gameObject.SetActive(false);
        rushEyes.gameObject.SetActive(false);
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        currentState = State.PATROL;
        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
        target = objWaypoints[0].GetComponent<Transform>();
        attackTime = 0.0f;
        rushSprite.gameObject.SetActive(false);
        Physics2D.IgnoreLayerCollision(2, 8);
        Physics2D.IgnoreLayerCollision(2, 9);
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), mapCol.GetComponent<Collider2D>());
        rezTime = 0.0f;
        if (enemyData.GetSprite() == null)
        {
            enemyData.Init(100, 10, wanderSprite.GetComponent<SpriteRenderer>().sprite, "Abyss Keeper", "KEEPER");
        }
        if (enemyData.GetDead())
        {
            GetComponent<Collider2D>().enabled = false;
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

        if (lightOn && currentState != State.RUSH)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            if (enemyData.GetDead() && rezTime <= 0.0f)
            {
                rezTime = 30.0f;
                GetComponent<Collider2D>().enabled = false;
                mapCol.SetActive(false);
                wanderSprite.gameObject.SetActive(false);
            }
            else if (!enemyData.GetDead())
            {
                rb.AddForce(force);
                GetComponent<Collider2D>().enabled = true;
                mapCol.SetActive(true);
                if (currentState != State.RUSH)
                {
                    wanderSprite.gameObject.SetActive(true);
                }
            }

            if (rezTime > 0.0f)
            {
                rezTime -= Time.deltaTime;
                if (rezTime <= 0.0f)
                {
                    rezTime = 0.0f;
                    enemyData.SetDead(false);
                    isDead = false;
                    enemyData.ResetHealth(100);
                }
            }
        }

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        if (rb.velocity.x >= 0.01f)
        {
            wanderSprite.localScale = new Vector3(-1f, 1f, 1f);
            wanderEyes.localScale = new Vector3(-1f, 1f, 1f);
            rushEyes.localScale = new Vector3(-1f, 1f, 1f);
            rushSprite.eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            wanderSprite.localScale = new Vector3(1f, 1f, 1f);
            wanderEyes.localScale = new Vector3(1f, 1f, 1f);
            rushEyes.localScale = new Vector3(1f, 1f, 1f);
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
                else if (lightOn)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0f;
                    attackTime = 1.25f;
                    currentState = State.FREEZE;
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
                else if (lightOn)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0f;
                    attackTime = 1.25f;
                    currentState = State.FREEZE;
                }
                break;
            case State.FREEZE:
                if (lightOn)
                {
                    wanderEyes.gameObject.SetActive(true);
                    attackTime -= Time.deltaTime;
                    if (attackTime <= 0.0f && lightOn)
                    {
                        wanderEyes.gameObject.SetActive(false);
                        rushEyes.gameObject.SetActive(true);
                        rb.velocity = Vector3.zero;
                        rb.angularVelocity = 0f;
                        speed *= 10;
                        lightOn = false;
                        wanderSprite.gameObject.SetActive(false);
                        rushSprite.gameObject.SetActive(true);
                        currentState = State.RUSH;
                    }
                }
                else
                {
                    wanderEyes.gameObject.SetActive(false);
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0f;
                    totalTime = 0.0f;
                    currentState = State.IDLE;
                }
                break;
            case State.RUSH:
                target = player.transform;
                if (Vector3.Distance(player.transform.position, transform.position) >= 10.0f)
                {
                    rushEyes.gameObject.SetActive(false);
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0f;
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
