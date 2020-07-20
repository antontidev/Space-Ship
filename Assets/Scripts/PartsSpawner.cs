using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> parts;

    private Rocket rocket;

    [SerializeField]
    public ClickManager manager;

    [SerializeField]
    public GameObject parent;

    public IEnumerator Spawn(List<GameObject> list)
    {
        foreach (var el in list)
        {
            var part = Instantiate(el, Random.onUnitSphere * 5, transform.rotation);

            part.transform.parent = parent.transform;

            var shippart = part.GetComponent<ShipPart>();

            shippart.onClick += manager.HandleClick;
            shippart.onClick += rocket.Handle;
            yield return null;
        }
    }

    public IEnumerator Spawn()
    {
        foreach (var el in parts)
        {
            var part = Instantiate(el, Random.onUnitSphere * 5, transform.rotation);


            part.transform.parent = parent.transform;

            var shipPart = part.GetComponent<ShipPart>();
            shipPart.onClick += rocket.Handle;
            shipPart.onClick += manager.HandleClick;

            yield return null;
        }
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

        this.rocket.manager = manager;
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

    private void PartClicked(GameObject obj)
    {

    }
}
