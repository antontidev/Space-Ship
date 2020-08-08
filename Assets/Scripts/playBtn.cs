using UnityEngine;
using UnityEngine.SceneManagement;

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
