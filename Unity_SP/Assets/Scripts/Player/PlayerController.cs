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

    float hInput, vInput;
    string idleAnim;
    void Start()
    {
        hInput = vInput = 0f;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.speed = 0.5f;
        playerData.Init();
    }


    // Update for input
    void Update()
    {
        HandleInputMovement();
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
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        if(Input.GetKey(KeyCode.LeftShift))
        {
            hInput *= 1.2f;
            vInput *= 1.2f;
            anim.speed = 0.8f;
            playerData.AlterValue("stamina",1);
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

}
