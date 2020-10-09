﻿using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Levels/CreateLevel", order = 1)]
public class Level : ScriptableObject
{
    [SerializeField]
    public List<GameObject> modules;
    [SerializeField]
    public List<GameObject> trueModules;
    [SerializeField]
    public GameObject rocket;
    [SerializeField]
    public float levelTime;
    [SerializeField]
    public int astronautCount;
}
