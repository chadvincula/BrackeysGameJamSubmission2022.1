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
    private float _initialVolume = 1f;
    public bool isTryingToBeQuiet = false;

    private void Start()
    {
        _passiveSounds = GetComponent<AudioSource>();
        _initialVolume = _passiveSounds.volume;
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

        if(!isTryingToBeQuiet && _passiveSounds.volume < _initialVolume)
            _passiveSounds.volume += Time.deltaTime;
    }

    public IEnumerator GoQuiet(float timeTillQuiet)
    {
        isTryingToBeQuiet = true;
        while(_passiveSounds.volume > 0f)
        {
            _passiveSounds.volume -= Time.deltaTime / timeTillQuiet;
            yield return null;
        }
    }
}
