using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.SceneManagement;

public class AbyssImitater : MonoBehaviour
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
    private Transform enemySprite;
    [SerializeField]
    private Transform mimicSprite;
    [SerializeField]
    private List<Sprite> itemSprites;
    private Animator ar;
    private int decider;
    private int targetIndex;
    private float speed = 200f;
    private float nextWaypointDistance = 1f;

    private bool shook;
    private float attackTime;
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
        MIMIC,
        TARGET,
        FREEZE,
        CHASE
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
        eyes.gameObject.SetActive(false);
        ar = mimicSprite.GetComponent<Animator>();
        shook = false;
        mimicSprite.GetComponent<SpriteRenderer>().sprite = itemSprites[Random.Range(0, 3)];
        enemySprite.gameObject.SetActive(false);
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        currentState = State.MIMIC;
        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
        target = player.transform;
        Physics2D.IgnoreLayerCollision(2, 8);
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), mapCol.GetComponent<Collider2D>());
        rezTime = 0.0f;
        if (enemyData.GetSprite() == null)
        {
            enemyData.Init(40, 15, enemySprite.GetComponent<SpriteRenderer>().sprite, "Abyss Imitater", "IMITATER");
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
            mimicSprite.gameObject.SetActive(false);
            enemySprite.gameObject.SetActive(false);
        }
        else if (!enemyData.GetDead())
        {
            if (currentState == State.CHASE || currentState == State.TARGET)
            {
                rb.AddForce(force);
                mimicSprite.gameObject.SetActive(false);
                enemySprite.gameObject.SetActive(true);
            }
            else
            {
                mimicSprite.gameObject.SetActive(true);
                enemySprite.gameObject.SetActive(false);
            }
            GetComponent<Collider2D>().enabled = true;
        }

        if (rezTime > 0.0f)
        {
            rezTime -= Time.deltaTime;
            if (rezTime <= 0.0f)
            {
                rezTime = 0.0f;
                enemyData.SetDead(false);
                isDead = false;
                enemyData.ResetHealth(40);
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
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0f;
                    attackTime = 5.0f;
                    currentState = State.FREEZE;
                }
                else if (Vector3.Distance(player.transform.position, transform.position) <= 2.0f)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0f;
                    eyes.gameObject.SetActive(true);
                    mimicSprite.gameObject.SetActive(false);
                    enemySprite.gameObject.SetActive(true);
                    currentState = State.TARGET;
                }
                break;
            case State.TARGET:           
                target = player.transform;
                if (Vector3.Distance(player.transform.position, transform.position) >= 5.0f)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0f;
                    eyes.gameObject.SetActive(false);
                    mimicSprite.gameObject.SetActive(true);
                    enemySprite.gameObject.SetActive(false);
                    currentState = State.MIMIC;
                }
                break;
            case State.FREEZE:
                if (lightOn)
                {
                    attackTime -= Time.deltaTime;
                    if (attackTime <= 0.0f && lightOn)
                    {
                        rb.velocity = Vector3.zero;
                        rb.angularVelocity = 0f;
                        speed *= 10;
                        lightOn = false;
                        eyes.gameObject.SetActive(true);
                        mimicSprite.gameObject.SetActive(false);
                        enemySprite.gameObject.SetActive(true);
                        currentState = State.CHASE;
                    }
                }
                else if (Vector3.Distance(player.transform.position, transform.position) <= 2.0f)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0f;
                    eyes.gameObject.SetActive(true);
                    mimicSprite.gameObject.SetActive(false);
                    enemySprite.gameObject.SetActive(true);
                    currentState = State.TARGET;
                }
                else
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0f;
                    currentState = State.MIMIC;
                }
                break;
            case State.CHASE:
                target = player.transform;
                if (enemyData.GetDead())
                {
                    eyes.gameObject.SetActive(false);
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = 0f;
                    speed /= 10;
                    currentState = State.MIMIC;
                }
                break;
            default:
                break;
        }
    }
}
