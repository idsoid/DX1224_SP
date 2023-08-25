using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [Header("Audio")]
    private AudioSource sfxAudioSrc;

    public AudioClip sprint;
    public AudioClip inventory;
    public AudioClip fireBurning;
    public AudioClip fireExtinguish;
    public AudioClip fireStoked;
    public AudioClip equip;
    public AudioClip playerAttack;
    public AudioClip flashLight;
    public AudioClip unlockNoise;
    public AudioClip exitDoorOpen;
    public AudioClip playerDamaged;
    public AudioClip frost;
    public AudioClip eat;
    public AudioClip heal;
    public AudioClip pickup;

    //enemystuff
    public AudioClip enemyHurt;

    // Start is called before the first frame update
    void Start()
    {
        sfxAudioSrc = GetComponent<AudioSource>();
    }

    public void playAudio(string name)
    {
        switch (name)
        {
            case "sprint":
                if(!sfxAudioSrc.isPlaying)
                    sfxAudioSrc.PlayOneShot(sprint);
                break;
            case "inventory":
                sfxAudioSrc.PlayOneShot(inventory);
                break;
            case "fireBurning":
                if (!sfxAudioSrc.isPlaying)
                    sfxAudioSrc.PlayOneShot(fireBurning);
                break;
            case "fireExtinguish":
                sfxAudioSrc.PlayOneShot(fireExtinguish);
                break;
            case "fireStoked":
                sfxAudioSrc.PlayOneShot(fireStoked);
                break;
            case "equip":
                sfxAudioSrc.PlayOneShot(equip);
                break;
            case "pickup":
                sfxAudioSrc.PlayOneShot(pickup);
                break;
            case "enemyHurt":
                sfxAudioSrc.PlayOneShot(enemyHurt);
                break;
            default:
                break;
        }


    }
}
