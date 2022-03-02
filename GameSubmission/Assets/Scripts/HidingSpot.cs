using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : InteractScript
{
    [SerializeField] private bool isMobile = false;
    private bool _playerInHiding = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerInHiding && !_canInteract)
            _canInteract = true;
    }

    protected override void PerformInteraction()
    {
        if (_player.GetGrabbing()) return;
        if(!_playerInHiding)
        {
            Debug.Log("Hiding...");
            HidePlayer();
        }
        else
        {
            Debug.Log("No longer hiding");
            UnHidePlayer();
        }
    }

    private void HidePlayer()
    {
        _player.SetHidden(true);

        // foreach (var button in _player.buttonIcons)
        // {
        //     button.enabled = false;
        // }
        iconParent.enabled = false;
        
        _playerInHiding = true;
        if(isMobile)
        {
            transform.parent.parent = _player.transform;
            Vector3 tempPosition = transform.parent.localPosition;
            tempPosition.x = 0f;
            transform.parent.localPosition = tempPosition;
        }
        else
            _player.enabled = false;
        SpriteRenderer spriteRenderer = _player.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        Physics.IgnoreLayerCollision(9, 16, true); //Player x EntityVision
        Physics.IgnoreLayerCollision(9, 17, true); //Player x EntityBody
    }

    private void UnHidePlayer()
    {
        _player.SetHidden(false);
        
        // foreach (var button in _player.buttonIcons)
        // {
        //     button.enabled = true;
        // }
        iconParent.enabled = true;
        
        _playerInHiding = false;
        if(isMobile)
        {
            Transform parentOfPlayer = transform.parent.parent.parent;
            transform.parent.parent = parentOfPlayer;
        }
        else
            _player.enabled = true;
        SpriteRenderer spriteRenderer = _player.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        Physics.IgnoreLayerCollision(9, 16, false);
        Physics.IgnoreLayerCollision(9, 17, false);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (_player.GetGrabbing() || _player.GetHidden()) return;
        if(other.transform.parent != null && other.transform.parent.TryGetComponent(out Player player))
            base.OnTriggerEnter(other);
    }

    protected override void OnTriggerExit(Collider other)
    {
        if(other.transform.parent != null && other.transform.parent.TryGetComponent(out Player player))
            base.OnTriggerExit(other);
    }
}
