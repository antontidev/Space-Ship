using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsSpawner : MonoBehaviour
{
	public delegate void ObjectSpawned(List<GameObject> list);

	public ObjectSpawned OnObjectSpawned;

	[SerializeField]
	private List<GameObject> parts;

	[SerializeField]
	public List<GameObject> trash;

	private Rocket rocket;

	[SerializeField]
	public ClickManager manager;

	public IEnumerator Spawn(List<GameObject> list)
	{
		foreach (var el in list)
		{
			var part = Instantiate(el, Random.onUnitSphere * 5, transform.rotation);


			var shippart = part.GetComponent<ShipPart>();

			shippart.onClick += manager.HandleClick;
			shippart.onClick += rocket.Handle;
			yield return null;
		}
		OnObjectSpawned?.Invoke(list);
	}

	public IEnumerator SpawnTrash()
    {
		foreach (var element in trash)
        {
			var trashPart = Instantiate(element, Random.onUnitSphere * 5, transform.rotation);

			yield return null;
        }
    }

	public IEnumerator Spawn()
	{

		foreach (var el in parts)
		{
			var part = Instantiate(el, Random.onUnitSphere * 5, transform.rotation);

			var shipPart = part.GetComponent<ShipPart>();
			shipPart.onClick += rocket.Handle;
			shipPart.onClick += manager.HandleClick;

			yield return null;
		}
		OnObjectSpawned?.Invoke(parts);
	}


	public GameObject SpawnPlanet(GameObject planet)
	{
		return Instantiate(planet, Vector3.zero, transform.rotation);
	}

	public GameObject SpawnRocket(GameObject rocket, Transform position)
	{
		var rocketObj = Instantiate(rocket, position.position, transform.rotation);
		DeactivateChildrens(rocketObj);
		this.rocket = rocketObj.GetComponent<Rocket>();

		return rocketObj;
	}

	private void DeactivateChildrens(GameObject go)
	{
		for (int j = 0; j < go.transform.childCount; j++)
		{
			go.transform.GetChild(j).gameObject.SetActive(false);
		}
	}

	public void SubmitList(List<GameObject> list)
	{
		parts = list;

		StartCoroutine(Spawn());
	}
}
