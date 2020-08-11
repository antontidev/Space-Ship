using System;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public Action OnReadyModuleChange;

    [SerializeField]
    public List<GameObject> trueParts;

    [SerializeField]
    public List<Transform> positions;

    public Dictionary<string, bool> ready;

    private void Start()
    {
        ready = new Dictionary<string, bool>();
        foreach (var el in trueParts)
        {
            ready[el.tag] = false;
        }
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
        var namePart = part.name.Substring(0, part.name.IndexOf('('));
        foreach (var el in trueParts)
        {
            if (el.name == namePart)
            {
                ready[part.tag] = true;

                OnReadyModuleChange?.Invoke();
                return true;
            }
            else
            {
                ready[part.tag] = false;

                OnReadyModuleChange?.Invoke();
                return false;
            }
        }
        return false;
    }

    public void Handle(GameObject part)
    {
        CheckPart(part);
        part.transform.parent = gameObject.transform;

        switch (part.tag)
        {
            case "Top":
                part.transform.position = positions[0].position;
                break;
            case "Middle":
                part.transform.position = positions[1].position;
                break;
            case "Bottom":
                part.transform.position = positions[2].position;
                break;
        }
        part.transform.rotation = Quaternion.Euler(0, 0, 0);

        part.GetComponent<Rigidbody>().useGravity = false;
        part.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        part.GetComponent<PhysObj>().enabled = false;
    }
}
