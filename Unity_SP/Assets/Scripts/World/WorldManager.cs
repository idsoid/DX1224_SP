using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [SerializeField]
    private WorldState worldState;

    [SerializeField]
    private GameObject drops;

    [SerializeField]
    private GameObject locks;

    // Start is called before the first frame update
    void Start()
    {
        if(worldState.altered)
        {
            RemovePickedUpObjects();
        }
        else
        {
            Init();
        }
    }

    private void RemovePickedUpObjects()
    {
        for (int i = 0; i < worldState.pickedUp.Count; i++)
        {
            if (worldState.pickedUp[i])
            {
                Destroy(drops.GetComponentsInChildren<PickUp>()[i].gameObject);
            }
        }
        for(int i = 0; i < worldState.locksOpened.Count; i++)
        {
            if(worldState.locksOpened[i])
            {
                Destroy(locks.GetComponentsInChildren<LockedDoor>()[i].gameObject);
            }
        }
    }

    private void Init()
    {
        for (int i = 0; i < worldState.pickedUp.Count; i++)
        {
            worldState.pickedUp[i] = false;
        }
        for(int i = 0; i < worldState.locksOpened.Count; i++)
        {
            worldState.locksOpened[i] = false;
        }
    }
    // Update is called once per frame

}
