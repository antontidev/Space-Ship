using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PartsSpawner : MonoBehaviour
{
    public Action<Rocket> RocketSpawned;

    private List<GameObject> parts;

    private List<GameObject> trueParts;

    private GameObject rocketObj;

    private Rocket rocketComp;

    public Transform rocketSpawnPosition;

    [Header("Rocket holder gameObject")]
    [SerializeField]
    private GameObject rocketHolder;

    [Inject]
    [Obsolete("Use ZenAutoInjecter instead")]
    private DiContainer _diContainer;

    [Header("All trash objects")]
    [SerializeField]
    private List<GameObject> trash;

    [Header("Rocks objects")]
    [SerializeField]
    private List<GameObject> rocks;

    [SerializeField]
    private Material glowEffect;

    [SerializeField]
    private ClickManager manager;

    public void SpawnLevel()
    {
        SpawnRocket();

        SpawnTrash();

        SpawnRocks();

        SpawnAllParts();
    }

    public void SpawnTrash()
    {
        foreach (var element in trash)
        {
            Instantiate(element,
                        UnityEngine.Random.onUnitSphere * 5,
                        transform.rotation,
                        null);
        }
    }

    public void SpawnRocks()
    {
        foreach (var element in rocks)
        {
            Instantiate(element,
                        UnityEngine.Random.onUnitSphere * 5,
                        transform.rotation,
                        null);
        }
    }

    public void SpawnAllParts()
    {
        var allParts = parts.Concat(trueParts);

        foreach (var el in allParts)
        {
            var part = Instantiate(el,
                                   UnityEngine.Random.onUnitSphere * 5,
                                   transform.rotation,
                                   null);

            //var shipPart = part.GetComponent<ShipPart>();

            //shipPart.onSecondClick += rocketComp.Handle;
            //shipPart.onSecondClick += manager.HandleClick;
        }
    }

    private void SpawnRocket()
    {
        var rock = Instantiate(rocketObj,
                               rocketHolder.transform.position,
                               transform.rotation);

        rock.transform.localPosition = Vector3.zero;

        DeactivateChildrens(rock);
        rocketComp = rock.GetComponent<Rocket>();

        rocketComp.SubmitTrueParts(trueParts);

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

        rocketObj = level.rocket;

        SpawnLevel();
    }

    #region Obsolete
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
