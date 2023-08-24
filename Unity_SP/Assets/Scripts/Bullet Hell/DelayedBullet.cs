using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedBullet : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float timeToActivate;
    [SerializeField] private float delayAfterActivate;

    private Rigidbody2D rb;

    private bool activate;
    private Vector2 direction;
    private float delay;
    private bool shoot;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        activate = false;
        delay = delayAfterActivate;
        shoot = false;
    }

    private void Update()
    {
        if (timeToActivate > 0)
            timeToActivate -= Time.deltaTime;
        else if (timeToActivate < 0)
        {
            direction = (target.transform.position - transform.position).normalized;
            activate = true;
            timeToActivate = 0;
        }

        if (activate)
            Activated();
    }

    private void Activated()
    {
        if (delay > 0)
            delay -= Time.deltaTime;
        else if (delay < 0)
        {
            transform.parent = null;
            shoot = true;
            delay = 0;
        }

        if (shoot)
        {
            rb.velocity = Vector3.zero;
            rb.velocity = direction * 20f;
        }
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    public void SetTimeToActivate(float newTimeToActivate)
    {
        timeToActivate = newTimeToActivate;
    }
}
