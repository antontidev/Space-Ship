using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{

    [SerializeField]
    private Timer timer;

    [Scene]
    public string nextScene;

    [SerializeField]
    private LevelManager2 levelManager;

    [SerializeField]
    private PartsSpawner spawner;

    private GameObject planet;

    private GameObject rocketObj;

    //For testing
    [SerializeField]
    public Rocket rocket;

    private void Start()
    {

        Application.targetFrameRate = 300;
        timer.Up += MakeDecision;
        levelManager.NextLevelLoaded += NotifyManagers;

        levelManager.NextLevel();
    }

    private void NotifyManagers(Level level)
    {
        planet = spawner.SpawnPlanet(planet: level.planet);

        var planetComponent = planet.GetComponent<Planet>();

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
        //Rewrite
        if (rocket.IsReady)
        {
            PlayCutscene();
            ChangeLevel();
        }
        else
        {
            GoToEndScene(rocket.IsReady);
        }
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