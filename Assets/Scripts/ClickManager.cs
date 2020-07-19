using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField]
    public GameManagerScript gameManager;


    public void HandleClick(GameObject gameObject)
    {
        Debug.Log("Hi");
    }
}
