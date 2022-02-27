using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EntitySounds : MonoBehaviour
{
    private AudioSource _passiveSounds;
    private float _timer = 0;
    private float _secondsTillPlay = 5;

    private void Start()
    {
        _passiveSounds = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _secondsTillPlay)
        {
            _timer = 0;
            _secondsTillPlay = Random.Range(4, 6);
            _passiveSounds.Play();
        }
    }
}
