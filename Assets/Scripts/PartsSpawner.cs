using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsSpawner : MonoBehaviour
{
    [SerializeField]
        private GameObject shipPrefab;
    [SerializeField]

    void Start()
    {
        
        for (int i = 1; i < 10; i++)
        {
        Instantiate(shipPrefab, Random.onUnitSphere * 10, transform.rotation);
        }       
    }
}
