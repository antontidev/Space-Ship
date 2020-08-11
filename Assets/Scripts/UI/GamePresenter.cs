using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

public abstract class IGamePresetner : MonoBehaviour
{
    public abstract void SetTimer(int timer);
}

public class GamePresenter : IGamePresetner
{
    [SerializeField]
    private Timer timer;

    [SerializeField]
    private TextMeshProUGUI timerText; 

    public override void SetTimer(int timer)
    {
        timerText.text = timer.ToString();
    }

    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        timer.roundedTimer.Subscribe(x =>
        {
            SetTimer(x);
        });
    }
}
