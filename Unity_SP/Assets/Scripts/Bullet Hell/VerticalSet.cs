using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalSet : MonoBehaviour
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
        rb.velocity = new Vector2(0f, speed);
        time = timeToSet;
    }

    private void Update()
    {
        if (!set)
        {
            if (transform.position.y > 2.4f && rb.velocity != new Vector2(0f, -speed))
                rb.velocity = new Vector2(0f, -speed);
            else if (transform.position.y < -2.4f && rb.velocity != new Vector2(0f, speed))
                rb.velocity = new Vector2(0f, speed);
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

    public float GetVertical()
    {
        return transform.position.y;
    }
}
