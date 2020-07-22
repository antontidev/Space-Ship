using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulePerk : MonoBehaviour
{
    [SerializeField]
    public float gravity;
    [SerializeField]
    public float oxygen;
    [SerializeField]
    public float temperature;

    private void Start()
    {
        


    }

    float ReturnActive()
    {
        switch (gameObject.tag)
        {
            case "Middle":
                return oxygen;
                break;
            case "Top":
                return temperature;
                break;
            default:
                return gravity;
                break;
        }
    }
}
