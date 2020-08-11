using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ModulePerk))]
public class ShipPart : MonoBehaviour
{
    public Action<GameObject> onSecondClick;

    private int clickCount = 0;

    [Inject]
    public GlowManager glowManager;

    [Inject]
    public ActivePartManager activePartManager;

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ClickOnObject()
    {
        clickCount++;

        switch (clickCount)
        {
            case 1:
                glowManager.PutNewModule(meshRenderer.materials);
                break;

            case 2:
                onSecondClick?.Invoke(gameObject);

                activePartManager.PutNewModule(gameObject);

                glowManager.RestoreMaterials();

                clickCount = 0;

                break;

            default:
                clickCount = 0;
                break;
        }
    }
}