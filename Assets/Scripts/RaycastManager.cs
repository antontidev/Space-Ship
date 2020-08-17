using InputSamples.Gestures;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RaycastManager : MonoBehaviour
{
    [SerializeField]
    private GameObject selectParticles;

    [SerializeField]
    private Camera cam;

    [Inject]
    private ModulesBridge modulesBridge;

    [SerializeField]
    private GestureController gestureController;

    [Header("Layer for raycasting")]
    [Layer]
    public int tapLayer;

    private AudioSource clickSound;

    private ShipPart lastShipPart;

    private Dictionary<string, ShipPart> cachedShipParts;

    private void Start()
    {
        cachedShipParts = new Dictionary<string, ShipPart>();

        clickSound = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        gestureController.Tapped += OnTappedHandler;
    }

    private void OnDisable()
    {
        gestureController.Tapped -= OnTappedHandler;
    }

    private void OnTappedHandler(TapInput input)
    {
        var ray = cam.ScreenPointToRay(input.PressPosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, 1 << tapLayer))
        {
            var gameObj = hit.transform.gameObject;

            ShipPart shipPart = GetShipPart(gameObj);

            shipPart.ClickOnObject();

            if (shipPart.ClickCount == 0)
            {
                clickSound.Play();
                RemoveGlow();
            }
            if (shipPart.ClickCount == 1)
            {
                if (lastShipPart != null)
                {
                    if (lastShipPart.gameObject.GetInstanceID() != gameObj.GetInstanceID())
                    {
                        RemoveGlow();
                    }
                }

                var glow = Instantiate(selectParticles, gameObj.transform);

                glow.transform.localPosition = Vector3.zero;
            }

            lastShipPart = shipPart;
        }
        else
        {
            RemoveGlow();
            if (lastShipPart != null)
            {
                lastShipPart.ClickCount = 0;
            }
        }
    }

    private void RemoveGlow()
    {
        if (lastShipPart != null)
        {
            foreach (Transform child in lastShipPart.transform)
            {
                child.gameObject.SetActive(false);
                Destroy(child.gameObject);
            }
        }
    }

    private ShipPart GetShipPart(GameObject gameObj)
    {
        if (cachedShipParts.ContainsKey(gameObj.name))
        {
            return cachedShipParts[gameObj.name];
        }
        else
        {
            var shipPart = gameObj.GetComponent<ShipPart>();

            return cachedShipParts[gameObj.name] = shipPart;
        }
    }
}