using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioHandler sfxHandler;

    private bool cheats = false;

    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField]
    private PlayerData playerData;

    [SerializeField]
    private GameObject flashlight;

    [SerializeField]
    private GameObject candle;

    public bool lightOn;
    public float candleBurnTime;

    float hInput, vInput;
    string idleAnim;

    bool canRun, isRunning;

    bool isHungry;

    float mvmt;

    [SerializeField]
    float speedmult = 1f;

    private enum HAND
    {
        FLASHLIGHT,
        CANDLE
    }

    private HAND hand;

    [SerializeField]
    AudioHandler audioHandler;

    private void Awake()
    {
        if (playerData.ReadyLoad)
        {
            playerData.Load();

        }
        else
        {
            playerData.Init();
        }
    }
    void OnEnable()
    {
        hand = HAND.FLASHLIGHT;
        candleBurnTime = 0f;
        lightOn = true;
        isHungry = false;
        canRun = true;
        isRunning = false;
        hInput = vInput = 0f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        

        
    }

    private void Start()
    {
        
        if (playerData.loadOldPos)
        {
            transform.position = playerData.temppos;
            playerData.SetValue("health", playerData.temphp);
            playerData.SetValue("hunger", playerData.temphunger);
            playerData.loadOldPos = false;
            InventoryManager.Instance.LoadInventory();
        }
        else if (!playerData.ReadyLoad)
        {
            InventoryManager.Instance.Items.Clear();
        }
        else
        {
            InventoryManager.Instance.LoadInventory();
            playerData.ReadyLoad = false;
        }
        playerData.canMove = true;
    }

    // Update for input
    void Update()
    {
        HandleCheatCode();
        PlayerRun();
        HandleInputMovement();
        HandleTemperatureChange();
        HandleHungerChange();
        HandleHandSwapping();
        HandleFlashlightToggle();
        
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

    #region
    private void HandleInputMovement()
    {
        //if (playerData.canMove)
        //{
            mvmt = playerData.GetValue("speed") + (playerData.GetValue("temperature") * 0.01f);
            hInput = Input.GetAxis("Horizontal") * mvmt * speedmult;
            vInput = Input.GetAxis("Vertical") * mvmt * speedmult;


            if (canRun)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    hInput *= 1.5f;
                    vInput *= 1.5f;
                    anim.speed = 0.8f;
                    isRunning = true;
                    audioHandler.playAudio("sprint");
                }
                else
                {
                    isRunning = false;
                    anim.speed = 0.5f;

                }
            }

            rb.velocity = new Vector2(hInput, vInput);
        //}
    }

    private void HandleTemperatureChange()
    {
        if (playerData.safe && playerData.GetValue("temperature") != playerData.GetValue("maxTemperature"))
        {
            playerData.AlterValue("temperature", Time.deltaTime * playerData.GetValue("tempDecreaseMultiplier") * 5);
            playerData.isCold = false;
        }
        else if (!playerData.safe)
        {
            //Decrease temperature until cold
            if (!playerData.isCold)
            {
                float increment = -Time.deltaTime * playerData.GetValue("tempDecreaseMultiplier");

                //if candle is being used, decrease rate of temp
                if (hand == HAND.CANDLE && lightOn && candleBurnTime > 0f)
                {
                    candleBurnTime -= Time.deltaTime;
                    if(candleBurnTime <= 0f)
                    {
                        candle.SetActive(false);
                    }
                    increment *= 0.4f;
                }

                playerData.AlterValue("temperature", increment);
                if (playerData.GetValue("temperature") == 0)
                {
                    playerData.isCold = true;
                }
            }
        }
        if (playerData.isCold)
        {
            playerData.AlterValue("health", -Time.deltaTime * 5); 
        }
    }

    private void HandleHungerChange()
    {
        playerData.AlterValue("hunger", -Time.deltaTime * playerData.GetValue("hungerDecreaseMultiplier"));
        if (playerData.GetValue("hunger") <= 0)
        {
            isHungry = true;
        }
        else
        {
            isHungry = false;
        }

        if (isHungry)
        {
            playerData.AlterValue("health", -Time.deltaTime * 1);
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
                if (playerData.GetValue("stamina") >= playerData.GetValue("maxStamina"))
                {
                    playerData.SetValue("stamina", 80);
                }
            }
        }
        else
        {
            playerData.AlterValue("stamina", +Time.deltaTime * 10f);
            if (playerData.GetValue("stamina") >= playerData.GetValue("maxStamina"))
            {
                playerData.SetValue("stamina", 80);
                canRun = true;
                isRunning = false;
            }
        }

    }

    private void HandleMovementAnimation()
    {
        if (vInput > 0)
        {
            anim.Play("walk_n");
            idleAnim = "idle_n";
        }
        else if (vInput < 0)
        {
            anim.Play("walk_s");
            idleAnim = "idle_s";
        }
        else if(hInput > 0)
        {
            anim.Play("walk_e");
            idleAnim = "idle_e";
        }
        else if (hInput < 0)
        {
            anim.Play("walk_w");
            idleAnim = "idle_w";
        }
        else
        {
            anim.Play(idleAnim);
        }
    }

    private void PrintSelf()
    {
        //Debug.Log("hInput: " + hInput);
        //Debug.Log("vInput: " + vInput);
    }

    private void HandleFlashlightToggle()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            lightOn = !lightOn;
            switch (hand)
            {
                case HAND.FLASHLIGHT:
                    flashlight.SetActive(lightOn);
                    if (lightOn)
                        sfxHandler.playAudio("flashLightOn");
                    else
                        sfxHandler.playAudio("flashLightOff");
                    break;
                case HAND.CANDLE:
                    candle.SetActive(lightOn);
                    break;
            }
        }
    }

    private void HandleHandSwapping()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            switch(hand)
            {
                case HAND.FLASHLIGHT:
                    hand = HAND.CANDLE;
                    flashlight.SetActive(false);
                    if (lightOn && candleBurnTime > 0f)
                    {
                        
                        candle.SetActive(true);
                    }
                    break;
                case HAND.CANDLE:
                    hand = HAND.FLASHLIGHT;
                    if (lightOn)
                    {
                        flashlight.SetActive(true);
                        candle.SetActive(false);
                    }
                    break;
            }
        }
    }

    private void HandleCheatCode()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            cheats = !cheats;
            if(!cheats)
            {
                speedmult = 1f;
            }
        }
        if(cheats)
        {
            speedmult = 5f;
            playerData.SetValue("health", 100);
        }

    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Fireplace"))
        {
            if(hand == HAND.CANDLE && candleBurnTime > 0f)
            {
                candle.SetActive(true);
            }
            candleBurnTime = 180f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SafeZone"))
        {
            playerData.safe = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("SafeZone"))
        {
            playerData.safe = false;
        }
    }

    
}
