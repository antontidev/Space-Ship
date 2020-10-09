using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SummonManager : MonoBehaviour
{
    public GameObject player;

    public float pointTowardsSpeed = 10f;

    public float minPlayerDistance = 1f;

    /// <summary>
    /// Holds all coroutines that moves the astronauts to the player
    /// </summary>
    private Dictionary<int, Coroutine> movers;

    [Inject]
    private AstronautInventory astronautInventory;

    private void Start()
    {
        movers = new Dictionary<int, Coroutine>();
    }

    private void Update()
    {
        var astronauts = astronautInventory.inventory;

        PointTowardsPlayer(astronauts);

        FaceTowardsPlayer(astronauts);
    }

    /// <summary>
    /// Moves astronauts to the player
    /// </summary>
    /// <param name="astronauts"></param>
    private void PointTowardsPlayer(List<GameObject> astronauts)
    {
        var playerPosition = player.transform.position;

        foreach (var astronaut in astronauts)
        {
            var astronautTransform = astronaut.transform;

            var astronautID = astronaut.GetInstanceID();

            if (!movers.ContainsKey(astronautID) && 
                Vector3.Distance(playerPosition, 
                                 astronautTransform.position) > minPlayerDistance)
            {
                var mover = StartCoroutine(MoveToPlayer(astronautTransform));

                movers.Add(astronautID, mover);
            }
        }
    }

    /// <summary>
    /// Facing astronauts to the player direction of move
    /// </summary>
    /// <param name="astronauts"></param>
    private void FaceTowardsPlayer(List<GameObject> astronauts)
    {
        foreach (var astronaut in astronauts)
        {
            astronaut.transform.LookAt(player.transform, player.transform.up);
        }
    }

    /// <summary>
    /// Coroutines that moves astronauts to the player if distance
    /// between them is bigger than minPlayerDistance
    /// </summary>
    /// <param name="astronautTransform">
    /// Transform of astronaut that should be moved to the player
    /// </param>
    /// <returns></returns>
    private IEnumerator MoveToPlayer(Transform astronautTransform)
    {
        var playerTransform = player.transform;

        while (Vector3.Distance(playerTransform.position,
                                astronautTransform.position) > minPlayerDistance)
        {
            var newPosition = Vector3.MoveTowards(astronautTransform.position,
                                playerTransform.position, Time.deltaTime);

            astronautTransform.position = newPosition;

            yield return null;
        }

        var astronautID = astronautTransform.gameObject.GetInstanceID();

        movers.Remove(astronautID);
    }
}
