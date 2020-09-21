using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public enum ItemType : int
{
    Modules,
    Astrounaut,
    Trash,
    Rock
}

/// <summary>
/// Class for representing store lot
/// which can be sold
/// </summary>
public class StoreLot
{
    /// <summary>
    /// Static property for price comparing
    /// </summary>
    private static float compareValueDelta = 0.001f;

    private float price;

    private GameObject item;

    public StoreLot()
    {
    }

    public bool IsBigger(float anotherPrice)
    {
        var delta = anotherPrice - price;

        return delta > compareValueDelta;
    }
}

/// <summary>
/// Custom EventArgs for ItemAdded and ItemChanged events
/// for IInventory childrens
/// </summary>
/// <remarks>
/// Nothing special
/// </remarks>
public class InventoryEventArgs<T>
{
    public T Type
    {
        get; private set;
    }

    public GameObject Item
    {
        get; private set;
    }

    public GameObject OldItem
    {
        get; private set;
    }

    public InventoryEventArgs(T Type,
                              GameObject Item,
                              GameObject OldItem)
    {
        this.Type = Type;
        this.Item = Item;
        this.OldItem = OldItem;
    }
}

/// <summary>
/// It's better to store all items in 
/// Dictionary<ItemType, List<GameObject>>
/// to get a quick access to all similar objects of type
/// </summary>
/// <remarks>
/// API may be easily grown by adding new methods to this interface.
/// </remarks>
public interface IInventory
{
    /// <summary>
    /// Invokes when item has been added to Inventory
    /// </summary>
    event EventHandler<InventoryEventArgs<ItemType>> ItemAdded;

    /// <summary>
    /// Ads value to the inventory
    /// </summary>
    /// <param name="itemType">
    /// Type of item which should be added to inventory
    /// </param>
    /// <param name="item"></param>
    void PutItem(GameObject item, ItemType itemType = ItemType.Modules);

    /// <summary>
    /// Enumerates around collection of modules
    /// and place the best ones to the rocket
    /// </summary>
    /// <returns></returns>
    void PlaceToRocket(GameObject module);

    /// <summary>
    /// Removes item from inventory
    /// </summary>
    /// <param name="item"></param>
    /// <param name="itemType"></param>
    void Delete(GameObject item, ItemType itemType = ItemType.Modules);
}

/// <summary>
/// Inventory for rocket GameObject. Holds rocket modules.
/// </summary>
public class RocketInventory : IInventory
{
    private Dictionary<string, GameObject> inventory;

    private Dictionary<string, float> inventoryPrice;

    public ReactiveCollection<GameObject> backToGlobalInventory;

    public event EventHandler<InventoryEventArgs<ItemType>> ItemAdded;

    public event EventHandler<InventoryEventArgs<string>> ItemChanged;

    public RocketInventory()
    {
        backToGlobalInventory = new ReactiveCollection<GameObject>();
        inventory = new Dictionary<string, GameObject>();
        inventoryPrice = new Dictionary<string, float>();
    }

    /// <summary>
    /// Trying to place module object on rocket. If rocket
    /// has better module than new module it returns module to global 
    /// inventory.
    /// </summary>
    /// <param name="module"></param>
    public void PlaceToRocket(GameObject module)
    {
        var shipPart = module.GetComponent<ShipPart>();

        var newPrice = shipPart.PartValue;

        var moduleTag = module.tag;

        if (inventoryPrice.ContainsKey(moduleTag))
        {
            var currentLevelModulePrice = inventoryPrice[moduleTag];

            if (newPrice >= currentLevelModulePrice)
            {
                PutItem(module);
            }
            else
            {
                backToGlobalInventory.Add(module);
            }
        }
        else
        {
            PutItem(module);
        }
    }

    /// <summary>
    /// Puts old module back to global inventory
    /// </summary>
    /// <param name="item"></param>
    /// <param name="itemType"></param>
    public void PutItem(GameObject item,
                        ItemType itemType = ItemType.Modules)
    {
        Debug.Log("Whoa, new module dude! " + item.name);

        var moduleTag = item.tag;

        var shipPart = item.GetComponent<ShipPart>();

        var modulePrice = shipPart.PartValue;

        if (inventory.ContainsKey(moduleTag))
        {
            var oldObject = inventory[moduleTag];

            backToGlobalInventory.Add(oldObject);

            var changedArgs = new InventoryEventArgs<string>(moduleTag, 
                                                             item, 
                                                             oldObject);

            ItemChanged?.Invoke(this, changedArgs);
        }
        else
        {
            var addedArgs = new InventoryEventArgs<ItemType>(itemType, 
                                                             item, 
                                                             null);

            ItemAdded?.Invoke(this, addedArgs);
        }

        inventory[moduleTag] = item;

        inventoryPrice[moduleTag] = modulePrice;
    }

    /// <summary>
    /// Deletes module from backToGlobalInventory after
    /// it has been added to.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="itemType"></param>
    public void Delete(GameObject item, 
                       ItemType itemType = ItemType.Modules)
    {
        backToGlobalInventory.Remove(item);
    }
}

