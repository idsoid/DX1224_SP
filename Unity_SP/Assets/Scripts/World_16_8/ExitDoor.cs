using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private GameObject closeSprite;
    [SerializeField] private GameObject openSprite;

    private bool closed;

    private void Start()
    {
        closed = true;
    }

    public void ToggleClose(bool x)
    {
        closed = x;
        if (x)
        {
            closeSprite.SetActive(true);
            openSprite.SetActive(false);
        }
        else
        {
            closeSprite.SetActive(false);
            openSprite.SetActive(true);
        }
    }
}
