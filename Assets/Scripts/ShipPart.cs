using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]

public class ShipPart : MonoBehaviour
{
  void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
