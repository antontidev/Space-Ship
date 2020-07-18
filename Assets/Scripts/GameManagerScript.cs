using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManagerScript : MonoBehaviour
{

    GameObject gameTimer = GameObject.Find("Timer");
    Timer theTimer = gameTimer.GetComponent<Timer>();

    void Update()
    {
        if (theTimer.timer <= 0)
        {
            theTimer.ResetTimer();
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
