using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timer;

    void Start()
    {
        timer = 60;
    }

    void Update()
    {
        timer -= Time.deltaTime;
    }

    void ResetTimer()
    {
        timer = 60;
    }
}
