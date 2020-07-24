using InputSamples.Gestures;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

            ShipPart shipPart;
            if (cachedShipParts.ContainsKey(gameObj.name))
            {
                shipPart = cachedShipParts[gameObj.name];
            }
            else
            {
                shipPart = hit.transform.gameObject.GetComponent<ShipPart>();

                cachedShipParts[gameObj.name] = shipPart;
            }

            shipPart.onClick?.Invoke(gameObj);
        }
    }
}
