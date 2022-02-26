using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyDarknessFade : MonoBehaviour
{
    public GameObject gameplayHUD;

    private void Start()
    {
        StartCoroutine(ActivateHUD());
    }

    IEnumerator ActivateHUD()
    {
        yield return new WaitForSeconds(2);
        gameplayHUD.SetActive(true);
    }
}
