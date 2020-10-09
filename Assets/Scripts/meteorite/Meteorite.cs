using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    public GameObject collisionSystem;

    public GameObject moveSystem;

    private List<int> collisionLayers;

    private void Start()
    {
        var playerLayer = LayerMask.NameToLayer("Player");

        var summonLayer = LayerMask.NameToLayer("Summon");

        collisionLayers = new List<int>(){ playerLayer,
                                           summonLayer };
    }

    void OnCollisionEnter(Collision collision)
    {
        OnCollisionEffect();

        var collisionLayer = collision.gameObject.layer;

        foreach (var element in collisionLayers)
        {
            if (collisionLayer == element)
            {

            }
        }

        Destroy(gameObject, 2);
    }

    void OnCollisionEffect()
    {
        collisionSystem.SetActive(true);

        moveSystem.SetActive(false);
    }
}
