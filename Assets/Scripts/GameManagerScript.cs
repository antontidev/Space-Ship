using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance
    {
        get; private set;
    }

    public enum GameState { Ready, Running, Loose, Win }

    public delegate void OnStateChangeHandler();
    public event OnStateChangeHandler OnStateChange;

    public GameState gameState { get; private set; }

    [SerializeField]
    Timer timer;

    [SerializeField]
    LevelManager levelManager;

    [SerializeField]
    public GameObject levelManagerObject;

    [SerializeField]
    PartsSpawner spawner;

    [SerializeField]
    TrashSpawner trashManager;

    private GameObject planet;

    private GameObject rocketObj;

    //For testing
    [SerializeField]
    public Rocket rocket;


    [SerializeField]
    public GameObject placeholder;

    private void Awake()
    {

        Instance = this;
        gameState = GameState.Ready;
        timer.Up += MakeDecision;
        levelManager.NextLevelLoaded += NotifyManagers;

        levelManager.NextLevel();
    }

    void NotifyManagers(Level level)
    {
        planet = spawner.SpawnPlanet(level.planet);
        var planetComponent = planet.GetComponent<Planet>();

        rocketObj = spawner.SpawnRocket(level.rocket, planetComponent.spawnRocketPostition);
        rocket = rocketObj.GetComponent<Rocket>();

        spawner.SubmitList(level.modules);
        rocket.SubmitTrueParts(level.trueModules);

        StartCoroutine(trashManager.Spawn());
        StartCoroutine(spawner.Spawn(level.trueModules));

        timer.ResetTimer(level.levelTime);
    }

    void MakeDecision()
    {
        if (rocket.IsReady)
        {
            gameState = GameState.Win;
            ChangeLevel();
        }
        else
        {
            gameState = GameState.Loose;
            ChangeLevel();
        }

        GoToEndScene(rocket.IsReady);
//        GoToEndScene(rocket.IsReady);
    }

    public KeyValuePair<GameObject, bool> CheckPart(GameObject part)
    {
        return new KeyValuePair<GameObject, bool>(part, rocket.CheckPart(part));
    }

    void ChangeLevel()
    {
        //ClearLevel();
        //levelManager.NextLevel();
    }

    private void ClearLevel()
    {
        DestroyImmediate(planet);
        foreach (Transform child in placeholder.transform)
        {
            DestroyImmediate(child.gameObject);
        }

        DestroyImmediate(rocketObj);
    }

    void GoToLevelOne()
    {
        SceneManager.LoadScene("GameScene");
    }

    void GoToEndScene(bool isReady)
    {
        var go = new GameObject();

        go.tag = "Ready";

        go.AddComponent<IsReady>().isReady = isReady;

        DontDestroyOnLoad(go);

        SceneManager.LoadScene(2);
    }
}
