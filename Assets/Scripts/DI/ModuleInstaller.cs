using LeoLuz.PlugAndPlayJoystick;
using Zenject;

public class ModuleInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ModulesBridge>().AsCached().NonLazy();
        Container.Bind<ActivePartManager>().AsCached().NonLazy();

        Container.Bind<GlowManager>().AsCached().NonLazy();
        Container.Bind<PlanetBridge>().AsCached().NonLazy();
        Container.Bind<LevelLoader>().AsCached().NonLazy();
        Container.Bind<IJoystickInput>().AsCached().NonLazy();

        Container.Bind<RocketInventory>().AsCached().NonLazy();
        Container.Bind<GlobalInventory>().AsCached().NonLazy();
        Container.Bind<AstronautInventory>().AsCached().NonLazy();
    }

    #region Obsolere
    /// <summary>
    /// Wrong concept of DI framewors
    /// </summary>
    /// <returns></returns>
    public DiContainer GetContainer()
    {
        return Container;
    }
    #endregion
}