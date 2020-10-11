using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class SummonSpawner
{
    List<GameObject> summons;

    float spawnDelta;

    public SummonSpawner(List<GameObject> summons, float spawnDelta)
    {
        this.summons = summons;
        this.spawnDelta = spawnDelta;
    }

    /// <summary>
    /// For test purposes
    /// </summary>
    public void SpawnOne()
    {
        var summon = UnityEngine.Object.Instantiate(summons[0],
                    UnityEngine.Random.onUnitSphere * spawnDelta,
                    Quaternion.identity,
                    null);

        summon.SetActive(true);
    }

    public void SpawnAll()
    {
        throw new NotImplementedException();
    }
}

public class PartsSpawner : MonoBehaviour
{
    public Action<Rocket> RocketSpawned;

    #region Parts and rocket

    [Header("Parts")]
    public GameObject rocketHolder;

    public List<GameObject> trash;

    public  List<GameObject> rocks;

    public Transform rocketSpawnPosition;

    private List<GameObject> parts;

    private GameObject rocketObj;

    private Rocket rocketComp;

    #endregion

    #region Spawn

    [Header("Spawn parameters")]
    public float spawnDelta = 10f;

    #endregion

    #region Summons

    [Header("Summons")]

    public List<GameObject> summons;

    private int summonCount;

    #endregion

    public void SpawnLevel()
    {
        SpawnRocket();

        SpawnTrash();

        SpawnRocks();

        SpawnAllParts();

        SpawnSummons();
    }

    /// <summary>
    /// Hides logic of spawning summons
    /// </summary>
    private void SpawnSummons()
    {
        var summonSpawner = new SummonSpawner(summons, spawnDelta);

        for (int i = 0; i < summonCount; i++)
        {
            summonSpawner.SpawnOne();
        }
    }

    public void SpawnTrash()
    {
        foreach (var element in trash)
        {
            Instantiate(element,
                        UnityEngine.Random.onUnitSphere * spawnDelta,
                        transform.rotation,
                        null);
        }
    }

    public void SpawnRocks()
    {
        foreach (var element in rocks)
        {
            Instantiate(element,
                        UnityEngine.Random.onUnitSphere * spawnDelta,
                        transform.rotation,
                        null);
        }
    }

    public void SpawnAllParts()
    {
        var allParts = parts.Concat(trueParts);

        foreach (var element in allParts)
        {
            Instantiate(element,
                        UnityEngine.Random.onUnitSphere * spawnDelta,
                        transform.rotation,
                        null);
        }
    }

    private void SpawnRocket()
    {
        rocketHolder.transform.position = rocketSpawnPosition.position;

        var rock = Instantiate(rocketObj,
                               rocketHolder.transform);

        rock.transform.localPosition = Vector3.zero;

        DeactivateChildrens(rock);
        rocketComp = rock.GetComponent<Rocket>();

        RocketSpawned?.Invoke(rocketComp);
    }

    private void DeactivateChildrens(GameObject go)
    {
        for (int j = 0; j < go.transform.childCount; j++)
        {
            go.transform.GetChild(j).gameObject.SetActive(false);
        }
    }

    public void LevelLoaded(Level level)
    {
        parts = level.modules;

        trueParts = level.trueModules;

        summonCount = level.astronautCount;

        rocketObj = level.rocket;

        SpawnLevel();
    }

    #region Obsolete   
    [Obsolete("Don't used")]
    private List<GameObject> trueParts;

    [SerializeField]
    [HideInInspector]
    private Material glowEffect;

    [SerializeField]
    [HideInInspector]
    private ClickManager manager;

    [Inject]
    [Obsolete("Use ZenAutoInjecter component instead")]
    private DiContainer _diContainer;

    [Obsolete("Planet already exist")]
    private GameObject planet;

    [Obsolete("Planet already exist")]
    public delegate void PlanetSpawned(Transform planetTransform);

    [Obsolete("Planet already exist on level")]
    public PlanetSpawned planetSpawned;

    /// <summary>
    /// Obsolete function for planet spawn
    /// </summary>
    [Obsolete("Planet should exist on level before actual gameplay")]
    private void SpawnPlanet()
    {
        var plan = _diContainer.InstantiatePrefab(planet, Vector3.zero, transform.rotation, null);

        var planComp = plan.GetComponent<Planet>();

        Transform camTargetPlanet = plan.transform;

        //Find right planet to look at
        foreach (Transform child in plan.transform)
        {
            if (child.tag == "Planet")
            {
                camTargetPlanet = child;
            }
            else if (child.tag == "Station")
            {
                SetRocketHolderPosition(child.transform);
            }
        }

        planetSpawned?.Invoke(camTargetPlanet);
    }
    /// <summary>
    /// Used to set rocket positions
    /// </summary>
    /// <param name="parentTransform"></param>
    [Obsolete("Planet already exist so set holder in Editor")]
    private void SetRocketHolderPosition(Transform parentTransform)
    {
        rocketHolder.transform.position = parentTransform.position;
    }
    #endregion
}
