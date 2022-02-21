using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractScript : MonoBehaviour
{
    private Player _player;
    private PlayerControls _playerControls;
    private bool _canInteract = false, _isInteracting = false;

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
    }

    //Triggers when in range to interact with something.
    private void OnTriggerEnter(Collider other)
    {
        _canInteract = true;
    }

    //Makes sure the player doesn't interact with something out of range.
    private void OnTriggerExit(Collider other)
    {
        _canInteract = false;
    }
}
