using System;
using UniRx;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using Zenject;

public class IGameManager : MonoBehaviour
{
}

public class GameManagerScript : IGameManager
{
    [SerializeField]
    private GamePresenter gamePresenter;

    [SerializeField]
    private Timer timer;

    [SerializeField]
    private LevelManager2 levelManager;

    [SerializeField]
    private PartsSpawner spawner;

    [SerializeField]
    private PlayableDirector timelineManager;

    [SerializeField]
    private TimelineAsset winCutscene;

    [SerializeField]
    private TimelineAsset looseCutscene;

    [Inject]
    private ActivePartManager activePartManager;

    [Inject]
    private LevelLoader levelLoader;

    [Inject]
    private RocketInventory rocketInventory;

    [Scene]
    public string nextScene;

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
        spawner.RocketSpawned += SubmitRocket;
        // Obsolete call  planet already exist
        //spawner.planetSpawned += freeLookCamera.SetTargetObject;
    }

    private void OnDisable()
    {
        levelManager.NextLevelLoaded -= spawner.LevelLoaded;
        levelManager.NextLevelLoaded -= timer.LevelLoaded;
        levelManager.NextLevelLoaded -= activePartManager.LevelLoaded;
        timer.Up -= MakeDecision;
        spawner.RocketSpawned -= SubmitRocket;
        //timelineManager.stopped -= OnCutsceneEnded;
        // Obsolete call planet already exist
        //spawner.planetSpawned -= freeLookCamera.SetTargetObject;
    }

    private void SubmitRocket(Rocket rocket)
    {
        activePartManager.IsReady.Where(x => x == true).Subscribe(x =>
        {
            //MakeDecision();
        });
    }

    /// <summary>
    /// Plays cutscene and then loading the level;
    /// </summary>
    private void MakeDecision()
    {
        PlayCutscene();
    }

    private void PlayCutscene()
    {
        var canNext = rocketInventory.Full;

        if (canNext)
        {
            timelineManager.Play(winCutscene);

            ChangeLevel();
        }
        else
        {
            timelineManager.Play(looseCutscene);
        }
    }

    /// <summary>
    /// Invokes when playableDirector ends playing the cutscene
    /// </summary>
    /// <param name="timelineManager"></param>
    [Obsolete("No longer needed")]
    private void OnCutsceneEnded(PlayableDirector timelineManager)
    {
        ChangeLevel();
    }

    /// <summary>
    /// Not so good as it can be
    /// </summary>
    private void ChangeLevel()
    {
        var isReady = activePartManager.IsReady.Value;

        if (isReady && !levelLoader.IsLastLevel)
        {
            gamePresenter.FadeScene();

            levelLoader.LoadLevel(nextScene);
        }
        else
        {
            gamePresenter.ShowEndScreen();
        }
    }
}