using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;



public class GameManagerScript : MonoBehaviour
{

    

    void Update()
    {
        GameObject gameTimer = GameObject.FindGameObjectWithTag("Timer");
        Timer theTimer = gameTimer.GetComponent<Timer>();
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
