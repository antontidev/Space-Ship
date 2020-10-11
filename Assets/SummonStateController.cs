using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonStateController : MonoBehaviour
{
    private SummonStates summonStates;

    public AnimationController animationController;

    public PlayerRotator rotator;

    public PlayerController3d controller;

    public Material material;

    public SkinnedMeshRenderer renderer;

    /// <summary>
    /// You can set it through editor and choose initial state
    /// Only for test purposes, in real game choose Free state
    /// </summary>
    public SummonState initialSummonState;

    [Layer]
    public int controlLayer;

    // Start is called before the first frame update
    void Start()
    {
        summonStates = new SummonStates(initialSummonState);

        summonStates.CreateStates(animationController,
                                  rotator,
                                  controller,
                                  material,
                                  renderer,
                                  controlLayer);
    }

    public void ChangeState(SummonState state)
    {
        summonStates.state.Value = state;
    }
}
