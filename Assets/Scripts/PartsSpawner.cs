using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> shipPrefab;

    void Start()
    {
        shipPrefab.ForEach((GameObject obj) =>
        {
            Instantiate(obj, Random.onUnitSphere * 1, transform.rotation);
        });
    }

    public void SubmitList(List<GameObject> list)
    {
        shipPrefab = list;
    }
}
