using System;
using UniRx;
using UnityEngine;

/// <summary>
/// Timer which gets value from loaded level
/// </summary>
public class Timer : MonoBehaviour
{
    /// <summary>
    /// Actual timer value updated in Update callback
    /// </summary>
    private float timer;

    /// <summary>
    /// Observable timer for future purposes which can help to avoid
    /// using emitted property
    /// </summary>
    public ReactiveProperty<int> roundedTimer { get; private set; }

    public delegate void TimerUp();

    private bool emitted = false;

    /// <summary>
    /// Holds tiimer update observable
    /// </summary>
    private IDisposable timerUpdater;

    /// <summary>
    /// Used to indicated that timer is actually up
    /// </summary>
    public Action Up;

    private void Awake() 
    { 
        roundedTimer = new ReactiveProperty<int>();

        //timerUpdater = Observable.EveryUpdate().Subscribe(_ =>
        //{
        //    timer -= Time.deltaTime;

        //    roundedTimer.Value = Mathf.FloorToInt(timer);
        //});
    }

    ///// <remarks>
    ///// I prefer to use Observable timer instead of checking for it's actual
    ///// value and then emmiting TimerUp Event
    ///// </remarks>
    void Update()
    {
        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;

            roundedTimer.Value = Mathf.FloorToInt(timer);
        }
        else if (!emitted)
        {
            Up?.Invoke();
            emitted = true;
        }
    }

    /// <example>
    /// <code>
    /// LevelManager2 levelManager;
    /// Timer timer;
    /// 
    /// void OnEnable() 
    /// {
    ///     levelManager.NextLevelLoaded += timer.LevelLoaded;
    /// }
    /// 
    /// void OnDisable()
    /// {
    ///     levelManager.NextLevelLoaded -= timer.LevelLoaded;
    /// }
    /// </code>
    /// </example>
    public void LevelLoaded(Level level) => ResetTimer(level.levelTime);

    private void ResetTimer(float t)
    {
        timer = t;
        emitted = false;
    }
}
