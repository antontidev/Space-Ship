using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]

public class ShipPart : MonoBehaviour
{

    void Start()
    { 
    }

    void Update()
    {
        
    }

    void OnMouseClick()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }
}
