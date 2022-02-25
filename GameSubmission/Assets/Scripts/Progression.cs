using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progression : MonoBehaviour
{
    public float startingSanity = 0.8f;
    public static bool _hasLevel2Key = false;
    public static bool _hasLevel3Key = false;

    public float GetSanity()
    {
        return startingSanity;
    }

    public void SaveSanity(float amount)
    {
        startingSanity = amount;
    }
}
