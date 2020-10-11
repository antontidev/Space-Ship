using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ZenAutoInjecter))]
public class PhysObj : MonoBehaviour
{
    public bool Active;

    [Inject]
    private Gravity planet;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;

        rb.constraints = RigidbodyConstraints.FreezeRotation;

        Active = true;
    }

    void FixedUpdate()
    {
        if (Active)
        {
            planet.Attract(transform, rb);
        }
    }
}
