using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> trueParts;

    private Dictionary<GameObject, bool> ready;

    private void Start()
    {
        ready = new Dictionary<GameObject, bool>();
    }

    public bool IsReady
    {
        get
        {
            return ready.Count < trueParts.Count;
        }
    }

    public bool CheckPart(GameObject part)
    {
        Debug.Log("I'm in checkpart");
        foreach (var el in trueParts)
        {
            if (el == part)
            {
                ready.Add(part, true);
                return true;
            }
        }
        return false;
    }
}
