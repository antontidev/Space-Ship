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

    public enum GameState { Ready, Running, Lose, Win }

    public delegate void OnStateChangeHandler();
    public event OnStateChangeHandler OnStateChange;

    public GameState gameState { get; private set; }

    [SerializeField]
    Timer timer;

    [SerializeField]
    LevelManager levelManager;

    [SerializeField]
    PartsSpawner spawner;

    [SerializeField]
    PartsSpawner trashManager;

    private Rocket rocket;

    private void Start()
    {
        Instance = this;
        gameState = GameState.Ready;
        timer.Up += MakeDecision;
        levelManager.NextLevelLoaded += NotifyManagers;
    }

    void NotifyManagers(Level level)
    {
        spawner.SubmitList(level.modules);
        StartCoroutine(trashManager.Spawn());
        var newRocket = Instantiate(level.rocket);

        rocket = newRocket.GetComponent<Rocket>();

        var planet = Instantiate(level.planet);
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
            gameState = GameState.Lose;
        }

        GoToEndScene(rocket.IsReady);
    }

    public KeyValuePair<GameObject, bool> CheckPart(GameObject part)
    {
        return new KeyValuePair<GameObject, bool>(part, rocket.CheckPart(part));
    }

    void ChangeLevel()
    {
        levelManager.NextLevel();
    }

    void GoToLevelOne()
    {
        SceneManager.LoadScene("GameScene");
    }

    void GoToEndScene(bool isReady)
    {

    }
}
