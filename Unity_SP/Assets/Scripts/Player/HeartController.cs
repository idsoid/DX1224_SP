using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeartController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private Rigidbody2D rb;

    private float hInput, vInput;
    private float mvmt = 5f;
    public float iFrameTimer;
    private bool takeDamage;

    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        takeDamage = false;
    }

    private void Update()
    {
        HandleInputMovement();

        if (takeDamage)
            TakeDamage(10);

        if (iFrameTimer > 0)
            iFrameTimer -= Time.deltaTime;
        else if (iFrameTimer < 0)
            iFrameTimer = 0;
    }

    private void HandleInputMovement()
    {
        hInput = Input.GetAxis("Horizontal") * mvmt;
        vInput = Input.GetAxis("Vertical") * mvmt;

        rb.velocity = new Vector2(hInput, vInput);
    }

    private void TakeDamage(int damageTaken)
    {
        if (iFrameTimer == 0)
        {
            playerData.AlterValue("health", -damageTaken);
            iFrameTimer = 0.5f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("eProj") ||
            collision.gameObject.CompareTag("eProjOrange") && rb.velocity == new Vector2(0f, 0f) ||
            collision.gameObject.CompareTag("eProjBlue") && rb.velocity != new Vector2(0f, 0f))
        {
            takeDamage = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("eProj") ||
            collision.gameObject.CompareTag("eProjOrange") && rb.velocity == new Vector2(0f, 0f) ||
            collision.gameObject.CompareTag("eProjBlue") && rb.velocity != new Vector2(0f, 0f))
        {
            takeDamage = false;
        }
    }
}
