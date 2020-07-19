using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class playBtn : MonoBehaviour
{
  public void playGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
