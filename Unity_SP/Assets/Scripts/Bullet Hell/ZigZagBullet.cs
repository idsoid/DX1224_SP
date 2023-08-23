using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagBullet : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D rb;

    private float velocityX;
    private float velocityY;
    private float originalPositionY;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        originalPositionY = transform.position.y;
        if (transform.position.y > 0)
        {
            velocityY = speed * 3;
        }
        else if (transform.position.y < 0)
        {
            velocityY = -speed * 3;
        }
    }

    private void Update()
    {
        if (transform.position.x > 6.5f)
        {
            if (velocityX != -speed)
                velocityX = -speed;
        }
        else if (transform.position.x < -6.5f)
        {
            if (velocityX != speed)
                velocityX = speed;
        }

        if (transform.position.y > originalPositionY + 1.5f)
        {
            velocityY = -speed * 2;
        }
        else if (transform.position.y < originalPositionY - 1.5f)
        {
            velocityY = speed * 2;
        }

        rb.velocity = new Vector2(velocityX, velocityY); 
    }
}
