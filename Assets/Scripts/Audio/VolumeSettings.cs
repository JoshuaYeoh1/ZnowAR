using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer mixer;

    public Slider masterSlider, musicSlider, sfxSlider;

    public const string MIXER_MASTER="masterVolume", MIXER_MUSIC="musicVolume", MIXER_SFX="sfxVolume"; 

    void Awake()
    {
        masterSlider.onValueChanged.AddListener(setMasterVolume);

        musicSlider.onValueChanged.AddListener(setMusicVolume);

        sfxSlider.onValueChanged.AddListener(setSFXVolume);
    }

    void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat(Singleton.MASTER_KEY, 1f);

        musicSlider.value = PlayerPrefs.GetFloat(Singleton.MUSIC_KEY, 1f);

        sfxSlider.value = PlayerPrefs.GetFloat(Singleton.SFX_KEY, 1f);
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat(Singleton.MASTER_KEY, masterSlider.value);

        PlayerPrefs.SetFloat(Singleton.MUSIC_KEY, musicSlider.value);

        PlayerPrefs.SetFloat(Singleton.SFX_KEY, sfxSlider.value);
    }

    void setMasterVolume(float value)
    {
        mixer.SetFloat(MIXER_MASTER,Mathf.Log10(value)*20);
    }
    
    void setMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_MUSIC,Mathf.Log10(value)*20);
    }
    
    void setSFXVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX,Mathf.Log10(value)*20);
    }

}
