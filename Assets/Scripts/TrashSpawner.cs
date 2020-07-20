using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> parts;

    [SerializeField]
    public GameObject parent;

    public IEnumerator Spawn(List<GameObject> list)
    {
        foreach (var el in list)
        {
            var part = Instantiate(el, Random.onUnitSphere * 5, transform.rotation);

            part.transform.parent = parent.transform;

            //part.GetComponent<ShipPart>().onClick += PartClicked;
            yield return null;
        }
    }

    public IEnumerator Spawn()
    {
        foreach (var el in parts)
        {
            var part = Instantiate(el, Random.onUnitSphere * 5, transform.rotation);

            part.transform.parent = parent.transform;
            //part.GetComponent<ShipPart>().onClick += PartClicked;
            yield return null;
        }
    }
}