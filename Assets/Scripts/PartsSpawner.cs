using InputSamples.Gestures;
using System.Collections;
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

	private Transform planetTransform;

	[SerializeField]
	public List<GameObject> trash;

	[SerializeField]
	public Material glowEffect;

	[SerializeField]
	public ClickManager manager;

	public IEnumerator SpawnLevel()
    {
		SpawnPlanet();

		SpawnRocket();

		var trash = StartCoroutine(SpawnTrash());

		var parts = StartCoroutine(SpawnAllParts());

		yield return trash;
		yield return parts;
    }

	public IEnumerator SpawnTrash()
    {
		foreach (var element in trash)
        {
			var trashPart = Instantiate(element, Random.onUnitSphere * 5, transform.rotation);

			yield return null;
        }
    }

	public IEnumerator SpawnAllParts()
	{
		var allParts = parts.Concat(trueParts);

		foreach (var el in allParts)
		{
			var part = Instantiate(el, Random.onUnitSphere * 5, transform.rotation);

			var shipPart = part.GetComponent<ShipPart>();

			//shipPart.SubmitGlowMaterial(glowEffect);
			shipPart.onClick += rocketComp.Handle;
			shipPart.onClick += manager.HandleClick;

			yield return null;
		}
	}


	private void SpawnPlanet()
	{
		var plan = Instantiate(planet, Vector3.zero, transform.rotation);

		planetTransform = plan.transform;

		planetSpawned?.Invoke(planetTransform);
	}

	private void SpawnRocket()
	{
		var rock = Instantiate(rocketObj, planetTransform.position, transform.rotation);
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

		StartCoroutine(SpawnLevel());
	}
}
