using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]

public class ShipPart : MonoBehaviour
{   
    public delegate void OnClick(GameObject obj);
    public OnClick onClick;

    private void Start()
    {
    }

    private void OnMouseEnter()
    {
        
    }

    void OnMouseDown()
    {
        onClick?.Invoke(gameObject);
        //if (ship.modules < 3)
        //{
        //    ship.modules += 1;
        //    this.transform.parent = SpaceShipPrefab.transform;

        //    switch (ship.modules)
        //    { case 1:
        //        this.transform.position = GameObject.FindGameObjectWithTag("ModuleOne").transform.position;
        //            this.transform.rotation = GameObject.FindGameObjectWithTag("ModuleOne").transform.rotation;
        //            break;
        //      case 2:
        //        this.transform.position = GameObject.FindGameObjectWithTag("ModuleTwo").transform.position;
        //            this.transform.rotation = GameObject.FindGameObjectWithTag("ModuleTwo").transform.rotation;
        //            break;
        //      case 3:
        //        this.transform.position = GameObject.FindGameObjectWithTag("ModuleThree").transform.position;
        //            this.transform.rotation = GameObject.FindGameObjectWithTag("ModuleThree").transform.rotation;
        //            break;
        //    }

        //    GetComponent<Rigidbody>().useGravity = false;
        //    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        //    GetComponent<PhysObj>().enabled = false;

        //}
    }
}
