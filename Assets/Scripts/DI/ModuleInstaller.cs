using UniRx;
using UnityEngine;
using Zenject;

public class ModuleInstaller : MonoInstaller
{
    [SerializeField]
    private Material glowMaterial;

    public override void InstallBindings()
    {
        Container.Bind<Material>().FromInstance(glowMaterial);
        Container.Bind<GlowManager>().AsCached().NonLazy(); ;
        Container.Bind<ActivePartManager>().AsCached().NonLazy();
    }
}