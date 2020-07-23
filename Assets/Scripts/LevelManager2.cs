using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager2 : MonoBehaviour
{
    public delegate void LevelLoaded(Level level);
    public LevelLoaded NextLevelLoaded;

    public Level level;
    public Level NextLevel()
    {
        NextLevelLoaded?.Invoke(level);
        return level;
    }
}
