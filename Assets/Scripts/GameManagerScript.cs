using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    GameObject gameTimer;

    List<ScriptableObject> levels;

    private Timer timer;
    private void Start()
    {
        timer = gameTimer.GetComponent<Timer>();  
    }

    void Update()
    {
        if (timer.timer <= 0)
        {
            timer.ResetTimer();
            goToLevelOne();
        }
    }

    void changeLevel()
    {
        SceneManager.LoadScene("GameScene");
    }

    void goToLevelOne()
    {
        SceneManager.LoadScene("GameScene");
    }
}
