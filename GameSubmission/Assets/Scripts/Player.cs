using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    //Movement variables to edit within Inspector
    [SerializeField] private float groundSpeed = 10f, jumpStrength = 5f, gravityMod = 1f;

    //Hidden Variables tied to the player. Add more as needed.
    private float _sanity = 0f, _gravity = -9.81f;
    private bool _isHidden = false, _canShift = true, _isGrabbing = false;
    public string shiftAction;

    //References to PlayerInput and CharacterController
    private PlayerControls _playerInput;
    private CharacterController _body;
    private Vector3 _velocity;
    private Animator _animator;

    public VisibilityScript visibilityScript;
    public SpriteRenderer[] buttonIcons;

    public GameObject smallBox;

    private void Awake()
    {
        _playerInput = new PlayerControls();
        _body = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.Move.performed += HandleMove;
        _playerInput.Player.Jump.performed += HandleJump;
        _playerInput.Player.ShiftUp.performed += HandleSUP;
        _playerInput.Player.ShiftDown.performed += HandleSDN;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.Player.Move.performed -= HandleMove;
        _playerInput.Player.Jump.performed -= HandleJump;
        _playerInput.Player.ShiftUp.performed -= HandleSUP;
        _playerInput.Player.ShiftDown.performed -= HandleSDN;
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
        if(movingVector.magnitude != 0) _animator.SetBool("walking", true);
        else _animator.SetBool("walking", false);

        //Then we move the body as affected by gravity.
        _velocity.y += (_gravity * gravityMod) * Time.deltaTime;
        _body.Move(_velocity * Time.deltaTime);
    }

    //All this does is just flip the player sprite to the left or right.
    private void HandleMove(InputAction.CallbackContext context)
    {
        if (context.ReadValue<float>() < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            foreach (var icon in buttonIcons)
            {
                icon.flipX = true;
            }
        }
        else if (context.ReadValue<float>() > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            foreach (var icon in buttonIcons)
            {
                icon.flipX = false;
            }
        }
    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        if(_body.isGrounded) _velocity.y += jumpStrength;
    }

    private void HandleSUP(InputAction.CallbackContext context)
    {
        if (!_canShift) return;
        switch (shiftAction)
        {
            case "EnterBR2":
                _body.Move(new Vector3(0,0,2));
                visibilityScript.EnterLayer2();
                break;
            case "EnterBR3":
                _body.Move(new Vector3(0,0,2));
                visibilityScript.EnterLayer3();
                break;
            case "EnterBR4":
                _body.Move(new Vector3(0,0,2));
                visibilityScript.EnterLayer4();
                break;
            case "EnterClosetRoom":
                if(smallBox != null) smallBox.SetActive(false);
                _body.Move(new Vector3(0,0,2));
                visibilityScript.EnterLayer2();
                break;
        }
    }

    private void HandleSDN(InputAction.CallbackContext context)
    {
        if (!_canShift) return;
        switch (shiftAction)
        {
            case "LeaveBR2":
                _body.Move(new Vector3(0,0,-2));
                visibilityScript.LeaveLayer2();
                break;
            case "LeaveBR3":
                _body.Move(new Vector3(0,0,-2));
                visibilityScript.LeaveLayer3();
                break;
            case "LeaveBR4":
                _body.Move(new Vector3(0,0,-2));
                visibilityScript.LeaveLayer4();
                break;
            case "LeaveClosetRoom":
                if(smallBox != null) smallBox.SetActive(true);
                _body.Move(new Vector3(0,0,-2));
                visibilityScript.LeaveLayer2();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnterBR2") || other.CompareTag("EnterBR3") || other.CompareTag("EnterBR4") ||
            other.CompareTag("LeaveBR2") || other.CompareTag("LeaveBR3") || other.CompareTag("LeaveBR4") ||
            other.CompareTag("EnterClosetRoom") || other.CompareTag("LeaveClosetRoom"))
        {
            _canShift = true;
            shiftAction = other.tag;
            SpriteRenderer shiftUpButton = buttonIcons[2];
            SpriteRenderer shiftDownButton = buttonIcons[3];
            if(other.tag.Contains("Enter"))
            {
                shiftUpButton.gameObject.SetActive(_canShift);
                shiftDownButton.gameObject.SetActive(!_canShift);
            }
            else if(other.tag.Contains("Leave"))
            {
                shiftDownButton.gameObject.SetActive(_canShift);
                shiftUpButton.gameObject.SetActive(!_canShift);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnterBR2") || other.CompareTag("EnterBR3") || other.CompareTag("EnterBR4") ||
            other.CompareTag("LeaveBR2") || other.CompareTag("LeaveBR3") || other.CompareTag("LeaveBR4") ||
            other.CompareTag("EnterClosetRoom") || other.CompareTag("LeaveClosetRoom"))
        {
            _canShift = false;
            shiftAction = null;
            SpriteRenderer shiftUpButton = buttonIcons[2];
            SpriteRenderer shiftDownButton = buttonIcons[3];
            shiftUpButton.gameObject.SetActive(_canShift);
            shiftDownButton.gameObject.SetActive(_canShift);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.moveDirection.y >= -0.3f)
        {
            // Debug.Break();
            Rigidbody rb = hit.collider.attachedRigidbody;
            if(rb != null)
            {
                // Debug.Break();
                // Vector3 forceDirection = transform.position - hit.transform.position;
                // forceDirection.y = 0f;
                // forceDirection.Normalize();


                var inputDirection = _playerInput.Player.Move.ReadValue<float>();
                float pushForce = (rb.mass <= 1f) ? inputDirection * groundSpeed : inputDirection * groundSpeed / rb.mass;
                var movingVector = new Vector3(pushForce, 0f, 0f);
                rb.AddForceAtPosition(movingVector, transform.position, ForceMode.Force);
            }
        }
    }

    public void AllowMovement(bool permissionToMove)
    {
        if(permissionToMove)
        {
            _playerInput.Player.Move.Enable();
            _playerInput.Player.Jump.Enable();
        }
        else
        {
            _playerInput.Player.Move.Disable();
            _playerInput.Player.Jump.Disable();
        }
    }

    public bool GetGrabbing()
    {
        return _isGrabbing;
    }

    public void SetGrabbing(bool status)
    {
        _isGrabbing = status;
        _animator.SetBool("carrying", _isGrabbing);
    }

    public bool GetHidden()
    {
        return _isHidden;
    }

    public void SetHidden(bool status)
    {
        _isHidden = status;
    }
}
