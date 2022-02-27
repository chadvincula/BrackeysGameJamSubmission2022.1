using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider masterVolume, bgmVolume, sfxVolume;
    public AudioMixer audioMixer;

    private void Start()
    {
        masterVolume.value = PlayerPrefs.GetFloat("masterVol", 0.75f);
        bgmVolume.value = PlayerPrefs.GetFloat("bgmVol", 0.75f);
        sfxVolume.value = PlayerPrefs.GetFloat("sfxVol", 0.75f);
    }

    public void SetMaster(float sliderValue)
    {
        audioMixer.SetFloat("masterVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("masterVol", sliderValue);
    }
    
    public void SetBGM(float sliderValue)
    {
        audioMixer.SetFloat("bgmVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("bgmVol", sliderValue);
    }
    
    public void SetSFX(float sliderValue)
    {
        audioMixer.SetFloat("sfxVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("sfxVol", sliderValue);
    }
}
