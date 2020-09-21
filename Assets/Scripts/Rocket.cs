using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Rocket : MonoBehaviour
{
    private RocketInventory rocketInventory;

    [SerializeField]
    public List<Transform> positions;

    [Inject]
    public void Construct(RocketInventory rocketInventory)
    {
        this.rocketInventory = rocketInventory;
    }

    private void Start()
    {
        rocketInventory.ItemAdded += RocketInventory_ItemAdded;
        rocketInventory.ItemChanged += RocketInventory_ItemChanged;
    }

    private void OnDisable()
    {
        rocketInventory.ItemAdded -= RocketInventory_ItemAdded;
        rocketInventory.ItemChanged -= RocketInventory_ItemChanged;
    }

    /// <summary>
    /// Invokes when there is changes in level of rocket. When new module is better than old.
    /// </summary>
    /// <param name="_">
    /// Sender parameter. I don't want to use it so _
    /// </param>
    /// <param name="e">
    /// Added event argument. Holds information about level and module
    /// </param>
    private void RocketInventory_ItemChanged(object _, InventoryEventArgs<string> e)
    {
    }

    /// <summary>
    /// Invokes when there is new item in rocket inventory.
    /// Invokes many times as there is variables of rocket levels.
    /// </summary>
    /// <param name="_">
    /// Sender parameter. I don't want to use it so _
    /// </param>
    /// <param name="e">
    /// Added event argument. Holds information about level and module
    /// </param>
    private void RocketInventory_ItemAdded(object _, InventoryEventArgs<ItemType> e)
    {
        var module = e.Item;

        module.transform.parent = transform;

        module.SetActive(false);
    }

    #region Obsolete    
    [Inject]
    [Obsolete("Use rocketInventory instead")]
    private ActivePartManager activePartManager;

    [SerializeField]
    private List<GameObject> trueParts;

    [Obsolete("There is no longer True or False parts")]
    public void SubmitTrueParts(List<GameObject> trueParts)
    {
        this.trueParts = trueParts;
    }

    /// <summary>
    /// Used for handle new rocket part positions
    /// </summary>
    /// <param name="part"></param>
    [Obsolete("Use RocketInventory callbacks instead")]
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

        // part.GetComponent<Rigidbody>().useGravity = false;
        //part.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        part.GetComponent<PhysObj>().enabled = false;
    }
    #endregion
}