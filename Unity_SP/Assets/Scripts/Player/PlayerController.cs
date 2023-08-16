using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField]
    private PlayerData playerData;


    [SerializeField]
    private bool isNearCampfire = false;

    [SerializeField]
    private float mvmt = 2f;

    float hInput, vInput;
    string idleAnim;

    bool canRun,isRunning;

    bool isCold;
    bool isHungry;

    void Start()
    {
        isHungry = false;
        isCold = false;
        canRun = true;
        isRunning = false;
        hInput = vInput = 0f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        playerData.Init();
    }


    // Update for input
    void Update()
    {
        PlayerRun();
        HandleInputMovement();
        HandleTemperatureChange();
        HandleHungerChange();
    }

    //fixed update for data processing
    void FixedUpdate()
    {
        PrintSelf();
    }

    void LateUpdate()
    {
        HandleMovementAnimation();
    }
    private void HandleInputMovement()
    {
        hInput = Input.GetAxis("Horizontal") * mvmt;
        vInput = Input.GetAxis("Vertical") * mvmt;


        if (canRun)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                hInput *= 1.5f;
                vInput *= 1.5f;
                anim.speed = 0.8f;
                isRunning = true;
            }
            else
            {
                isRunning = false;
                anim.speed = 0.5f;
            }
        }

        rb.velocity = new Vector2 (hInput, vInput);
    }

    private void HandleTemperatureChange()
    {
        if (isNearCampfire && playerData.GetValue("temperature") != 100)
        {
            playerData.AlterValue("temperature", Time.deltaTime * playerData.GetValue("tempDecreaseMultiplier"));
            isCold = false;
        }
        else if (!isNearCampfire)
        {
            if (!isCold)
            {
                playerData.AlterValue("temperature", -Time.deltaTime * playerData.GetValue("tempDecreaseMultiplier"));
                if (playerData.GetValue("temperature") == 0)
                {
                    isCold = true;
                }
            }
        }
        if (isCold)
        {
            playerData.AlterValue("health", -Time.deltaTime * 10); //to replace 10
        }
    }

    private void HandleHungerChange()
    {
        playerData.AlterValue("hunger", -Time.deltaTime * playerData.GetValue("hungerDecreaseMultiplier"));
        if(playerData.GetValue("hunger") <= 0)
        {
            isHungry = true;
        }
        else
        {
            isHungry = false;
        }

        if(isHungry)
        {
            playerData.AlterValue("health", -Time.deltaTime * 10); //to replace 10
        }
    }

    private void PlayerRun()
    {
        if (canRun)
        {
            if (isRunning)
            {
                playerData.AlterValue("stamina", -Time.deltaTime * 20f);
                if (playerData.GetValue("stamina") <= 0)
                {
                    playerData.SetValue("stamina", 0);
                    anim.speed = 0.5f;
                    canRun = false;
                    isRunning = false;
                }
            }
            else
            {
                playerData.AlterValue("stamina", +Time.deltaTime * 10f);
                if (playerData.GetValue("stamina") >= 80)
                {
                    playerData.SetValue("stamina",80);
                }
            }
        }
        else
        {
            playerData.AlterValue("stamina", +Time.deltaTime * 10f);
            if (playerData.GetValue("stamina") >= 80)
            {
                playerData.SetValue("stamina", 80);
                canRun = true;
                isRunning = false;
            }
        }

    }

    private void HandleMovementAnimation()
    {
        if(hInput > 0)
        {
            anim.Play("walk_e");
            idleAnim = "idle_e";
        }
        else if (hInput < 0)
        {
            anim.Play("walk_w");
            idleAnim = "idle_w";
        }

        else if (vInput > 0)
        {
            anim.Play("walk_n");
            idleAnim = "idle_n";
        }
        else if (vInput < 0)
        {
            anim.Play("walk_s");
            idleAnim = "idle_s";
        }
        else
        {
            anim.Play(idleAnim);
        }
    }

    private void PrintSelf()
    {
        Debug.Log("hInput: " + hInput);
        Debug.Log("vInput: " + vInput);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("SafeZone"))
        {
            isNearCampfire = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SafeZone"))
        {
            isNearCampfire = false;
        }
    }
}
