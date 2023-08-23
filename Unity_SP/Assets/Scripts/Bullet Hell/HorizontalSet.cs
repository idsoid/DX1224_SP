using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalSet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float timeToSet;

    private Rigidbody2D rb;

    private bool set;
    private float time;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        set = false;
        rb.velocity = new Vector2(speed, 0f);
        time = timeToSet;
    }

    private void Update()
    {
        if (!set)
        {
            if (transform.position.x > 5.2f && rb.velocity != new Vector2(-speed, 0f))
                rb.velocity = new Vector2(-speed, 0f);
            else if (transform.position.x < -5.2f && rb.velocity != new Vector2(speed, 0f))
                rb.velocity = new Vector2(speed, 0f);
        }

        if (time > 0)
            time -= Time.deltaTime;
        else if (time < 0)
        {
            rb.velocity = Vector2.zero;

            set = true;
            time = 0;
        }
    }

    public bool GetSet()
    {
        return set;
    }

    public float GetHorizontal()
    {
        return transform.position.x;
    }
}
