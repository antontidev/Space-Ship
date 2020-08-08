using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PartsSpawner : MonoBehaviour
{
    public delegate void PlanetSpawned(Transform planetTransform);

    public PlanetSpawned planetSpawned;

    private List<GameObject> parts;

    private List<GameObject> trueParts;

    private GameObject rocketObj;

    private GameObject planet;

    private Rocket rocketComp;

    private Transform rocketSpawnPosition;

    [SerializeField]
    public List<GameObject> trash;

    [SerializeField]
    public Material glowEffect;

    [SerializeField]
    public ClickManager manager;

    public void SpawnLevel()
    {
        SpawnPlanet();

        SpawnRocket();

        SpawnTrash();

        SpawnAllParts();
    }

    public void SpawnTrash()
    {
        foreach (var element in trash)
        {
            var trashPart = Instantiate(element, Random.onUnitSphere * 5, transform.rotation);

        }
    }

    public void SpawnAllParts()
    {
        var allParts = parts.Concat(trueParts);

        foreach (var el in allParts)
        {
            var part = Instantiate(el, Random.onUnitSphere * 5, transform.rotation);

            var shipPart = part.GetComponent<ShipPart>();

            //shipPart.SubmitGlowMaterial(glowEffect);
            shipPart.onClick += rocketComp.Handle;
            shipPart.onClick += manager.HandleClick;
        }
    }


    private void SpawnPlanet()
    {
        var plan = Instantiate(planet, Vector3.zero, transform.rotation);

        var planComp = plan.GetComponent<Planet>();

        Transform camTargetPlanet = plan.transform;

        //Find right planet to look at
        foreach (Transform child in plan.transform)
        {
            if (child.tag == "Planet")
            {
                camTargetPlanet = child;
            }
        }

        rocketSpawnPosition = planComp.spawnRocketPostition;

        planetSpawned?.Invoke(camTargetPlanet);
    }

    private void SpawnRocket()
    {
        var rock = Instantiate(rocketObj, rocketSpawnPosition.position, transform.rotation);
        DeactivateChildrens(rock);
        rocketComp = rock.GetComponent<Rocket>();
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

        planet = level.planet;

        SpawnLevel();
    }
}
