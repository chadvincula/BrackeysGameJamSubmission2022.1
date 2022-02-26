using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SanityContoller : MonoBehaviour
{
    public float drainInterval = 5f, drainAmount = 0.05f;

    private float _timer = 0, sanity = 0.2f;
    private Image _sanityFill;
    private Progression _progression;
    private PostProcessVolume _ppv;
    
    private CinemachineBasicMultiChannelPerlin _perlin;

    private Vignette _vignette;

    private void Awake()
    {
        _ppv = FindObjectOfType<PostProcessVolume>();
        _ppv.profile.TryGetSettings(out _vignette);
        _perlin = FindObjectOfType<CinemachineBasicMultiChannelPerlin>();
    }

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
        if (sanity < 0) sanity = 0;
        _sanityFill.fillAmount = sanity;
        if (sanity >= 1)
        {
            if (SceneManager.GetActiveScene().name == "TheBackrooms")
                MaxSanityBR();
            else
                ResetToDayOne();
        }

        if (sanity < 0.4)
        {
            _vignette.intensity.value = 0.5f;
            _vignette.color.value = Color.black;
        }
        else if (sanity > 0.8)
        {
            _vignette.intensity.value = 0.5f;
            _vignette.color.value = Color.white;
            _perlin.m_AmplitudeGain = 0.5f;
        }
        else
        {
            _perlin.m_AmplitudeGain = 0;
        }

        if (sanity <= 0)
        {
            if (SceneManager.GetActiveScene().name == "TheBackrooms")
                MinSanityBR();
            else
                MinSanityOffice();
        }
    }
    
    //This could use a coroutine inside to make things dramatic. Polish.
    private void MaxSanityBR()
    {
        SceneManager.LoadScene("TheBackrooms");
    }
    
    //This should be different by being a GAMEOVER?
    private void MinSanityBR()
    {
        SceneManager.LoadScene("TheBackrooms");
    }
    
    //This could trigger the vignette + textBox;
    private void MinSanityOffice()
    {
        //Trigger vignette
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
            /*sanity -= drainAmount;
            if (sanity < 0) sanity = 0;
            _sanityFill.fillAmount = sanity;*/
            SetSanity(-drainAmount);
            _timer = 0;
        }
    }

    public void ResetToDayOne()
    {
        SceneManager.LoadScene("Day1");
    }

    public void GoToBackrooms()
    {
        SceneManager.LoadScene("TheBackrooms");
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
