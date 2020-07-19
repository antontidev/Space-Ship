using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rocket : MonoBehaviour
{
    public delegate void OnReadyChange();
    public OnReadyChange onReadyModuleChange;

    [SerializeField]
    public List<GameObject> trueParts;

    [SerializeField]
    private List<Transform> positions;

    public Dictionary<string, bool> ready;

    public Quaternion defaultRotation;

    public ClickManager manager;

    private void Start()
    {
        ready = new Dictionary<string, bool>();
        foreach(var el in trueParts)
        {
            ready[el.tag] = false;
        }

    }

    public void SubmitTrueParts(List<GameObject> list)
    {
        trueParts = list;
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
                return true;
            }
            else
            {
                ready[part.tag] = false;
                return false;
            }
        }
        onReadyModuleChange?.Invoke();
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
        part.transform.rotation = Quaternion.Euler(-90, 0, 0);

        part.GetComponent<Rigidbody>().useGravity = false;
        part.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        part.GetComponent<PhysObj>().enabled = false;
    }

    public void ExchangeTransform(Transform trans)
    {

    } 
}
