using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelThreeAuthenticationKey : InteractScript
{
    [SerializeField] private SanityContoller _sanityContoller = null;
    protected override void HandleInteract(InputAction.CallbackContext context)
    {
        base.HandleInteract(context);
        if (base._canInteract)
        {
            Progression._hasLevel3Key = true;
            SceneManager.LoadScene("Day5");
        }
    }
}
