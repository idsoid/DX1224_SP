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
    private float iFrameTimer;

    //public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //healthBar.fillAmount = playerData.GetValue("health") / playerData.GetValue("maxHealth");
        HandleInputMovement();

        if(Input.GetMouseButtonDown(0))
        {
            playerData.AlterValue("health", -10);
        }

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
            Debug.Log("Ouch");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("eProj"))
        {
            TakeDamage(10);
        }
        if (collision.gameObject.CompareTag("eProjOrange") && rb.velocity == new Vector2(0f, 0f))
        {
            TakeDamage(10);
        }
        if (collision.gameObject.CompareTag("eProjBlue") && rb.velocity != new Vector2(0f, 0f))
        {
            TakeDamage(10);
        }
    }
}
