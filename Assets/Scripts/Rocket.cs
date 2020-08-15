using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public Action OnReadyModuleChange;

    public List<GameObject> trueParts;

    [SerializeField]
    public List<Transform> positions;

    public Dictionary<string, bool> ready;

    public ReactiveProperty<bool> IsReady
    {
        get; private set;
    }

    void Awake()
    {
        ready = new Dictionary<string, bool>();
        IsReady = new ReactiveProperty<bool>
        {
            Value = false
        };
    }

    public bool CheckPart(GameObject part)
    {
        foreach (var el in trueParts)
        {
            if (string.Format("{0}(Clone)", el.name) == part.name)
            {
                ready[part.tag] = true;

                IsReady.Value = CheckReady();

                OnReadyModuleChange?.Invoke();
                return true;
            }
        }
        return false;
    }

    private bool CheckReady()
    {
        foreach (var el in ready)
        {
            if (!el.Value)
            {
                return false;
            }
        }

        return true;
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

    public void SubmitTrueParts(List<GameObject> trueParts)
    {
        this.trueParts = trueParts;

        PopulateReadyDictionary(trueParts);
    }

    private void PopulateReadyDictionary(List<GameObject> trueParts)
    {
        foreach (var el in trueParts)
        {
            ready[el.tag] = false;
        }
    }
}