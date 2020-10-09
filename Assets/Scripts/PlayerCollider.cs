using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    /// <summary>
    /// Used for object colliding
    /// </summary>
    [SerializeField]
    private RaycastManager raycastManager;

    private void OnTriggerEnter(Collider other)
    {
        var gameObj = other.gameObject;

        raycastManager.CollisionWithObject(gameObj);
    }
}
