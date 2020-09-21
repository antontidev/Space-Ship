using UnityEngine;
using Zenject;

public abstract class IPause : MonoBehaviour
{
    public abstract void PauseGame();

    public abstract void UnpauseGame();

    public abstract void GoToMenu();

}

public class PausePresenter : IPause
{
    /// <summary>
    /// Audio sound
    /// </summary>
    [SerializeField]
    private AudioSource gameSound;

    [Scene]
    public string menuScene;

    [Inject]
    private LevelLoader levelLoader;

    [SerializeField]
    private FadeManager fadeManager;

    /// <summary>
    /// Callback for OnClick event on PauseScreen
    /// </summary>
    public override void PauseGame()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
        gameSound.mute = true;
    }

    /// <summary>
    /// Callback for OnClick event on PauseScreen
    /// </summary>
    public override void UnpauseGame()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        gameSound.mute = false;
    }
    public override void GoToMenu()
    {
        fadeManager.FadeScene();
        levelLoader.LoadLevel(menuScene);
    }
}
