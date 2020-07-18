using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField]
    GameObject gameTimer;

    private Timer timer;



    void Update()
    {
        GameObject gameTimer = GameObject.FindGameObjectWithTag("Timer");
        Timer theTimer = gameTimer.GetComponent<Timer>();
        if (theTimer.timer <= 0)
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
