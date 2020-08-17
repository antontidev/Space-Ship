using System;
using UniRx;
using UnityEngine;
using Zenject;

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

[RequireComponent(typeof(ModulePerk))]
public class ShipPart : MonoBehaviour
{
    [SerializeField]
    public Sprite sprite;

    public Action<GameObject> onSecondClick;

    public int ClickCount
    {
        get; set;
    } = 0;

    [Inject]
    private GlowManager glowManager;

    [Inject]
    private ModulesBridge modulesBridge;

    [Inject]
    private ActivePartManager activePartManager;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ClickOnObject()
    {
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

    public void DeactivateGlow()
    {
     
    }
}