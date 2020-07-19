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
            case "Bottom":
                part.transform.position = GameObject.FindGameObjectWithTag("ModuleOne").transform.position;
                break;
            case "Middle":
                part.transform.position = GameObject.FindGameObjectWithTag("ModuleTwo").transform.position;
                break;
            case "Top":
                part.transform.position = GameObject.FindGameObjectWithTag("ModuleThree").transform.position;
                break;
        }

        part.GetComponent<Rigidbody>().useGravity = false;
        part.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        part.GetComponent<PhysObj>().enabled = false;
    }
}
