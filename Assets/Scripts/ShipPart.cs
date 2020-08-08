using MyBox;
using System.Collections.Generic;
using UnityEngine;


public class ShipPart : MonoBehaviour
{
    public delegate void OnSecondClick(GameObject obj);

    public OnSecondClick onClick;

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

                if (glowEffect != null)
                {
                    list.Add(glowEffect);
                }

                renderer.materials = list.ToArray();
                break;
            case 2:
                onClick?.Invoke(gameObj);

                //var matList = ToList(renderer.materials);

                //if (matList[matList.Count] != null)
                //{
                ////    matList.RemoveAt(matList.Count);
                //}

                //renderer.materials = matList.ToArray();

                clickCount = 0;

                break;
            default:
                clickCount = 0;
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
