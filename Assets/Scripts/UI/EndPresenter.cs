using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreFinder
{

}

public abstract class IEndPresenter : MonoBehaviour
{
    public abstract void SetScore(int score);
    public abstract void RetryGame();
    public abstract void ExitToMenu();
}

public class EndPresenter : IEndPresenter
{
    [SerializeField]
    private TextMeshProUGUI scoreText; 

    /// <summary>
    /// Menu scene name to load when user press exit
    /// </summary>
    [Scene]
    public string menuScene;

    /// <summary>
    /// Game level scene name to load when user press retry
    /// </summary>
    [Scene]
    public string gameScene;

    /// <summary>
    /// Callbacl for UI ExitButton
    /// </summary>
    public override void ExitToMenu()
    {
        SceneManager.LoadScene(menuScene);
    }

    /// <summary>
    /// Callback for UI RetryButton
    /// </summary>
    public override void RetryGame()
    {
        SceneManager.LoadScene(gameScene);
    }

    /// <summary>
    /// Set actual score value on EndScreen
    /// </summary>
    /// <param name="score"></param>
    public override void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
