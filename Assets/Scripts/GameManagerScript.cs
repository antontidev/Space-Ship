﻿using System;
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
        timer.Up += MakeDecision;
        levelManager.NextLevelLoaded += NotifyManagers;

        levelManager.NextLevel();
    }

    private void NotifyManagers(Level level)
    {
        var planet = spawner.SpawnPlanet(planet: level.planet);

        cam.LookAt = cam.Follow = planet.transform;

        planetComponent = planet.GetComponent<Planet>();

        planetComponent.gestureController = gestureController;

      //  gestureController.Dragged += planetComponent.GestureController_Dragged;

        rocketObj = spawner.SpawnRocket(level.rocket, planetComponent.spawnRocketPostition);
        rocket = rocketObj.GetComponent<Rocket>();

        spawner.SubmitList(level.modules);
        rocket.SubmitTrueParts(level.trueModules);

        StartCoroutine(spawner.Spawn(level.trueModules));
        StartCoroutine(spawner.SpawnTrash());

        timer.ResetTimer(level.levelTime);
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