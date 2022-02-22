using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector3 = UnityEngine.Vector3;

public class GrabScript : MonoBehaviour
{
    private Player _playerParent;
    private PlayerControls _playerControls;
    private GameObject _grabbableObject;

    private bool _canGrab = false, _isGrabbing = false;

    private void Awake()
    {
        _playerParent = GetComponentInParent<Player>();
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
        _playerControls.Player.Grab.performed += HandleGrab;
    }

    private void OnDisable()
    {
        _playerControls.Disable();
        _playerControls.Player.Grab.performed -= HandleGrab;
    }

    private void HandleGrab(InputAction.CallbackContext context)
    {
        //Checks to see  if the player is currently grabbing or not.
        switch (_isGrabbing)
        {
            //This just makes sure that there is a reference to a grabbable object.
            case false when _grabbableObject != null:
                _isGrabbing = true;
                _grabbableObject.transform.parent = transform;
                _grabbableObject.transform.localPosition = Vector3.zero;
                _grabbableObject.GetComponent<Rigidbody>().isKinematic = true;
                break;
            case true:
                _isGrabbing = false;
                _grabbableObject.transform.parent = null;
                _grabbableObject.GetComponent<Rigidbody>().isKinematic = false;
                break;
        }
    }

    //Triggers when in range to grab something.
    private void OnTriggerEnter(Collider other)
    {
        if (_isGrabbing) return;
        _canGrab = true;
        _grabbableObject = other.gameObject;
    }

    //Removes the reference once the player is out of range. Can't grab what's not there.
    private void OnTriggerExit(Collider other)
    {
        if (_isGrabbing) return;
        _canGrab = false;
        _grabbableObject = null;
    }
}
