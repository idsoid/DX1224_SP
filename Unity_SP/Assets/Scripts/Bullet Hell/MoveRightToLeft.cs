using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRightToLeft : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(-speed, 0f);
    }

    private void Update()
    {
        if (transform.position.x < -8f)
            Destroy(gameObject);
    }
}
