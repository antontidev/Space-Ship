using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

/// <summary>
/// Perk of rocket module
/// </summary>
[Serializable]
public class PerkModule : Perks
{
    public float price;

    public override float GetPrice()
    {
        return price;
    }
}

public enum ModuleState
{
    Planet,
    Rocket,
    Inventory
}

public interface State
{
    void OnStateActions();
}

public abstract class ModuleStateBase : State
{
    protected Rigidbody rb;

    protected PhysObj phys;

    protected BoxCollider collider;

    public ModuleStateBase(Rigidbody rb,
                           PhysObj phys, 
                           BoxCollider collider)
    {
        this.rb = rb;
        this.phys = phys;
        this.collider = collider;
    }

    public abstract void OnStateActions();
}

public class PlanetState : ModuleStateBase
{
    public PlanetState(Rigidbody rb,
                       PhysObj phys,
                       BoxCollider collider) : base(rb, phys, collider)
    {
    }

    public override void OnStateActions()
    {
        collider.enabled = true;

        phys.Active = true;
    }
}

public class InventoryState : ModuleStateBase
{
    public InventoryState(Rigidbody rb,
                          PhysObj phys,
                          BoxCollider collider) : base(rb, phys, collider)
    {
    }

    public override void OnStateActions()
    {
        rb.gameObject.SetActive(false);
    }
}

public class RocketState : ModuleStateBase
{
    public RocketState(Rigidbody rb,
                       PhysObj phys,
                       BoxCollider collider) : base(rb, phys, collider)
    {
    }

    public override void OnStateActions()
    {
        collider.enabled = false;

        phys.Active = false;
    }
}

[Serializable]
public class ModuleStateController
{
    public Dictionary<ModuleState, ModuleStateBase> states;

    public ReactiveProperty<ModuleState> state;

    private IDisposable _update;

    public ModuleStateController(ModuleState defaultState)
    {
        states = new Dictionary<ModuleState, ModuleStateBase>();

        state = new ReactiveProperty<ModuleState>();

        state.Value = defaultState;
    }

    ~ModuleStateController()
    {
        _update.Dispose();
    }


    private void StateChanged(ModuleState state)
    {
        var newState = states[state];

        newState.OnStateActions();
    }

    public void CreateStates(Rigidbody rb, 
                             PhysObj phys,
                             BoxCollider collider)
    {
        var planetState = new PlanetState(rb, phys, collider);

        var rocketState = new RocketState(rb, phys, collider);

        var inventoryState = new InventoryState(rb, phys, collider);

        states.Add(ModuleState.Planet, planetState);

        states.Add(ModuleState.Rocket, rocketState);

        states.Add(ModuleState.Inventory, inventoryState);


        _update = state.Subscribe(x =>
        {
            StateChanged(x);
        });
    }
}

/// <summary>
/// Module item class holding information about price
/// </summary>
public class ShipPart : ItemPart<PerkModule>
{
    public ModuleStateController stateController;

    private void Start()
    {
        stateController = new ModuleStateController(ModuleState.Planet);

        var rb = GetComponent<Rigidbody>();

        var phys = GetComponent<PhysObj>();

        var collider = GetComponent<BoxCollider>();

        stateController.CreateStates(rb, phys, collider);
    }

    #region Obsolete
    [HideInInspector]
    [SerializeField]
    [Obsolete("Possibly obsolete, it will dissapear somedays")]
    public float PartValue;

    [SerializeField]
    [HideInInspector]
    [Obsolete("This is no longer needed")]
    public Sprite sprite;

    [Obsolete("Now parts installed automatically")]
    public int ClickCount
    {
        get; set;
    } = 0;

    public Action<GameObject> onSecondClick;

    [Inject]
    private GlowManager glowManager;

    [Inject]
    private ModulesBridge modulesBridge;

    [Inject]
    private ActivePartManager activePartManager;
    
    ///// <summary>
    ///// Used to compare another ShipPart with current and check
    ///// if it has better perks than this
    ///// </summary>
    //[Obsolete("Use Perks instead for item that you need to sell")]
    //public bool IsBetter(ShipPart another)
    //{
    //    var anotherPrice = another.PartValue;

    //    return anotherPrice > PartValue;
    //}

    /// <summary>
    /// Don't now that is it exactly
    /// </summary>
    [Obsolete("Use RaycastManager instead")]
    public void ClickOnObject()
    {
        onSecondClick?.Invoke(gameObject);

        modulesBridge.PutNewModule(gameObject);
        activePartManager.PutNewModule(gameObject);

        ClickCount++;

        switch (ClickCount)
        {
            case 1:
                glowManager.PutNewModule(gameObject);
                break;

            case 2:
                onSecondClick?.Invoke(gameObject);

                modulesBridge.PutNewModule(gameObject);
                activePartManager.PutNewModule(gameObject);

                ClickCount = 0;

                break;

            default:
                ClickCount = 0;
                break;
        }
    }
    #endregion
}

/// <summary>
/// Used for UI modules
/// </summary>
[Obsolete("No longer needed")]
public class ModulesBridge
{
    public struct Pair<T, T1>
    {
        public T Key;
        public T1 Value;
    }

    public ReactiveCollection<Pair<string, GameObject>> modules;

    public ReactiveProperty<string> levelModule;

    public ReactiveProperty<string> moduleValue;

    public ModulesBridge()
    {
        modules = new ReactiveCollection<Pair<string, GameObject>>();
        levelModule = new ReactiveProperty<string>();
        moduleValue = new ReactiveProperty<string>();
    }

    public void PutNewModule(GameObject module)
    {
        var level = module.tag;

        var index = Contains(level);

        if (index != -1)
        {
            var pair = modules[index];
            Remove(pair);

            pair = new Pair<string, GameObject>
            {
                Key = level,
                Value = module
            };

            modules.Add(pair);
        }
        else
        {
            var pair = new Pair<string, GameObject>
            {
                Key = level,
                Value = module
            };
            modules.Add(pair);
        }
    }

    private int Contains(string module)
    {
        for (int i = 0; i < modules.Count; i++)
        {
            Pair<string, GameObject> element = modules[i];

            if (element.Key == module)
            {
                return i;
            }
        }
        return -1;
    }

    private void Remove(Pair<string, GameObject> pair)
    {
        var module = pair.Value;

        //UnityEngine.Object.Destroy(module);

        modules.Remove(pair);
    }
}
