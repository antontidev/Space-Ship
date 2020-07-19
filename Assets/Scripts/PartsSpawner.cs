using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> parts;

    [SerializeField]
    public Rocket rocket;

    public IEnumerator Spawn(List<GameObject> list)
    {
        foreach (var el in list)
        {
            var part = Instantiate(el, Random.onUnitSphere * 5, transform.rotation);
            var shippart = part.GetComponent<ShipPart>();
            shippart.onClick += rocket.Handle;
            yield return null;
        }
    }

    public IEnumerator Spawn()
    {
        foreach (var el in parts)
        {
            var part = Instantiate(el, Random.onUnitSphere * 5, transform.rotation);
            part.GetComponent<ShipPart>().onClick += rocket.Handle;
            yield return null;
        }
    }


    public GameObject SpawnPlanet(GameObject planet)
    {
        return Instantiate(planet, Vector3.zero, transform.rotation);
    }

    public void SpawnRocket(GameObject rocket, Transform position)
    {
        var rocketObj = Instantiate(rocket, position.position, transform.rotation);
        this.rocket = rocketObj.GetComponent<Rocket>();
    }

    public void SubmitList(List<GameObject> list)
    {
        parts = list;

        StartCoroutine(Spawn());
    }

    private void PartClicked(GameObject obj)
    {

    }
}
