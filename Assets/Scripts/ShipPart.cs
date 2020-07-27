using FairyGUI;
using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipPart : MonoBehaviour
{
    private Transform defaultPosition;
    public delegate void OnClick(GameObject obj);

    public OnClick onClick;
    [AutoProperty]
    private UIPanel panel;

    public void Awake()
    {
        defaultPosition = gameObject.transform;
    }

    private void OnMouseEnter()
    {
        
    }

    private void OnMouseExit()
    {
        
    }

    void OnMouseDown()
    {
        onClick?.Invoke(gameObject);
    }
}
