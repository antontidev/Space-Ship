using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> trueParts;

    [SerializeField]
    private List<Transform> positions;

    private Dictionary<GameObject, bool> ready;

    public Quaternion defaultRotation;

    private void Start()
    {
        ready = new Dictionary<GameObject, bool>();
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
        part.transform.rotation = transform.rotation;

        part.GetComponent<Rigidbody>().useGravity = false;
        part.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        part.GetComponent<PhysObj>().enabled = false;
    }

    public void ExchangeTransform(Transform trans)
    {

    } 
}
