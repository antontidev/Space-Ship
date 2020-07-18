using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Levels/CreateLevel", order = 1)]
public class Level : ScriptableObject
{
    [SerializeField]
    public List<GameObject> objects;
    [SerializeField]
    public Mesh planet;
}
