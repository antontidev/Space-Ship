using System;
using UnityEngine;

[Obsolete("Use perk base class and inherit ItemPart class")]
public class ModulePerk : MonoBehaviour
{
    [SerializeField]
    public float gravity;
    [SerializeField]
    public float oxygen;
    [SerializeField]
    public float temperature;

    float ReturnActive()
    {
        switch (gameObject.tag)
        {
            case "Middle":
                return oxygen;
            case "Top":
                return temperature;
            default:
                return gravity;
        }
    }
}
