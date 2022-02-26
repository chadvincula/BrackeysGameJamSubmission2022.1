using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Epilogue : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(TheEnd());
    }

    IEnumerator TheEnd()
    {
        yield return new WaitForSeconds(32);
        Application.Quit();
        Debug.Log("We quit!");
    }
}
