using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class FlashQuote : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private int _randomLine;
    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _randomLine = Random.Range(0, 6);
        if (SceneManager.GetActiveScene().name != "TheBackrooms")
        {
            switch (_randomLine)
            {
                case 0:
                    _text.SetText("I thought I had been drinking water.");
                    break;
                case 1:
                    _text.SetText("It was not real.");
                    break;
                case 2:
                    _text.SetText("Must've been the tri-tip.");
                    break;
                case 3:
                    _text.SetText("Who spiked the drinks?");
                    break;
                case 4:
                    _text.SetText("It was not real.");
                    break;
                case 5:
                    _text.SetText("It was not real.");
                    break;
            }
        }
        else
        {
            switch (_randomLine)
            {
                case 0:
                    _text.SetText("Why am I here?");
                    break;
                case 1:
                    _text.SetText("It was not real.");
                    break;
                case 2:
                    _text.SetText("Note To Self: Avoid slime thing.");
                    break;
                case 3:
                    _text.SetText("This can't be happening.");
                    break;
                case 4:
                    _text.SetText("So cold...");
                    break;
                case 5:
                    _text.SetText("When will this end?");
                    break;
            }
        }
    }
}
