using UnityEngine;
using Zenject;
using InputSamples.Gestures;

public class TestInstaller : MonoInstaller
{
    [SerializeField]
    public GameObject gesturePrefab;

    public override void InstallBindings()
    {
        Container.Bind<GestureController>().AsSingle().NonLazy();
    }
}