using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongBar : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float horizontalSpeed;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        transform.Rotate(new Vector3(0f, 0f, 90f));
    }

    private void Update()
    {
        transform.Rotate(rotationSpeed * new Vector3(0f, 0f, 1f) * Time.deltaTime);

        if (transform.position.x > 8f && rb.velocity != new Vector2(-horizontalSpeed, 0f))
            rb.velocity = new Vector2(-horizontalSpeed, 0f);
        else if (transform.position.x < -8f && rb.velocity != new Vector2(horizontalSpeed, 0f))
            rb.velocity = new Vector2(horizontalSpeed, 0f);
    }
}
