﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Timer : MonoBehaviour
{
    [SerializeField]
    public float timer;

    void Update()
    {
        timer -= Time.deltaTime;
    }

    public void ResetTimer()
    {
        timer = 60;
    }
}