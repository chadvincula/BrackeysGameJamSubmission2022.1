using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractScript : MonoBehaviour
{
    private PlayerControls _playerControls;
    private bool _canInteract = false, _isInteracting = false;
    protected Player _player;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
        _playerControls.Player.Interact.performed += HandleInteract;
    }

    private void OnDisable()
    {
        _playerControls.Disable();
        _playerControls.Player.Interact.performed -= HandleInteract;
    }

    //Note: Unfinished, you need to set isInteracting to true so that you don't trigger it twice.
    private void HandleInteract(InputAction.CallbackContext context)
    {
        if(_canInteract) Debug.Log("You've reached a checkpoint!");
        PerformInteraction();
    }

    protected virtual void PerformInteraction()
    {
        Debug.Log("Performing Interaction...");
    }

    //Triggers when in range to interact with something.
    private void OnTriggerEnter(Collider other)
    {
        _canInteract = true;
        _player = other.gameObject.GetComponentInParent<Player>();
    }

    //Makes sure the player doesn't interact with something out of range.
    private void OnTriggerExit(Collider other)
    {
        _canInteract = false;
    }
}
