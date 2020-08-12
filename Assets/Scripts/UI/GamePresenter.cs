using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

public abstract class IGamePresetner : MonoBehaviour
{
    public abstract void SetTimer(int timer);
    public abstract void PauseGame();
    public abstract void UnpauseGame();
}

public class GamePresenter : IGamePresetner
{
    [SerializeField]
    private Timer timer;

    [SerializeField]
    private TextMeshProUGUI timerText;

    [SerializeField]
    private GameObject pauseScreen;

    void Start()
    {
        Initialize();
    }
    
    /// <summary>
    /// Callback which executes when timer value is changes
    /// </summary>
    /// <param name="timer"></param>
    public override void SetTimer(int timer)
    {
        timerText.text = timer.ToString();
    }

    /// <summary>
    /// Subscribe rounded timer to timer TextView
    /// </summary>
    private void Initialize()
    {
        timer.roundedTimer.Subscribe(x =>
        {
            SetTimer(x);
        });
    }

    /// <summary>
    /// Callback for OnClick event on PauseScreen
    /// </summary>
    public override void PauseGame()
    {
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
    }

    /// <summary>
    /// Callback for OnClick event on PauseScreen
    /// </summary>
    public override void UnpauseGame()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
    }
}
