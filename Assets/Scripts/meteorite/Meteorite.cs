using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Meteorite : MonoBehaviour
{
    public GameObject collisionSystem;

    public GameObject moveSystem;

    /// <summary>
    /// Used to indicate that happened not destroyable collision
    /// </summary>
    public Action<GameObject, Transform> OnPlainCollision;

    /// <summary>
    /// Collision with object that killable
    /// </summary>
    public Action<int, Transform> OnCertainCollision;

    private List<int> collisionLayers;

    [Inject]
    private Gravity planet;

    [Inject]
    private KillableObjects killableObjectsLayers;

    private void Start()
    {
        collisionLayers = killableObjectsLayers.enumLayerList;

        var planetTransform = planet.transform;

        transform.LookAt(planetTransform);
    }

    void OnCollisionEnter(Collision collision)
    {
        OnCollisionEffect();

        var collisionObject = collision.gameObject;

        var collisionLayer = collisionObject.layer;

        if (collisionLayers.Contains(collisionLayer))
        {
            OnCertainCollision?.Invoke(collisionLayer, collisionObject.transform);
        }

        OnPlainCollision?.Invoke(gameObject, collisionObject.transform);
    }

    void OnCollisionEffect()
    {
        collisionSystem.SetActive(true);

        moveSystem.SetActive(false);
    }
}
