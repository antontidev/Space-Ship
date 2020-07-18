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
            Instantiate(obj, Random.onUnitSphere * 10, transform.rotation);
        });
    }
}
