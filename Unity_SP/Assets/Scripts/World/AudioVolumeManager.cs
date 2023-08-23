using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolumeManager : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider masterSlider;
    public Slider sfxSlider;
    public Slider bgmSlider;
    [SerializeField]
    AudioHandler audioHandler;

    [SerializeField]
    private PlayerPrefs playerPrefs;

    

    // Start is called before the first frame update
    void Start()
    {
        playerPrefs.Save();
        playerPrefs.Load();
    }

    void Update()
    {
        masterSlider.value = playerPrefs.GetVolume("master");
        sfxSlider.value = playerPrefs.GetVolume("sfx");
        bgmSlider.value = playerPrefs.GetVolume("bgm");

        SetMasterVolume();
        SetSFXVolume();
        SetBGMVolume();

        playerPrefs.Save();
    }

    public void SetMasterVolume()
    {
        mixer.SetFloat("MasterVolume", masterSlider.value);
        playerPrefs.SetVolume("master", masterSlider.value);
        audioHandler.playAudio("inventory"); // to let the player hear when the change it
    }

    public void SetSFXVolume()
    {
        mixer.SetFloat("SFXVolume", sfxSlider.value);
        playerPrefs.SetVolume("sfx", sfxSlider.value);
        audioHandler.playAudio("inventory"); // to let the player hear when the change it
    }

    public void SetBGMVolume()
    {
        mixer.SetFloat("BGMVolume", bgmSlider.value);
        playerPrefs.SetVolume("bgm", bgmSlider.value);
        // change to bgm audioHandler.playAudio("inventory"); // to let the player hear when the change it
    }
}
