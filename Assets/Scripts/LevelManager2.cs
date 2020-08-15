using UnityEngine;

public class LevelManager2 : MonoBehaviour
{
    public delegate void LevelLoaded(Level level);
    public LevelLoaded NextLevelLoaded;

    public Level level;
    public Level NextLevel()
    {
#if UNITY_EDITOR 
        //level.levelTime = 5f;
#endif
        NextLevelLoaded?.Invoke(level);
        return level;
    }
}
