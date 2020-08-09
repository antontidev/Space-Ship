using MyBox;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ShipPart : MonoBehaviour
{
    public Action<GameObject> onClick;

    private int clickCount = 0;

    private Material glowEffect;

    private Material defaultMaterial;

    private MeshRenderer meshRenderer;

    public void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        defaultMaterial = meshRenderer.material;
    }

    public void SubmitGlowMaterial(Material glow)
    {
        glowEffect = glow;
    }

    public void ClickOnObject(GameObject gameObj)
    {
        clickCount++;

        switch (clickCount)
        {
            case 1:
                meshRenderer.material = glowEffect;
                break;

            case 2:
                onClick?.Invoke(gameObj);

                meshRenderer.material = defaultMaterial;

                clickCount = 0;

                break;

            default:
                clickCount = 0;
                break;
        }
    }
}