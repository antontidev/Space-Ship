using FairyGUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipPart : MonoBehaviour
{
    private Transform defaultPosition;
    public delegate void OnClick(GameObject obj);

    public OnClick onClick;
    private UIPanel panel;

    public void Awake()
    {
        defaultPosition = gameObject.transform;

        panel = GetComponent<UIPanel>();
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
