using UniRx;
using UnityEngine;
using Zenject;

public class PlanetBridge
{
    public ReactiveProperty<string> planetName;

    public ReactiveProperty<float> gravity;

    public ReactiveProperty<int> temperature;

    public ReactiveProperty<bool> atmosphere;

    public PlanetBridge()
    {
        planetName = new ReactiveProperty<string>();
        gravity = new ReactiveProperty<float>();
        temperature = new ReactiveProperty<int>();
        atmosphere = new ReactiveProperty<bool>();
    }
}

public class PlanetPerk : MonoBehaviour
{
    [Inject]
    private PlanetBridge planetBridge;

    [SerializeField]
    private string planetName;

    [SerializeField]
    private float gravity;

    [SerializeField]
    private int temperature;

    [SerializeField]
    private bool atmosphere;

    private void Awake()
    {
        planetBridge.planetName.Value = planetName;
        planetBridge.gravity.Value = gravity;
        planetBridge.temperature.Value = temperature;
        planetBridge.atmosphere.Value = atmosphere;
    }
}
