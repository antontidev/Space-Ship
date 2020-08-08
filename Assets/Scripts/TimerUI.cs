using System;
using TMPro;
using UniRx;
using UnityEngine;

/// <summary>
/// For textMeshPro SubscribeWithText UniRx extension
/// </summary>
namespace UniRx
{
    public static class UnityUIExtensions
    {
        public static IDisposable SubscribeToText<T>(this IObservable<T> source, TextMeshProUGUI text)
        {
            return source.SubscribeWithState(text, (x, t) => t.text = x.ToString());
        }
    }
}

[RequireComponent(typeof(TextMeshProUGUI))]
public class TimerUI : MonoBehaviour
{

    [SerializeField]
    Timer time;

    private TextMeshProUGUI timerText;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();

        time.roundedTimer.SubscribeToText(timerText);
    }
}
