using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class Rocket : MonoBehaviour
{
    [Inject]
    private ActivePartManager activePartManager;

    public List<GameObject> trueParts;

    [SerializeField]
    public List<Transform> positions;

    public void Handle(GameObject part)
    {
        activePartManager.CheckPart(part);
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
    }
}