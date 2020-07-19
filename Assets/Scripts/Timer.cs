using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Timer : MonoBehaviour
{
    [SerializeField]
    public float timer;

    private float defaultTimer;

    public delegate void TimerUp();

    public TimerUp Up;

    private void Start()
    {
        defaultTimer = timer;
        ResetTimer();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0.0f)
        {
            Up?.Invoke();
            ResetTimer();
        }
    }

    public void ResetTimer()
    {
        timer = defaultTimer;
    }
}
