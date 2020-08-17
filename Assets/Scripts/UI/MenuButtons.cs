using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [Scene]
    public string gameScene;

    [SerializeField]
    private GameObject settingsMenu;

    [SerializeField]
    private GameObject storeMenu;

    [SerializeField]
    private GameObject mainMenu;

    public TMP_Dropdown localeSelector;

    private IEnumerator Start()
    {
        yield return LocalizationSettings.InitializationOperation;

        // Generate list of available Locales
        var options = new List<TMP_Dropdown.OptionData>();
        int selected = 0;
        for (int i = 0; i < LocalizationSettings.AvailableLocales.Locales.Count; ++i)
        {
            var locale = LocalizationSettings.AvailableLocales.Locales[i];
            if (LocalizationSettings.SelectedLocale == locale)
                selected = i;
            options.Add(new TMP_Dropdown.OptionData(locale.name));
        }
        localeSelector.options = options;

        localeSelector.value = selected;
        localeSelector.onValueChanged.AddListener(LocaleSelected);
    }

    static void LocaleSelected(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        settingsMenu.SetActive(false);

        //TODO
        //storeMenu.SetActive(false);

        mainMenu.SetActive(true);
    }


    public void ToSettings()
    {
        mainMenu.SetActive(false);

        settingsMenu.SetActive(true);
    }

    public void ToStore()
    {
        //TODO
    }
}