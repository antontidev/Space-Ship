using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using UniRx;

public class IGameManager : MonoBehaviour
{

}

public class GameManagerScript : IGameManager
{
    [SerializeField]
    public FreeLookAddOn freeLookCamera;

    [SerializeField]
    private GamePresenter gamePresenter;

    [SerializeField]
    public Timer timer;

    [Inject]
    private ActivePartManager activePartManager;

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
        levelManager.NextLevelLoaded += activePartManager.LevelLoaded;
        timer.Up += MakeDecision;
        spawner.planetSpawned += freeLookCamera.SetTargetObject;
        spawner.RocketSpawned += SubmitRocket;
    }

    private void OnDisable()
    {
        levelManager.NextLevelLoaded -= spawner.LevelLoaded;
        levelManager.NextLevelLoaded -= timer.LevelLoaded;
        levelManager.NextLevelLoaded -= activePartManager.LevelLoaded;
        timer.Up -= MakeDecision;
        spawner.planetSpawned -= freeLookCamera.SetTargetObject;
        spawner.RocketSpawned -= SubmitRocket;
    }

    private void SubmitRocket(Rocket rocket)
    {
        this.rocket = rocket;

        activePartManager.IsReady.Where(x => x == true).Subscribe(x =>
        {
            MakeDecision();
        });
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
        var isReady = activePartManager.IsReady.Value;

        if (isReady && SceneManager.GetActiveScene().name != "Level3")
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            gamePresenter.ShowEndScreen();
        }
    }
}