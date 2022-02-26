using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelThreeAuthenticationKey : InteractScript
{
    [SerializeField] private SanityContoller _sanityContoller = null;
    protected override void HandleInteract(InputAction.CallbackContext context)
    {
        base.HandleInteract(context);
        if (base._canInteract)
        {
            Progression._hasLevel3Key = true;
            _sanityContoller.ProceedToNextDay();
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        base.interactableIcon.SetActive(true);
    }

    protected override void OnTriggerExit(Collider other)
    {
        _canInteract = false;
        _isInteracting = false;
        // StartCoroutine(DelayedInactiveTextbox(3f));
        base.interactableIcon.SetActive(false);

    }

    // private IEnumerator DelayedInactiveTextbox(float seconds)
    // {
    //     yield return new WaitForSeconds(seconds);
    //     currentTextbox.SetActive(false);
    // }
}