/// <summary>
/// Global inventory class. Accepts all items on level.
/// Place module item to the rocket inventory. If rocket inventory
/// </summary>
public class GlobalInventory : IInventory
{
    /// <summary>
    /// Maps type of object from enum to actual List of items 
    /// that type in inventory.
    /// </summary>
    public Dictionary<ItemType, List<GameObject>> inventory;

    private RocketInventory rocketInventory;

    public event EventHandler<InventoryEventArgs<ItemType>> ItemAdded;

    /// <summary>
    /// There is injected RocketInventory which is also 
    /// as GlobalInventory created using Zenject Singletons.
    /// </summary>
    /// <param name="rocketInventory">
    /// Injected RocketInventory object created by Zenject
    /// </param>
    [Inject]
    public GlobalInventory(RocketInventory rocketInventory)
    {
        this.rocketInventory = rocketInventory;

        inventory = new Dictionary<ItemType, List<GameObject>>();

        var addEvent = rocketInventory.backToGlobalInventory.ObserveAdd();

        // Returns object back to global inventory
        addEvent.Subscribe(x =>
        {
            List<GameObject> list;

            var item = x.Value;

            if (!inventory.ContainsKey(ItemType.Modules))
            {
                list = new List<GameObject>();

                inventory[ItemType.Modules] = list;
            }
            else
            {
                list = inventory[ItemType.Modules];

                rocketInventory.Delete(item);
            }

            list.Add(item);

            var addedEventArgs = new InventoryEventArgs<ItemType>(ItemType.Modules, 
                                                                  item, 
                                                                  null);

            ItemAdded?.Invoke(this, addedEventArgs);
        });
    }

    /// <summary>
    /// Method for deleting item from inventory
    /// I don't know where we need to use this
    /// but for API convinient it's better to leave it.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="itemType"></param>
    public void Delete(GameObject item, 
                       ItemType itemType = ItemType.Modules)
    {
        var list = inventory[itemType];

        list.Remove(item);
    }

    /// <summary>
    /// Try to place module to the RocketInventory.
    /// If it fails, module will return to GlobalInventory.
    /// </summary>
    /// <param name="module">
    /// Module that may be new module
    /// </param>
    public void PlaceToRocket(GameObject module)
    {
        rocketInventory.PlaceToRocket(module);
    }

    /// <summary>
    /// Pust item to global inventory
    /// </summary>
    /// <param name="item">
    /// Item that you want to put to the inventory
    /// </param>
    /// <param name="itemType">
    /// Type of item from ItemType Enum
    /// </param>
    public void PutItem(GameObject item, 
                        ItemType itemType)
    {
        Debug.Log("You picked" + item.name);

        if (itemType == ItemType.Modules)
        {
            PlaceToRocket(item);

            return;
        }

        if (!inventory.ContainsKey(itemType))
        {
            var newType = new List<GameObject>() { item };

            inventory.Add(itemType, newType);
        }
        else
        {
            var listType = inventory[itemType];

            listType.Add(item);
        }

        var addedEventArgs = new InventoryEventArgs<ItemType>(itemType, 
                                                              item,
                                                              null);

        ItemAdded?.Invoke(this, addedEventArgs);
    }
}

/// <summary>
/// Actually this doesn't work now
/// </summary>
[Obsolete("Use RaycastManager now")]
public class ClickManager : MonoBehaviour
{
    [SerializeField]
    public GameManagerScript gameManager;

    [SerializeField]
    private List<GameObject> effects;

    private Dictionary<string, GameObject> activeObject;

    public void HandleClick(GameObject gameO)
    {
        // Now we just need to store all collected objects to inventory

        var tagString = gameO.tag;

        if (activeObject.ContainsKey(tagString))
        {
            var obj = activeObject[tagString];

            obj.transform.parent = null;

            obj.transform.position = UnityEngine.Random.onUnitSphere * 2;

            AddRigidBody(obj);
            if (tagString == "Bottom")
            {
                foreach (Transform child in obj.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        DeleteRigidBody(gameO);

        activeObject[tagString] = gameO;

        if (tagString == "Bottom")
        {
            var gameObjTransform = gameO.transform;

            foreach (var element in effects)
            {

                Instantiate(element, gameObjTransform.position, gameObjTransform.rotation);
            }
        }

    }

    // Possibly obsolete
    private void AddRigidBody(GameObject go)
    {
        go.AddComponent<PhysObj>();
        //go.AddComponent<BoxCollider>();
    }

    // Possibly obsolete
    private void DeleteRigidBody(GameObject go)
    {
        var physObj = go.GetComponent<PhysObj>();
        var rigidGameO = go.GetComponent<Rigidbody>();
        var meshC = go.GetComponent<BoxCollider>();

        Destroy(physObj);
        Destroy(rigidGameO);
        //Destroy(meshC);
    }
}
