using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Timer : MonoBehaviour
{
    [SerializeField]
    public float timer;

    public delegate void TimerUp();

    private bool emitted = false;

    public TimerUp Up;

    void Update()
    {
        if (timer > 0.0f)
        {
            timer -= Time.deltaTime;
        }
        else if (!emitted)
        {
            Up?.Invoke();
            emitted = true;
        }
    }

    public void LevelLoaded(Level level)
    {
        ResetTimer(level.levelTime);
    }

    private void ResetTimer(float t)
    {
        timer = t;
        emitted = false;
    }
}
