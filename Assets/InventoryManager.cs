using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

/// <summary>
/// Used to store all objects in children and deactivates them
/// </summary>
public class InventoryManager : MonoBehaviour
{
    [Inject]
    private GlobalInventory globalInventory;

    private void OnEnable()
    {
        globalInventory.ItemAdded += GlobalInventory_ItemAdded;
    }

    private void OnDisable()
    {
        globalInventory.ItemAdded -= GlobalInventory_ItemAdded;
    }

    /// <summary>
    /// Invokes when item added to inventory.
    /// </summary>
    /// <param name="_">
    /// Sender parameter
    /// I don't think it's a good idea to use it, but you may.
    /// </param>
    /// <param name="e">
    /// Event args. Holds type of item and item itself which has been
    /// added to inventory.
    /// </param>
    private void GlobalInventory_ItemAdded(object _, InventoryEventArgs<ItemType> e)
    {
        var item = e.Item;

        item.transform.parent = transform;

        item.SetActive(false);
    }
}
