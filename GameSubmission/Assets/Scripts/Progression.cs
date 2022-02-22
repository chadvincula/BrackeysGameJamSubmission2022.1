using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progression : MonoBehaviour
{
    public float startingSanity = 0.8f;
    //private bool _hasLevel2Key = false;
    //private bool _hasLevel3Key = false;

    public float GetSanity()
    {
        return startingSanity;
    }

    public void SaveSanity(float amount)
    {
        startingSanity = amount;
    }
}
