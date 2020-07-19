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
        
    }

    void Update()
    {
        rocket = gameManager.rocket;
        foreach (KeyValuePair<string, bool> entry in rocket.ready)
        {   if (entry.Value == true)
            {
                switch (entry.Key)
                {
                    case "Top":
                        GameObject.Find("Canvas/Image1/Bottom").GetComponent<TextMeshProUGUI>().color = new Color32(10, 50, 10, 255);
                        break;
                    case "Middle":
                        GameObject.Find("Canvas/Image1/Middle").GetComponent<TextMeshProUGUI>().color = new Color32(10, 50, 10, 255);
                        break;
                    case "Bottom":
                        GameObject.Find("Canvas/Image1/Top").GetComponent<TextMeshProUGUI>().color = new Color32(10, 50, 10, 255);
                        break;
                }
            }
        }
    }
}
