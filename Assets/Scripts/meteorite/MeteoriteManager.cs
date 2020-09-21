using System;
using UniRx;
using UnityEngine;

public class MeteoriteManager : MonoBehaviour
{
    [Header("Meteorite prefab")]
    public GameObject meteoritePrefab;

    public float spawnDelta = 5f;

    public float spawnDistance = 5f;

    private IDisposable _update;

    private void Start()
    {
        _update = Observable.Interval(TimeSpan.FromSeconds(spawnDelta)).Subscribe(x =>
        {
            SpawnMeteorite();
        });
    }

    private void OnDestroy()
    {
        _update.Dispose();
    }

    void SpawnMeteorite()
    {
        var spawnPosition = UnityEngine.Random.onUnitSphere * spawnDistance;

        Instantiate(meteoritePrefab,
                    spawnPosition,
                    transform.rotation,
                    null);
    }
}
