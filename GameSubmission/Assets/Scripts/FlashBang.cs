using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBang : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip audioClip;

    private void Start()
    {
        _audioSource = FindObjectOfType<AudioSource>();
        _audioSource.Stop();
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }
}
