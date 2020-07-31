using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class PhysObj : MonoBehaviour
{
    Gravity planet;

    void Start()
    {
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<Gravity>();
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        planet.Attract(transform);
    }
}
