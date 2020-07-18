using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]
    public float timer = 60;
    [SerializeField]

    void Start()
    {
        timer = 60;
    }

    void Update()
    {
        timer -= Time.deltaTime;
    }

    public void ResetTimer()
    {
        timer = 60;
    }
}
