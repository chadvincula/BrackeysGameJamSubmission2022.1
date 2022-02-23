using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : InteractScript
{
    private bool _playerInHiding = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void PerformInteraction()
    {
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
        _playerInHiding = true;
        _player.enabled = false;
        SpriteRenderer spriteRenderer = _player.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        Physics.IgnoreLayerCollision(9, 12, true); //Player x EntityVision
        Physics.IgnoreLayerCollision(9, 13, true); //Player x EntityBody
    }

    private void UnHidePlayer()
    {
        _playerInHiding = false;
        _player.enabled = true;
        SpriteRenderer spriteRenderer = _player.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        Physics.IgnoreLayerCollision(9, 12, false);
        Physics.IgnoreLayerCollision(9, 13, false);
    }
}
