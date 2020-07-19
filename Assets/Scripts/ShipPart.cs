using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPart : MonoBehaviour
{   
        [SerializeField]
        private GameObject SpaceShip;
        [SerializeField]



    void OnMouseDown()
    {
        this.transform.position = GameObject.FindGameObjectWithTag("ModuleOne").transform.position;
        this.transform.parent = SpaceShip.transform;
    }
}
