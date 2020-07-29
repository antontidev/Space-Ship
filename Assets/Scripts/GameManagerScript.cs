using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InputSamples.Gestures;
using Cinemachine;

public class IGameManager : MonoBehaviour
{

}

public class GameManagerScript : IGameManager
{
    [SerializeField]
    private CinemachineFreeLook cam;

    [SerializeField]
    private GestureController gestureController;

    [SerializeField]
    public Timer timer;

    [Scene]
    public string nextScene;

    [SerializeField]
    public LevelManager2 levelManager;

    [SerializeField]
    public PartsSpawner spawner;

    private Planet planetComponent;

    private GameObject rocketObj;

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
        spawner.planetSpawned += SetCameraTarget;
        timer.Up += MakeDecision;
    }

    private void OnDisable()
    {
        levelManager.NextLevelLoaded -= spawner.LevelLoaded;
        levelManager.NextLevelLoaded -= timer.LevelLoaded;
        spawner.planetSpawned -= SetCameraTarget;
        timer.Up -= MakeDecision;
    }

    private void SetCameraTarget(Transform planetTransform)
    {
        cam.LookAt = cam.Follow = planetTransform;
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