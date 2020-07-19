using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    //For testing
    [SerializeField]
    public Rocket rocket;


    private void Start()
    {
        var levelManagerObj = GameObject.FindGameObjectsWithTag("LevelManager");

        if (levelManagerObj.Length == 0)
        {
            var obj = Instantiate(levelManagerObject);

            levelManager = obj.GetComponent<LevelManager>();
        }
        else
        {
            levelManager = levelManagerObj[0].GetComponent<LevelManager>();
        }

        Instance = this;
        gameState = GameState.Ready;
        timer.Up += MakeDecision;
        levelManager.NextLevelLoaded += NotifyManagers;

        levelManager.NextLevel();
    }

    void NotifyManagers(Level level)
    {
        var planet = spawner.SpawnPlanet(level.planet);
        var planetComponent = planet.GetComponent<Planet>();

        rocket = spawner.SpawnRocket(level.rocket, planetComponent.spawnRocketPostition);
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
//        GoToEndScene(rocket.IsReady);
    }

    public KeyValuePair<GameObject, bool> CheckPart(GameObject part)
    {
        return new KeyValuePair<GameObject, bool>(part, rocket.CheckPart(part));
    }

    void ChangeLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GoToLevelOne()
    {
        SceneManager.LoadScene("GameScene");
    }

    void GoToEndScene(bool isReady)
    {

    }
}
