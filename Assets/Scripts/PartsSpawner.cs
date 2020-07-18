using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private List<GameObject> shipPrefab;

    void Start()
    {
        shipPrefab.ForEach((GameObject obj) =>
        {
              Instantiate(obj, Random.onUnitSphere * 1, transform.rotation);
        });
        for(int i = 0; i < 3; i++)
        {
           
            Instantiate(prefab, Random.onUnitSphere, transform.rotation);
        }
    }

    public void SubmitList(List<GameObject> list)
    {
        shipPrefab = list;
    }
}
