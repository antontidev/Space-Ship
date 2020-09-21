using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeteoriteManager : MonoBehaviour
{
    [Header ("Meteorite prefab")]
    public GameObject meteoritePrefab;

    private bool envoked = false;

    void Update()
    {
        if (envoked == false)
        {
            Invoke("spawnMeteorite", 5);
            envoked = true;
        }

    }

    void spawnMeteorite()
    {
        Instantiate(meteoritePrefab, UnityEngine.Random.onUnitSphere * 5, transform.rotation, null);
        envoked = false;
    }
}
