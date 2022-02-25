using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SanityContoller : MonoBehaviour
{
    public float drainInterval = 5f, drainAmount = 0.05f;

    private float _timer = 0, sanity = 0.2f;
    private Image _sanityFill;
    private Progression _progression;

    private void Start()
    {
        _sanityFill = GetComponent<Image>();
        _progression = FindObjectOfType<Progression>();
        sanity = _progression.GetSanity();
        _sanityFill.fillAmount = sanity;
    }

    public float GetSanity()
    {
        return sanity;
    }

    public void SetSanity(float amount)
    {
        sanity += amount;
        _sanityFill.fillAmount = sanity;
        if (sanity >= 1)
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "TheBackrooms":
                    SceneManager.LoadScene("TheBackrooms");
                    break;
                default:
                    SceneManager.LoadScene("Day1");
                    break;
            }
        }
        else if (sanity <= 0)
        {
            if (SceneManager.GetActiveScene().name == "TheBackrooms")
                SceneManager.LoadScene("TheBackrooms");
        }
    }

    public void SaveSanityToProgress()
    {
        _progression.SaveSanity(sanity);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > drainInterval && sanity > 0)
        {
            sanity -= drainAmount;
            if (sanity < 0)
            {
                sanity = 0;
                if (SceneManager.GetActiveScene().name == "TheBackrooms")
                    SceneManager.LoadScene("TheBackrooms");
            }
            _sanityFill.fillAmount = sanity;
            _timer = 0;
        }
    }

    public void ResetToDayOne()
    {
        SceneManager.LoadScene("Day1");
    }

    public void ProceedToNextDay()
    {
        switch(gameObject.scene.name)
        {
            case "Day1":
            case "CharlesTestScene":
                SceneManager.LoadScene("Day2");
                break;
            case "Day2":
                SceneManager.LoadScene("Day3");
                break;
            case "Day3":
                SceneManager.LoadScene("Day4");
                break;
        }
    }
}
