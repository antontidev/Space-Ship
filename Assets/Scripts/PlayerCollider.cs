using UnityEngine;
using Zenject;

public class PersonCollider : MonoBehaviour
{
    /// <summary>
    /// Used for object colliding
    /// </summary>
    [Inject]
    protected RaycastManager raycastManager;

    public void OnTriggerEnter(Collider other)
    {
        var gameObj = other.gameObject;

        raycastManager.CollisionWithObject(gameObj);
    }
}

public class PlayerCollider : PersonCollider
{
    
}
