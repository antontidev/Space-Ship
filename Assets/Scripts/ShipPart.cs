using FairyGUI;
using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShipPart : MonoBehaviour
{
    public delegate void OnClick(GameObject obj);

    public OnClick onClick;

    private int clickCount = 0;

    private Material glowEffect;

    private MeshRenderer renderer;

    public void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
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
                var list = ToList(renderer.materials);

                list.Add(glowEffect);

                renderer.materials = list.ToArray();
                break;
            case 2:
                var matList = ToList(renderer.materials);

                matList.RemoveAt(matList.Count);

                renderer.materials = matList.ToArray();

                clickCount = 0;

                onClick?.Invoke(gameObj);
                break;
        }
    }

    private List<Material> ToList(Material[] materials)
    {
        List<Material> listMaterials = new List<Material>();
        materials.ForEach((Material material) =>
        {
            listMaterials.Add(material);
        });

        return listMaterials;
    }
}
