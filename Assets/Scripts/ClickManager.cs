using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField]
    public GameManagerScript gameManager;

    public Dictionary<string, GameObject> activeObjects;

    void Start ()
    {
        activeObjects = new Dictionary<string, GameObject>();
    }

    public void HandleClick(GameObject gameO)
    {
        var tagString = gameO.tag;

        if (activeObjects.ContainsKey(tagString))
        {
            var obj = activeObjects[tagString];

            obj.transform.DetachChildren();

            obj.transform.position = Random.onUnitSphere * 1;

            AddRigidBody(obj);
        }

        DeleteRigidBody(gameO);

        activeObjects[tagString] = gameO;
        
   
       
    }

    private void AddRigidBody(GameObject go)
    {
        go.AddComponent<PhysObj>();
        var meshC = go.AddComponent<MeshCollider>();
        meshC.convex = true;
    }

    private void DeleteRigidBody(GameObject go)
    {
        var physObj = go.GetComponent<PhysObj>();
        var rigidGameO = go.GetComponent<Rigidbody>();
        var meshC = go.GetComponent<MeshCollider>();

        Destroy(physObj);
        Destroy(rigidGameO);
        Destroy(meshC);
    }
}
