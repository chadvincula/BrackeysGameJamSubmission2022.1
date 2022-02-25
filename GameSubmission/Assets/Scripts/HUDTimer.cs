using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDTimer : MonoBehaviour
{
    private TextMeshProUGUI _textMeshProUGUI;
    private float _timer = 0f;
    private void Start()
    {
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        var seconds = (int) (_timer % 60);
        var minutes = (int) (_timer / 60) % 60;
        var hours = (int) (_timer / 3600) % 24;

        var timerString = $"{hours:00}:{minutes:00}:{seconds:00}";
        _textMeshProUGUI.text = timerString;
    }
}
