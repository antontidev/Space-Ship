using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> parts;

    public IEnumerator Spawn(List<GameObject> list)
    {
        foreach (var el in list)
        {
            var part = Instantiate(el, Random.onUnitSphere * 1, transform.rotation);
            //part.GetComponent<ShipPart>().onClick += PartClicked;
            yield return null;
        }
    }



    public IEnumerator Spawn()
    {
        foreach (var el in parts)
        {
            var part = Instantiate(el, Random.onUnitSphere * 1, transform.rotation);
            //part.GetComponent<ShipPart>().onClick += PartClicked;
            yield return null;
        }
        foreach (var el in trueModules)
        {
            var part = Instantiate(el, Random.onUnitSphere * 1, transform.rotation);
            //part.GetComponent<ShipPart>().onClick += PartClicked;
            yield return null;
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
