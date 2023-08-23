using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMHandler : MonoBehaviour
{
    [Header("Audio")]
    private AudioSource sfxAudioSrc;

    public List<AudioClip> lMusic;

    public int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        sfxAudioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!sfxAudioSrc.isPlaying)
        {
            sfxAudioSrc.clip = lMusic[index];
            sfxAudioSrc.Play();
        }
    }
}
