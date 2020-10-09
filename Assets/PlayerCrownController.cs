using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class PlayerCrownController : MonoBehaviour
{
    public Animator crown;

    [Inject]
    private AstronautInventory astronautInventory;

    void Start()
    {
        astronautInventory.astronautCount.Subscribe(x => 
        {
            var band = x > 0;

            var crownObject = crown.gameObject;

            crownObject.SetActive(band);

            crown.SetBool("Band", band);
        });
    }
}
