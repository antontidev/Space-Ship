using UnityEngine;
using Zenject;

public class GlowManager
{
    [Inject]
    public Material glowEffect;

    private Material[] materials;

    private Material[] defaultMaterials;

    private static GlowManager instance = null;

    public GlowManager(Material glowMaterial)
    {
        glowEffect = glowMaterial;
    }

    public void PutNewModule(Material[] newMaterials)
    {
        defaultMaterials = new Material[newMaterials.Length];

        materials = newMaterials;

        CopyMaterialsToDefault();

        MakeGlow();
    }

    public void RestoreMaterials()
    {
        CopyDefaultToMaterials();
    }

    private void MakeGlow()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = glowEffect;
        }
    }

    private void CopyDefaultToMaterials()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = defaultMaterials[i];
        }
    }

    private void CopyMaterialsToDefault()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            defaultMaterials[i] = materials[i];
        }
    }
}
