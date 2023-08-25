using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    private PickUp[] listpickup;

    [SerializeField]
    private WorldState worldState;

    [SerializeField] private AudioHandler sfxHandler;

    private void Start()
    {
        listpickup = GetComponentsInChildren<PickUp>();


        for (int i = 0; i < listpickup.Length; i++)
        {
            listpickup[i].dropIndex = i;
            listpickup[i].worldState = worldState;
            listpickup[i].SetAudioHandler(sfxHandler);
        }
    }


}
