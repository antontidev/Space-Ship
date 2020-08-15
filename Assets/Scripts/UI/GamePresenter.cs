using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

public abstract class IGamePresetner : MonoBehaviour
{
    public abstract void PauseGame();

    public abstract void UnpauseGame();

    public abstract void ShowEndScreen();
}

public class GamePresenter : IGamePresetner
{
    [SerializeField]
    private Timer timer;

    /// <summary>
    /// Reading actual timer value
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI timerText;

    /// <summary>
    /// Shows up when you press pause button
    /// </summary>
    [SerializeField]
    private GameObject pauseScreen;

    /// <summary>
    /// Planet text fields
    /// </summary>
    #region

    /// Bridge between planet and presenter
    [Inject]
    private PlanetBridge planetBridge;

    [SerializeField]
    private TextMeshProUGUI planetName;

    [SerializeField]
    private TextMeshProUGUI planetGravity;

    [SerializeField]
    private TextMeshProUGUI planetTemperature;

    [SerializeField]
    private TextMeshProUGUI planetAtmosphere;

    #endregion Planet Planet

    /// <summary>
    /// Modules UI
    /// </summary>
    #region

    [Inject]
    private ModulesBridge modulesBridge;

    [SerializeField]
    private CanvasMesh bottom;

    [SerializeField]
    private CanvasMesh middle;

    [SerializeField]
    private CanvasMesh top;

    #endregion

    [SerializeField]
    private GameObject endScreen;

    private void Start()
    {
        InitializeTimer();
        InitializePlanet();
        InitializeModules();
    }

    /// <summary>
    /// Bind modules to UI
    /// </summary>
    private void InitializeModules()
    {
        var observe = modulesBridge.modules.ObserveAdd();

        CanvasMesh canvasMesh;
        observe.Subscribe(x =>
        {
            // This code smells
            var pair = x.Value;
            switch (pair.Key)
            {
                case "Bottom":
                    canvasMesh = bottom;
                    break;

                case "Middle":
                    canvasMesh = middle;
                    break;

                default:
                    canvasMesh = top;
                    break;
            }
            // End of code that smells
            var module = pair.Value;

            var meshFilter = module.GetComponent<MeshFilter>();

            var mesh = meshFilter.mesh;

            canvasMesh.SubmitMesh(mesh);
        });
    }

    /// <summary>
    /// Subscribe planet to UI
    /// </summary>
    private void InitializePlanet()
    {
        planetBridge.atmosphere.Subscribe(x =>
        {
            planetAtmosphere.text = x.ToString();
        });

        planetBridge.temperature.Subscribe(x =>
        {
            planetTemperature.text = x.ToString();
        });

        planetBridge.gravity.Subscribe(x =>
        {
            planetGravity.text = x.ToString();
        });

        planetBridge.planetName.Subscribe(x =>
        {
            planetName.text = x.ToString();
        });
    }

    /// <summary>
    /// Subscribe UI to Timer
    /// </summary>
    private void InitializeTimer()
    {
        timer.roundedTimer.Where(x => x >= 0).Subscribe(x =>
        {
            timerText.text = x.ToString();
        });
    }

    /// <summary>
    /// Callback for OnClick event on PauseScreen
    /// </summary>
    public override void PauseGame()
    {
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
    }

    /// <summary>
    /// Callback for OnClick event on PauseScreen
    /// </summary>
    public override void UnpauseGame()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
    }

    public override void ShowEndScreen()
    {
        endScreen.SetActive(true);
    }
}