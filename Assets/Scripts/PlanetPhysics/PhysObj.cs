using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ZenAutoInjecter))]
public class PhysObj : MonoBehaviour
{
    [Inject]
    private Gravity planet;

    private void Awake()
    {
        var rb = GetComponent<Rigidbody>();

        rb.useGravity = false;

        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        planet.Attract(transform);
    }
}
