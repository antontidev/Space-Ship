using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [SerializeField]
    private Timer timer;

    /// <summary>
    /// Reading actual timer value
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI timerText;


    // Start is called before the first frame update
    void Start()
    {
        SubscribeTimer();
    }

    /// <summary>
    /// Subscribe UI to Timer
    /// </summary>
    private void SubscribeTimer()
    {
        timer.roundedTimer.Where(x => x >= 0).Subscribe(x =>
        {
            timerText.text = x.ToString();
        });
    }
}
