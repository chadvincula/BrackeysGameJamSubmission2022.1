using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarpetFluid : InteractScript
{
    public float sanityIncrease = 0.1f;

    public SanityContoller sanityContoller;

    protected override void HandleInteract(InputAction.CallbackContext context)
    {
        if(base._canInteract) sanityContoller.SetSanity(sanityIncrease);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        base.interactableIcon.SetActive(true);
    }

    protected override void OnTriggerExit(Collider other)
    {
        _canInteract = false;
        _isInteracting = false;
        base.interactableIcon.SetActive(false);
    }
}
