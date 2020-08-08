using InputSamples.Gestures;
using System.Collections.Generic;
using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    [SerializeField]
    public Camera cam;

    [SerializeField]
    public GestureController gestureController;

    [Header("Layer for raycasting")]
    [Layer]
    public int tapLayer;

    private Dictionary<string, ShipPart> cachedShipParts;

    private void Start()
    {
        cachedShipParts = new Dictionary<string, ShipPart>();
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

            shipPart.ClickOnObject(gameObj);
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
