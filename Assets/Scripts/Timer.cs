﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
<<<<<<< HEAD
    public float timer = 60;
    [SerializeField]
=======
    public float timer;
>>>>>>> spawner

    void Update()
    {
        timer -= Time.deltaTime;
    }

    public void ResetTimer()
    {
        timer = 60;
    }
}
