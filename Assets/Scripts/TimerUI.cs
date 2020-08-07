using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UniRx;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TimerUI : MonoBehaviour
{

    [SerializeField]
    Timer time;

    private TextMeshProUGUI timerText;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();

        time.roundedTimer.Subscribe(x => {
            UpdateText(x);
        });
    }

    void UpdateText(int timer) => timerText.text = timer.ToString();
}
