using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject BiosOverlay;
    public void StartGame()
    {
        SceneManager.LoadScene("Day1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        StartCoroutine(BootUpFade());
    }

    IEnumerator BootUpFade()
    {
        yield return new WaitForSeconds(15.5f);
        BiosOverlay.SetActive(false);
    }
}
