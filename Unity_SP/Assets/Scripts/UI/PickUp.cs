using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private AudioHandler sfxHandler;

    public ItemData item;

    public int dropIndex;

    public WorldState worldState;


    void Pickup()
    {
        InventoryManager.Instance.Add(item);
        worldState.pickedUp[dropIndex] = true;
        sfxHandler.playAudio("pickup");

        if (!worldState.altered)
        {
            worldState.altered = true;
        }
        Destroy(gameObject);
    }

    public void SetAudioHandler(AudioHandler newAudioHandler)
    {
        sfxHandler = newAudioHandler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Pickup();
        }
    }
}
