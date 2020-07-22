using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Timer : MonoBehaviour
{
    [SerializeField]
    public float timer;

    private float defaultTimer;

    public delegate void TimerUp();

    private bool emitted = false;

    public TimerUp Up;

    private void Start()
    {
        defaultTimer = timer;
    }

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

    public void ResetTimer(float t)
    {
        timer = t;
        emitted = false;
    }
}
