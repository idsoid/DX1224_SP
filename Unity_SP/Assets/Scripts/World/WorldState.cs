using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WorldState : ScriptableObject
{
    public List<bool> pickedUp;
    public bool altered = false;
}
