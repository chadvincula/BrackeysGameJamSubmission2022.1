using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractScript : MonoBehaviour
{
    private Player _player;
    private PlayerControls _playerControls;
    protected bool _canInteract = false, _isInteracting = false;

    public GameObject textBox;

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
    protected virtual void HandleInteract(InputAction.CallbackContext context)
    {
        if (!_canInteract) return;
        textBox.SetActive(true);
        _isInteracting = true;
    }

    //Triggers when in range to interact with something.
    protected virtual void OnTriggerEnter(Collider other)
    {
        _canInteract = true;
    }

    //Makes sure the player doesn't interact with something out of range.
    protected virtual void OnTriggerExit(Collider other)
    {
        _canInteract = false;
        _isInteracting = false;
        textBox.SetActive(false);
    }
}
