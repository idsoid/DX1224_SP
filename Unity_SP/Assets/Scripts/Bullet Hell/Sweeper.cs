using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweeper : MonoBehaviour
{
    [SerializeField] private GameObject pivot;
    [SerializeField] private float initialDelay;
    [SerializeField] private float sweepSpeed;
    [SerializeField] private float lingerTime;

    private bool sweep;
    private bool sweeped;

    private void Start()
    {
        sweep = false;
        sweeped = false;
    }

    private void Update()
    {
        if (initialDelay > 0)
            initialDelay -= Time.deltaTime;
        else if (initialDelay < 0)
        {
            sweep = true;
            initialDelay = 0;
        }

        if (sweep)
            Sweep();

        if (sweeped)
            Linger();
    }

    private void Sweep()
    {
        if (pivot.transform.localRotation.eulerAngles.z < 70f)
        {
            pivot.transform.Rotate(sweepSpeed * new Vector3(0f, 0f, 1f) * Time.deltaTime);
        }
        else
            sweeped = true;
    }

    private void Linger()
    {
        if (lingerTime > 0)
            lingerTime -= Time.deltaTime;
        else if (lingerTime < 0)
        {
            lingerTime = 0;

            Destroy(gameObject);
        }
    }
}
