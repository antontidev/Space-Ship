using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{

    public float grav = -10f;
    
    public void Attract(Transform body)
    {
        Vector3 centerDir = (body.position - transform.position).normalized;
        Vector3 bodyUpDir = body.up;
        body.rotation = Quaternion.FromToRotation(bodyUpDir, centerDir)*body.rotation;
        body.GetComponent<Rigidbody>().AddForce(centerDir*grav);
    }
}
