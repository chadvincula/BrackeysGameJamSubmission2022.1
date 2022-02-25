using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelTwoAuthenticationKey : InteractScript
{
    protected override void HandleInteract(InputAction.CallbackContext context)
    {
        base.HandleInteract(context);
        if (base._canInteract)
            Progression._hasLevel2Key = true;
    }

    protected override void OnTriggerExit(Collider other)
    {
        _canInteract = false;
        _isInteracting = false;
        StartCoroutine(DelayedInactiveTextbox(3f));

    }

    private IEnumerator DelayedInactiveTextbox(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        currentTextbox.SetActive(false);
    }
}
