﻿using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    private GamePresenter gamePresenter;

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

    [SerializeField]
    private AudioSource gameSound;

    private void Awake()
    {
        var activeScene = SceneManager.GetActiveScene();

        int sceneScore;

        if (activeScene.name != "Level3")
        {
            sceneScore = activeScene.buildIndex - 1;
        }
        else
        {
            sceneScore = activeScene.buildIndex;
        }

        gameSound.mute = true;

        SetScore(sceneScore);
    }

    /// <summary>
    /// Callback for UI ExitButton
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
        gamePresenter.FadeScene();
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