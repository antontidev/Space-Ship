using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class MeteoritePool
{
    public Queue<KeyValuePair<GameObject, GameObject>> pool;

    public MeteoritePool(GameObject meteorite,
                         GameObject fallEffect)
    {
        pool = new Queue<KeyValuePair<GameObject, GameObject>>();

    }
}

public class MeteoriteManager : MonoBehaviour
{
    [Layer]
    public int summonLayer;

    [Inject]
    private Gravity planet;

    [SerializeField]
    private GameObject fallMeteoriteEffect;

    [SerializeField]
    private float deleteMeteoriteDelay = 2f;

    [SerializeField]
    private float deleteMeteoriteFallEffectDelay = 0.5f;

    [Header("Meteorite prefab")]
    [SerializeField]
    private GameObject meteoritePrefab;

    [SerializeField]
    private float spawnDelta = 5f;

    [SerializeField]
    private float spawnDistance = 5f;

    private IDisposable _update;

    private Dictionary<int, GameObject> meteorites;

    private void Start()
    {
        meteorites = new Dictionary<int, GameObject>();

        _update = Observable.Interval(TimeSpan.FromSeconds(spawnDelta)).Subscribe(x =>
        {
            SpawnMeteorite();
        });
    }

    private void OnDestroy()
    {
        _update.Dispose();
    }

    /// <summary>
    /// Spawns meteorite
    /// </summary>
    void SpawnMeteorite()
    {
        var spawnPosition = UnityEngine.Random.onUnitSphere * spawnDistance;

        var newMeteorite = Instantiate(meteoritePrefab,
                    spawnPosition,
                    transform.rotation,
                    null);

        var meteoriteComponent = newMeteorite.GetComponent<Meteorite>();

        meteoriteComponent.OnPlainCollision += OnMeteoriteCollision;

        meteoriteComponent.OnCertainCollision += OnMeteoriteCertainCollision;

        var meteoriteInstanceID = newMeteorite.GetInstanceID();

        var meteoriteFallRegion = SpawnMeteoriteRegionFall(newMeteorite.transform, spawnPosition);

        meteorites.Add(meteoriteInstanceID, meteoriteFallRegion);

    }

    /// <summary>
    /// Hides logic of spawn region effect
    /// </summary>
    /// <param name="meteoritePosition">
    /// Meteorite spawn position
    /// </param>
    /// <returns></returns>
    private GameObject SpawnMeteoriteRegionFall(Transform meteorite, Vector3 meteoritePosition)
    {
        var direction = planet.transform.position - meteoritePosition;

        var ray = new Ray(meteoritePosition, direction);

        RaycastHit raycastHit;

        Physics.Raycast(ray, out raycastHit);
        
        var hitPoint = raycastHit.point;

        var fallEffectRotation = Quaternion.LookRotation(meteorite.forward, direction);

        var meteoriteFallEffect = Instantiate(fallMeteoriteEffect,
                                              hitPoint,
                                              fallEffectRotation,
                                              null);


        return meteoriteFallEffect;
    }

    private void OnMeteoriteCertainCollision(int layer, Transform obj)
    {
        if (layer == summonLayer)
        {
            var summon = obj.gameObject;

            var stateController = summon.GetComponent<SummonStateController>();

            stateController.ChangeState(SummonState.Dead);
        }
    }

    /// <summary>
    /// Deletes meteorite and fall region effect
    /// </summary>
    /// <param name="meteorite"></param>
    /// <param name="_"></param>
    private void OnMeteoriteCollision(GameObject meteorite, Transform _)
    {
        var meteoriteInstanceID = meteorite.GetInstanceID();

        var fallRegionObject = meteorites[meteoriteInstanceID];

        Destroy(meteorite, deleteMeteoriteDelay);

        Destroy(fallRegionObject, deleteMeteoriteFallEffectDelay);
    }
}
