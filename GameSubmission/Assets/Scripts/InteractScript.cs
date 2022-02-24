using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractScript : MonoBehaviour
{
    private PlayerControls _playerControls;
    protected bool _canInteract = false, _isInteracting = false;
    protected Player _player;
    protected GameObject currentTextbox = null;

    public GameObject textBox;

    protected virtual void Awake()
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
        // textBox.SetActive(true);
        currentTextbox = textBox;
        _isInteracting = true;
        Debug.Log("You've reached a checkpoint!");
        PerformInteraction();
    }

    protected virtual void PerformInteraction()
    {
        Debug.Log("Performing Interaction...");
        currentTextbox.SetActive(true);
    }

    //Triggers when in range to interact with something.
    protected virtual void OnTriggerEnter(Collider other)
    {
        _canInteract = true;
        if(other.gameObject.GetComponentInParent<Player>() != null)
            _player = other.gameObject.GetComponentInParent<Player>();
    }

    //Makes sure the player doesn't interact with something out of range.
    protected virtual void OnTriggerExit(Collider other)
    {
        _canInteract = false;
        _isInteracting = false;
        // textBox.SetActive(false);
        if(currentTextbox != null)
            currentTextbox.SetActive(false);
    }
}
