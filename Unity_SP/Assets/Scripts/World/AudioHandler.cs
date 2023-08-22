using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    [Header("Audio")]
    private AudioSource sfxAudioSrc;
    public AudioClip walkAudio;
    public AudioClip invenAudio;

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
                if (!sfxAudioSrc.isPlaying)
                {
                    sfxAudioSrc.clip = walkAudio;
                    sfxAudioSrc.Play();
                }
                break;
            case "inventory":
                if (!sfxAudioSrc.isPlaying)
                {
                    sfxAudioSrc.clip = invenAudio;
                    sfxAudioSrc.Play();
                }
                break;
            default:
                break;
        }


    }
}
