using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{

    [SerializeField]
    Timer time;

    private TextMeshProUGUI timerText;

    void Start()
    {
        timerText = GameObject.Find("Canvas/Text").GetComponent<TextMeshProUGUI>();
    }


    void Update()
    {
   //     timerText.text = Mathf.Floor(time.timer).ToString();
    }
}
