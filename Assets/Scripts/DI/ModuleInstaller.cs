using LeoLuz.PlugAndPlayJoystick;
using UniRx;
using UnityEngine;
using Zenject;

public class ModuleInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GlowManager>().AsCached().NonLazy(); ;
        Container.Bind<ActivePartManager>().AsCached().NonLazy();
        Container.Bind<PlanetBridge>().AsCached().NonLazy();
        Container.Bind<ModulesBridge>().AsCached().NonLazy();
        Container.Bind<LevelLoader>().AsCached().NonLazy();
        Container.Bind<IJoystickInput>().AsCached().NonLazy();
    }
}