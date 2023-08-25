using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [Header("Audio")]
    private AudioSource sfxAudioSrc;

    [SerializeField] private AudioClip sprint;
    [SerializeField] private AudioClip inventory;
    [SerializeField] private AudioClip fireBurning;
    [SerializeField] private AudioClip fireExtinguish;
    [SerializeField] private AudioClip fireStoked;
    [SerializeField] private AudioClip equip;
    [SerializeField] private AudioClip flashLightOn;
    [SerializeField] private AudioClip flashLightOff;
    [SerializeField] private AudioClip unlockNoise;
    [SerializeField] private AudioClip exitDoorOpen;
    [SerializeField] private AudioClip playerHurt;
    [SerializeField] private AudioClip frost;
    [SerializeField] private AudioClip eat;
    [SerializeField] private AudioClip drink;
    [SerializeField] private AudioClip pickup;

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
            case "flashLightOn":
                sfxAudioSrc.PlayOneShot(flashLightOn);
                break;
            case "flashLightOff":
                sfxAudioSrc.PlayOneShot(flashLightOff);
                break;
            case "unlockNoise":
                sfxAudioSrc.PlayOneShot(unlockNoise);
                break;
            case "exitDoorOpen":
                sfxAudioSrc.PlayOneShot(exitDoorOpen);
                break;
            case "playerHurt":
                sfxAudioSrc.PlayOneShot(playerHurt);
                break;
            case "frost":
                if (!sfxAudioSrc.isPlaying)
                    sfxAudioSrc.PlayOneShot(frost);
                break;
            case "eat":
                sfxAudioSrc.PlayOneShot(eat);
                break;
            case "drink":
                sfxAudioSrc.PlayOneShot(drink);
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
