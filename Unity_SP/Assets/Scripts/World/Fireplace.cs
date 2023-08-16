using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireplace : MonoBehaviour
{
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject safeZone;
    [SerializeField] private Animator fireplaceAnim;

    private bool lit;
    public float burnTime;

    private void Start()
    {
        lit = true;
        burnTime = 180f;
    }

    private void Update()
    {
        if (lit)
        {
            if (burnTime > 0f)
            {
                burnTime -= 1f * Time.deltaTime;
            }
            else if (burnTime < 0f)
            {
                burnTime = 0f;
            }
            else if (burnTime == 0f)
            {
                ToggleLit(false);
            }
        }
    }

    private void ToggleLit(bool x)
    {
        lit = x;
        fire.SetActive(lit);
        safeZone?.SetActive(lit);
        if (!x)
        {
            fireplaceAnim.Play("fireplace_unlit");
        }
        else
        {
            fireplaceAnim.Play("fireplace_lit");
        }
    }

    public void AddFuel()
    {
        burnTime += 60;
        if (!lit)
        {
            ToggleLit(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

        }
    }
}
