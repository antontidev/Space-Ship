﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public delegate void LevelLoaded(Level level);
    public LevelLoaded NextLevelLoaded;

    [SerializeField]
    public List<Level> levels;

    public int CurrentLevel
    {
        get; set;
    }

    private void Start()
    {
        NextLevelLoaded?.Invoke(levels[0]);
        CurrentLevel = 0;
    }

    public Level NextLevel()
    {
        var level = levels[CurrentLevel + 1];
        NextLevelLoaded?.Invoke(level);
        return level;
    }
}
