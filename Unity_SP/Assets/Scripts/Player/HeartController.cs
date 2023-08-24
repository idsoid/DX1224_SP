using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeartController : MonoBehaviour
{

    private float hInput, vInput;
    private float mvmt = 5f;

    private Rigidbody2D rb;

    [SerializeField]
    private PlayerData playerData;

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
    }

    private void HandleInputMovement()
    {
        hInput = Input.GetAxis("Horizontal") * mvmt;
        vInput = Input.GetAxis("Vertical") * mvmt;

        rb.velocity = new Vector2(hInput, vInput);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("eProj"))
        {
            playerData.AlterValue("health", -10);
        }
    }
}
