using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WelcomeScript : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private void Start()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(DailyWelcome());
    }

    IEnumerator DailyWelcome()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Day1":
                yield return new WaitForSeconds(8f);
                break;
            case "Day2":
                _text.SetText("Looks like you're a pretty hard worker! Starting today we'll be keeping an eye on you! Now get working!");
                yield return new WaitForSeconds(6f);
                break;
            case "Day3":
                _text.SetText("To mix up our daily welcome, here's a joke! What do you call a cow with three legs? ...TRI-TIP! We hope this motivates you!");
                yield return new WaitForSeconds(8f);
                break;
            case "Day4":
                _text.SetText("Today is extra special! Be sure to stay hyper-energized! Just don't go crazy with the free drinks!");
                yield return new WaitForSeconds(6f);
                break;
        }
        gameObject.SetActive(false);
    }
}
