using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]

public class ShipPart : MonoBehaviour
{
    public delegate void OnClick(GameObject obj);
    public OnClick onClick;

    void OnMouseDown()
    {
        onClick?.Invoke(gameObject);
    }
}
