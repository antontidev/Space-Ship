using UnityEngine;
using Zenject;

public class LevelManager2 : MonoBehaviour
{
    public delegate void LevelLoaded(Level level);

    public LevelLoaded NextLevelLoaded;

    public Level level;

    private RocketInventory rocketInventory;

    [Inject]
    public void Construct(RocketInventory rocketInventory)
    {
        this.rocketInventory = rocketInventory;
    }


    public Level NextLevel()
    {
#if UNITY_EDITOR 
        //level.levelTime = 5000f;
#endif
        var onRocketParts = level.onRocketParts;

        rocketInventory.SubmitOnRocketParts(onRocketParts);

        NextLevelLoaded?.Invoke(level);
        return level;
    }
}
