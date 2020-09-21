﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    /// <summary>
    /// Used for object colliding
    /// </summary>
    [SerializeField]
    private RaycastManager raycastManager;

    private void OnTriggerEnter(Collider other)
    {
        var gameObj = other.gameObject;

        raycastManager.CollisionWithObject(gameObj);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var gameObj = collision.gameObject;

        raycastManager.CollisionWithObject(gameObj);
    }
}
