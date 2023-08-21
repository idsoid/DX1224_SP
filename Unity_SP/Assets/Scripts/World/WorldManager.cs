using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [SerializeField]
    private WorldState worldState;

    [SerializeField]
    private List<GameObject> drops;
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
                Destroy(drops[i]);
            }
        }
    }

    private void Init()
    {
        for (int i = 0; i < worldState.pickedUp.Count; i++)
        {
            worldState.pickedUp[i] = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
