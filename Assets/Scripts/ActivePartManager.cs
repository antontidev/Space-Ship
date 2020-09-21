using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

/// <summary>
/// In future it's better to use this class with Zenject dependency injection framework
/// </summary>
[Obsolete("Use RocketInventory instead")]
public class ActivePartManager
{
    public ReactiveDictionary<string, ShipPart> activeModules;

    public ReactiveDictionary<string, bool> ready;

    private List<GameObject> trueParts;

    private Dictionary<int, ModuleComponents> cachedComponents;

    private GameObject clickedModule;

    public ReactiveProperty<bool> IsReady
    {
        get; private set;
    }

    private ActivePartManager()
    {
        activeModules = new ReactiveDictionary<string, ShipPart>();

        cachedComponents = new Dictionary<int, ModuleComponents>();

        ready = new ReactiveDictionary<string, bool>();

        IsReady = new ReactiveProperty<bool>
        {
            Value = false
        };
    }

    private struct ModuleComponents
    {
        public PhysObj phys;
        public BoxCollider collider;
        public Rigidbody rigidbody;
    }

    public void PutNewModule(GameObject module)
    {
        var moduleID = module.GetInstanceID();

        var shipPart = module.GetComponent<ShipPart>();

        var level = module.tag;

        // In all cases add or update link to active module
        activeModules[level] = shipPart;
    }

    public bool CheckPart(GameObject part)
    {
        foreach (var el in trueParts)
        {
            if (string.Format("{0}(Clone)", el.name) == part.name)
            {
                ready[part.tag] = true;

                IsReady.Value = CheckReady();

                return true;
            }
            if (el.tag == part.tag)
            {
                ready[part.tag] = false;
                return false;
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

    private ModuleComponents GetRequiredComponents(GameObject module)
    {
        var phys = module.GetComponent<PhysObj>();
        var collider = module.GetComponent<BoxCollider>();
        var rigidbody = module.GetComponent<Rigidbody>();

        var moduleComponents = new ModuleComponents
        {
            phys = phys,
            collider = collider,
            rigidbody = rigidbody
        };

        return moduleComponents;
    }

    public void LevelLoaded(Level level)
    {
        var trueParts = level.trueModules;

        this.trueParts = trueParts;

        FillActivePartDictionary();

        PopulateReadyDictionary();
    }

    private void FillActivePartDictionary()
    {
        foreach (var element in trueParts)
        {
            activeModules.Add(element.tag, null);
        }

    }

    private void ActivateModules(ModuleComponents modules)
    {
        modules.phys.enabled = true;
        modules.rigidbody.isKinematic = false;
        modules.collider.enabled = true;
    }

    private void DeactivateModules(ModuleComponents modules)
    {
        modules.phys.enabled = false;
        modules.rigidbody.isKinematic = true;
        modules.collider.enabled = false;
    }

    private void PopulateReadyDictionary()
    {
        foreach (var el in trueParts)
        {
            ready[el.tag] = false;
        }
    }
}