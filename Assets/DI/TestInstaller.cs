using InputSamples.Gestures;
using UnityEngine;
using Zenject;

public class TestInstaller : MonoInstaller
{
    [SerializeField]
    public GameObject gesturePrefab;

    public override void InstallBindings()
    {
        Container.Bind<GestureController>().AsSingle().NonLazy();
    }
}