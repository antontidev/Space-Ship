using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public abstract class IGamePresetner : MonoBehaviour
{
    public abstract void PauseGame();

    public abstract void UnpauseGame();

    public abstract void ShowEndScreen();

    public abstract void GoToMenu();
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
    /// Audio sound
    /// </summary>
    [SerializeField]
    private AudioSource gameSound;

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
    /// Modules Image
    /// </summary>
    #region

    [Inject]
    private ModulesBridge modulesBridge;

    [Inject]
    private ActivePartManager activePartManager;

    [SerializeField]
    private Image bottom;

    [SerializeField]
    private Image middle;

    [SerializeField]
    private Image top;

    private Dictionary<string, Image> moduleMap;

    #endregion

    /// <summary>
    /// Modules Holder
    /// </summary>
    #region

    [SerializeField]
    private Image bottomHolder;

    [SerializeField]
    private Image middleHolder;

    [SerializeField]
    private Image topHolder;

    private Dictionary<string, Image> holderMap;

    #endregion

    [SerializeField]
    private GameObject endScreen;

    [Scene]
    public string menuScene;

    private void Awake()
    {
        holderMap = new Dictionary<string, Image>();

        holderMap.Add(bottomHolder.tag, bottomHolder);
        holderMap.Add(middleHolder.tag, middleHolder);
        holderMap.Add(topHolder.tag, topHolder);

        moduleMap = new Dictionary<string, Image>();

        moduleMap.Add(bottom.tag, bottom);
        moduleMap.Add(middle.tag, middle);
        moduleMap.Add(top.tag, top);
    }

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

        var observeHolderReplace = activePartManager.ready.ObserveReplace();

        observeHolderReplace.Subscribe(replaceEvent =>
        {
            var level = replaceEvent.Key;

            var image = holderMap[level];

            ChangeColor(image, replaceEvent.NewValue);
        });

        var observeModuleReplace = activePartManager.activeModules.ObserveReplace();

        observeModuleReplace.Where(x => x.NewValue != null).Subscribe(replaceEvent =>
        {
            var level = replaceEvent.Key;

            var image = moduleMap[level];

            var shipPart = replaceEvent.NewValue;

            image.sprite = shipPart.sprite;

            image.color = Color.white;
        });
    }

    private void ChangeColor(Image image, bool state)
    {
        var color = state ? Color.green : Color.red;

        image.color = color;
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
        gameSound.mute = true;
    }

    /// <summary>
    /// Callback for OnClick event on PauseScreen
    /// </summary>
    public override void UnpauseGame()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
        gameSound.mute = false;
    }

    public override void ShowEndScreen()
    {
        endScreen.SetActive(true);
    }

    public override void GoToMenu()
    {
        SceneManager.LoadScene(menuScene);
    }
}