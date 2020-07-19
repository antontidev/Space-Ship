using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void exitGame()
    {
        SceneManager.LoadScene("Menu");
    }
}
