using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DailyText : MonoBehaviour
{
    private TextMeshProUGUI _text;

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        if (SceneManager.GetActiveScene().name != "TheBackrooms")
            _text.SetText(SceneManager.GetActiveScene().name);
        else
        {
            _text.SetText("???");
        }
    }
}
