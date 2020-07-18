using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject shipPrefab;
    [SerializeField]
    //private List<GameObject> shipPrefab;

    void Start()
    {
        //shipPrefab.ForEach((GameObject obj) =>
        //{
            for(int i = 0; i < 3; i++)
        {
            Instantiate(shipPrefab, Random.onUnitSphere * 10, transform.rotation);
        }
            
       // });
    }
}
