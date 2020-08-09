using UnityEngine;
using UnityEngine.SceneManagement;

public class IGameManager : MonoBehaviour
{

}

public class GameManagerScript : IGameManager
{
    [SerializeField]
    public FreeLookAddOn freeLookCamera;

    [SerializeField]
    public Timer timer;

    [Scene]
    public string nextScene;

    [SerializeField]
    public LevelManager2 levelManager;

    [SerializeField]
    public PartsSpawner spawner;

    //For testing
    [SerializeField]
    public Rocket rocket;

    private void Start()
    {
        Application.targetFrameRate = 60;

        levelManager.NextLevel();
    }

    private void OnEnable()
    {
        levelManager.NextLevelLoaded += spawner.LevelLoaded;
        levelManager.NextLevelLoaded += timer.LevelLoaded;
        timer.Up += MakeDecision;
        spawner.planetSpawned += freeLookCamera.SetTargetObject;
    }

    private void OnDisable()
    {
        levelManager.NextLevelLoaded -= spawner.LevelLoaded;
        levelManager.NextLevelLoaded -= timer.LevelLoaded;
        timer.Up -= MakeDecision;
        spawner.planetSpawned -= freeLookCamera.SetTargetObject;
    }

    private void MakeDecision()
    {
        PlayCutscene();
        ChangeLevel();
    }

    private void PlayCutscene()
    {

    }

    private void ChangeLevel()
    {
        SceneManager.LoadScene(nextScene);
    }

    private void GoToEndScene(bool isReady)
    {
        var go = new GameObject();

        go.name = "Ready";

        go.tag = "Ready";

        go.AddComponent<IsReady>().isReady = isReady;

        DontDestroyOnLoad(go);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}