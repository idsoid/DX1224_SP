using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public CombatManager combatManager;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
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
        combatManager.UpdateData();
    }

    public void bruh2()
    {
        this.gameObject.SetActive(false);
    }

}
