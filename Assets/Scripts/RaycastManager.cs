using InputSamples.Gestures;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// This class hides logic of comparing layer 
/// with constants from T enum that represents
/// all possible object that you can collect
/// </summary>
/// <typeparam name="T">Type which inherits from Enum</typeparam>
public class LayerChecker<T> where T : Enum
{
    /// <summary>
    /// Name of layer that you need to compare with Enum values
    /// </summary>
    public string LayerName
    {
        get; private set;
    }

    /// <summary>
    /// All possible Value of Enum
    /// </summary>
    public Array ItemTypes
    {
        get; private set;
    }

    public LayerChecker(int layer)
    {
        LayerName = LayerMask.LayerToName(layer);

        ItemTypes = Enum.GetValues(typeof(T));
    }

    /// <summary>
    /// Returns name of Enum item
    /// </summary>
    /// <param name="item">
    /// Actual Item from Enum
    /// </param>
    /// <returns></returns>
    public string GetItemName(T item)
    {
        return Enum.GetName(typeof(T), item);
    }
}

/// <summary>
/// Uses for checking collision from player and other objects
/// </summary>
public class RaycastManager : MonoBehaviour
{
    [SerializeField]
    private GameObject selectParticles;

    [SerializeField]
    private Camera cam;

    [Inject]
    private GlobalInventory globalInventory;

    private AudioSource collisionSound;

    private void Awake()
    {
        cachedShipParts = new Dictionary<string, ShipPart>();

        collisionSound = GetComponent<AudioSource>();
    }

    public void CollisionWithObject(GameObject gameObj)
    {
        var layer = gameObj.layer;

        var layerChecker = new LayerChecker<ItemType>(layer);

        var itemLayer = layerChecker.LayerName;

        foreach (ItemType item in layerChecker.ItemTypes)
        {
            var name = layerChecker.GetItemName(item);

            if (name == itemLayer)
            {
                RightCollisionWithRightObjects(gameObj);

                globalInventory.PutItem(gameObj, item);
            }
        }
    }

    private void RightCollisionWithRightObjects(GameObject gameObj)
    {
        collisionSound.Play();

        var glow = Instantiate(selectParticles, gameObj.transform.position, gameObj.transform.rotation);

        Destroy(glow, 3);
    }

    #region Obsolete
    [SerializeField]
    private GestureController gestureController;

    [Header("Layer for raycasting")]
    [Layer]
    public int tapLayer;

    [Obsolete("No longer needed")]
    private Dictionary<string, ShipPart> cachedShipParts;

    [Obsolete("No longer needed")]
    private ShipPart lastShipPart;

    /// <summary>
    /// Getting ShipPart object and caching it
    /// </summary>
    /// <param name="gameObj"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Use this with
    /// </summary>
    /// <example>
    /// private void OnEnable()
    /// {
    ///     gestureController.Tapped += OnTappedHandler;
    /// } 
    /// private void OnDisable()
    /// {
    ///     gestureController.Tapped -= OnTappedHandler;
    /// }
    /// </example>
    /// <param name="input"></param>
    [Obsolete("This is no longer needed")]
    private void OnTappedHandler(TapInput input)
    {
        var ray = cam.ScreenPointToRay(input.PressPosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, 1 << tapLayer))
        {
            var gameObj = hit.transform.gameObject;

            ShipPartColiision(gameObj);
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

    /// <summary>
    /// Collision only with ShipPart
    /// </summary>
    /// <param name="gameObj"></param>
    private void ShipPartColiision(GameObject gameObj)
    {
        ShipPart shipPart = GetShipPart(gameObj);

        shipPart.ClickOnObject();

        if (shipPart.ClickCount == 0)
        {
            collisionSound.Play();

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

            var glow = Instantiate(selectParticles, gameObj.transform.position, gameObj.transform.rotation);

            Destroy(glow, 3);

            glow.transform.localPosition = Vector3.zero;
        }

        lastShipPart = shipPart;
    }

    [Obsolete("Effects removes automatically")]
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

    #endregion
}