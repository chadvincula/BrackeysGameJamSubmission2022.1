using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //Movement variables to edit within Inspector
    [SerializeField] private float groundSpeed = 10f, jumpStrength = 5f, gravityMod = 1f;
    
    //Hidden Variables tied to the player. Add more as needed.
    private float _sanity = 0f, _gravity = -9.81f;
    private bool _isHidden = false, _canInteract = false, _canGrab = false;

    //References to PlayerInput and CharacterController
    private PlayerControls _playerInput;
    private CharacterController _body;

    private void Awake()
    {
        _playerInput = new PlayerControls();
        _body = GetComponent<CharacterController>();
    }
    
    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.Jump.performed += HandleJump;
        _playerInput.Player.Grab.performed += HandleGrab;
        _playerInput.Player.Interact.performed += HandleInteract;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.Player.Jump.performed -= HandleJump;
        _playerInput.Player.Grab.performed -= HandleGrab;
        _playerInput.Player.Interact.performed -= HandleInteract;
    }

    private void Update()
    {
        //First we move the body according to player input, and rotate it.
        var inputDirection = _playerInput.Player.Move.ReadValue<float>();
        var movingVector = new Vector3(inputDirection * groundSpeed, 0f, 0f);
        if(movingVector.x < 0) transform.rotation = Quaternion.Euler(0, 180, 0);
        else if (movingVector.x > 0) transform.rotation = Quaternion.Euler(0, 0, 0);
        _body.Move(movingVector * Time.deltaTime);

        //Then we move the body as affected by gravity.
        var fallingVector = new Vector3(0f, _gravity * gravityMod, 0f);
        _body.Move(fallingVector * Time.deltaTime);
    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        Debug.Log("Player is Jumping");
    }
    
    private void HandleGrab(InputAction.CallbackContext context)
    {
        Debug.Log("Player is grabbing.");
    }
    
    private void HandleInteract(InputAction.CallbackContext context)
    {
        Debug.Log("Player is interacting.");
    }
}
