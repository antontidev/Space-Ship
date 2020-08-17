using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using UniRx;
using UnityEngine.Playables;
using System.Collections;
using UnityEngine.Timeline;

public class IGameManager : MonoBehaviour
{
}

public class GameManagerScript : IGameManager
{
    [SerializeField]
    private FreeLookAddOn freeLookCamera;

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
        timelineManager.stopped += OnCutsceneEnded;
        spawner.planetSpawned += freeLookCamera.SetTargetObject;
        spawner.RocketSpawned += SubmitRocket;
    }

    private void OnDisable()
    {
        levelManager.NextLevelLoaded -= spawner.LevelLoaded;
        levelManager.NextLevelLoaded -= timer.LevelLoaded;
        levelManager.NextLevelLoaded -= activePartManager.LevelLoaded;
        timer.Up -= MakeDecision;
        timelineManager.stopped -= OnCutsceneEnded;
        spawner.planetSpawned -= freeLookCamera.SetTargetObject;
        spawner.RocketSpawned -= SubmitRocket;
    }

    private void SubmitRocket(Rocket rocket)
    {
        activePartManager.IsReady.Where(x => x == true).Subscribe(x =>
        {
            MakeDecision();
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
        var isReady = activePartManager.IsReady.Value;
        if (isReady)
        {
            timelineManager.Play(winCutscene);
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