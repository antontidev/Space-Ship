using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnObject", menuName = "Spawner/Object with position", order = 1)]
[Obsolete("Don't know why we need this")]
public class ObjectWithPosition : ScriptableObject
{
    [SerializeField]
    public GameObject obj;

    [SerializeField]
    public Vector3 position;
}
