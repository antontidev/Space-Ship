using UnityEngine;
using Zenject;

public class GlowManager
{
    //[Inject]
    //public GameObject glowParticles;

    private Material[] materials;

    private Material[] defaultMaterials;

    public void PutNewModule(GameObject module)
    {
        
    }

    private void MakeGlow()
    {
        
    }

    private void CopyDefaultToMaterials()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = defaultMaterials[i];
        }
    }
}
