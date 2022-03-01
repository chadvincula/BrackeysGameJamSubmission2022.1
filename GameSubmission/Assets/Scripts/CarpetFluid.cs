using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarpetFluid : InteractScript
{
    public float sanityIncrease = 0.1f;

    public SanityContoller sanityContoller;

    private AudioSource _soundEffect;

    private void Start()
    {
        _soundEffect = GetComponent<AudioSource>();
    }

    protected override void HandleInteract(InputAction.CallbackContext context)
    {
        if (base._canInteract)
        {
            sanityContoller.SetSanity(sanityIncrease);
            _soundEffect.Play();
        }
    }
}
