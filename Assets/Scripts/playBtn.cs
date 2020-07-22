using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class playBtn : MonoBehaviour
{
    [Scene]
    public string gameScene;
  public void playGame()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
