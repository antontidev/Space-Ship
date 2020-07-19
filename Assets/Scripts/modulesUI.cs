using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class modulesUI : MonoBehaviour
{
    [SerializeField]
    List<TextMeshProUGUI> lights;

    [SerializeField]
    GameManagerScript gameManager;

    private Rocket rocket;

    void Start()
    {
        rocket = gameManager.rocket;
        rocket.OnReadyModuleChange += UpdateCanvas;
    }

    public void UpdateCanvas()
    {
        rocket = gameManager.rocket;
        foreach (KeyValuePair<string, bool> entry in rocket.ready)
        {
            Color color;
            if (entry.Value == true)
            {
                color = Color.green;
            }
            else
            {
                color = Color.red;
            }
            switch (entry.Key)
            {
                case "Top":
                    GameObject.Find("Canvas/Image1/Bottom").GetComponent<TextMeshProUGUI>().color = color; 
                    break;
                case "Middle":
                    GameObject.Find("Canvas/Image1/Middle").GetComponent<TextMeshProUGUI>().color = color;
                    break;
                case "Bottom":
                    GameObject.Find("Canvas/Image1/Top").GetComponent<TextMeshProUGUI>().color = color;
                    break;
            }
        }
    }
}
