using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private CombatManager combatManager;

    private Animator anim;

    [SerializeField]
    private AudioHandler audioHandler;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        anim.Play("Slash1");
    }

    private void OnDisable()
    {
        this.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void bruh()
    {
        combatManager.DamageEnemy();
        audioHandler.playAudio("enemyHurt");
    }

    public void bruh2()
    {
        this.gameObject.SetActive(false);
    }

}
