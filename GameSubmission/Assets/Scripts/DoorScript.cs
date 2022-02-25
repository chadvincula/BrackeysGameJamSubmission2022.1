using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DoorScript : InteractScript
{
    protected override void HandleInteract(InputAction.CallbackContext context)
    {
        if (base._canInteract)
        {
            if(Progression._hasLevel3Key)
                SceneManager.LoadScene("Closet");
            else if(Progression._hasLevel2Key)
                SceneManager.LoadScene("TheBackrooms");
            else
                SceneManager.LoadScene("Day1");
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void OnTriggerExit(Collider other)
    {
        _canInteract = false;
        _isInteracting = false;
    }
}
