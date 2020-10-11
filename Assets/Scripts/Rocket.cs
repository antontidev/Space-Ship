using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Rocket : MonoBehaviour
{
    private RocketInventory rocketInventory;

    [SerializeField]
    public List<Transform> positions;

    /// <summary>
    /// Temporary for 3 part rocket
    /// </summary>
    private Dictionary<string, Vector3> PartsPositions;

    [Inject]
    public void Construct(RocketInventory rocketInventory)
    {
        this.rocketInventory = rocketInventory;
    }

    private void Start()
    {
        PartsPositions = new Dictionary<string, Vector3>();
        
        PopulatePosition();

        rocketInventory.ItemAdded += RocketInventory_ItemAdded;
        rocketInventory.ItemChanged += RocketInventory_ItemChanged;
    }

    private void PopulatePosition()
    {
        foreach (var partPosition in positions)
        {
            var tag = partPosition.tag;

            var position = partPosition.position;

            PartsPositions.Add(tag, position);
        }
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
        var module = e.Item;

        PlaceOnRocket(module);
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

        PlaceOnRocket(module);
    }

    private void PlaceOnRocket(GameObject module)
    {
        var shipPart = module.GetComponent<ShipPart>();

        shipPart.stateController.state.Value = ModuleState.Rocket;

        var newModuleTag = module.tag;

        module.transform.parent = transform;

        var position = PartsPositions[newModuleTag];

        var newModuleTransform = module.transform;

        newModuleTransform.rotation = Quaternion.identity;

        newModuleTransform.position = position;
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