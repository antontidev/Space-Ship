using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipPart : MonoBehaviour
{
    private Transform defaultPosition;
    public delegate void OnClick(GameObject obj);
    public OnClick onClick;

    public void Awake()
    {
        defaultPosition = gameObject.transform;
    }

    private void OnMouseEnter()
    {
        
    }

    void OnMouseDown()
    {
        onClick?.Invoke(gameObject);
    }
}
