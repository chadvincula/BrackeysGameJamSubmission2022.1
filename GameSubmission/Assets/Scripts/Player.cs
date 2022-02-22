using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    //Movement variables to edit within Inspector
    [SerializeField] private float groundSpeed = 10f, jumpStrength = 5f, gravityMod = 1f;
    
    //Hidden Variables tied to the player. Add more as needed.
    private float _sanity = 0f, _gravity = -9.81f;
    private bool _isHidden = false;

    //References to PlayerInput and CharacterController
    private PlayerControls _playerInput;
    private CharacterController _body;
    private Vector3 _velocity;

    private void Awake()
    {
        _playerInput = new PlayerControls();
        _body = GetComponent<CharacterController>();
    }
    
    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.Move.performed += HandleMove;
        _playerInput.Player.Jump.performed += HandleJump;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.Player.Move.performed -= HandleMove;
        _playerInput.Player.Jump.performed -= HandleJump;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        //Stop the player from falling once they hit the ground.
        if (_body.isGrounded && _velocity.y < 0f) _velocity.y = 0f;
        
        //First we move the body according to player input.
        var inputDirection = _playerInput.Player.Move.ReadValue<float>();
        var movingVector = new Vector3(inputDirection * groundSpeed, 0f, 0f);
        _body.Move(movingVector * Time.deltaTime);
        
        //Then we move the body as affected by gravity.
        _velocity.y += (_gravity * gravityMod) * Time.deltaTime;
        _body.Move(_velocity * Time.deltaTime);
    }

    //All this does is just flip the player sprite to the left or right.
    private void HandleMove(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() < 0) transform.rotation = Quaternion.Euler(0, 180, 0); //Moving Left.
        else if (context.ReadValue<float>() > 0) transform.rotation = Quaternion.Euler(0, 0, 0); //Moving Right.
    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        if(_body.isGrounded) _velocity.y += jumpStrength;
    }
}
