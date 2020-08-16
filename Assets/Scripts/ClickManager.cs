using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField]
    public GameManagerScript gameManager;

    public Dictionary<string, GameObject> activeObjects;

    [SerializeField]
    public GameObject smokeEffect;

    void Start() => activeObjects = new Dictionary<string, GameObject>();

    public void HandleClick(GameObject gameO)
    {
        var tagString = gameO.tag;

        if (activeObjects.ContainsKey(tagString))
        {
            var obj = activeObjects[tagString];

            obj.transform.parent = null;

            obj.transform.position = Random.onUnitSphere * 1;

            AddRigidBody(obj);
            if (tagString == "Bottom")
            {
                foreach (Transform child in obj.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        if (tagString == "Bottom")
        {
            var gameObjTransform = gameO.transform;

            Instantiate(smokeEffect, gameObjTransform.position, gameObjTransform.rotation);
        }

        DeleteRigidBody(gameO);

        activeObjects[tagString] = gameO;
    }

    private void AddRigidBody(GameObject go)
    {
        go.AddComponent<PhysObj>();
        go.AddComponent<BoxCollider>();
    }

    private void DeleteRigidBody(GameObject go)
    {
        var physObj = go.GetComponent<PhysObj>();
        var rigidGameO = go.GetComponent<Rigidbody>();
        var meshC = go.GetComponent<BoxCollider>();

        Destroy(physObj);
        Destroy(rigidGameO);
        Destroy(meshC);
    }
}
