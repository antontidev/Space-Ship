using UnityEngine;

public class Gravity : MonoBehaviour
{
    [SerializeField]
    public float grav = -10f;

    [SerializeField]
    public float playerGrav = -5f;

    [SerializeField]
    public float temperature;

    [SerializeField]
    public float oxygen;

    public void Attract(Transform body)
    {
        Vector3 centerDir = (body.position - transform.position).normalized;
        Vector3 bodyUpDir = body.up;
        body.rotation = Quaternion.FromToRotation(bodyUpDir, centerDir) * body.rotation;
        body.GetComponent<Rigidbody>().AddForce(centerDir * grav);
    }
}
