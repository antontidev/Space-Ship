using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// In future it's better to use this class with Zenject dependency injection framework
/// </summary>
public class ActivePartManager
{
    private Dictionary<string, GameObject> activeModules;

    private Dictionary<int, ModuleComponents> cachedComponents;

    private GameObject clickedModule;

    private struct ModuleComponents
    {
        public PhysObj phys;
        public BoxCollider collider;
        public Rigidbody rigidbody;
    }

    private ActivePartManager()
    {
        activeModules = new Dictionary<string, GameObject>();

        cachedComponents = new Dictionary<int, ModuleComponents>();
    }

    public void PutNewModule(GameObject module)
    {
        var moduleID = module.GetInstanceID();

        var level = module.tag;

        // Check if there is actual part of rocket
        if (activeModules.ContainsKey(level))
        {
            var previousModule = activeModules[level];

            var previousModuleID = previousModule.GetInstanceID();

            var previousModules = cachedComponents[previousModuleID];

            ActivateModules(previousModules);
        }

        if (!cachedComponents.ContainsKey(moduleID))
        {
            var components = GetRequiredComponents(module);

            cachedComponents[moduleID] = components;

            DeactivateModules(components);
        }

        // In all cases add or update link to active module
        activeModules[level] = module;
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
}
