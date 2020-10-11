using System;
using System.Collections.Generic;
using UniRx;
using UnityEditor.Animations;
using UnityEngine;
using Zenject;

public enum SummonState
{
    Free,
    Controll,
    Dead
}

public abstract class SummonStateBase : State
{
    protected AnimationController animationController;

    protected PlayerRotator rotator;

    protected PlayerController3d move;

    protected Material controlMaterial;

    protected SkinnedMeshRenderer renderer;

    public SummonStateBase(AnimationController animationController,
                           PlayerRotator rotator,
                           PlayerController3d move,
                           Material controlMaterial,
                           SkinnedMeshRenderer renderer)
    {
        this.animationController = animationController;
        this.rotator = rotator;
        this.move = move;
        this.controlMaterial = controlMaterial;
        this.renderer = renderer;
    }

    public abstract void OnStateActions();
}
public class FreeState : SummonStateBase
{
    public FreeState(AnimationController animationController,
                     PlayerRotator rotator,
                     PlayerController3d move,
                     Material controlMaterial,
                     SkinnedMeshRenderer renderer) : base(animationController,
                                                          rotator,
                                                          move, 
                                                          controlMaterial,
                                                          renderer)
    {

    }

    public override void OnStateActions()
    {
        animationController.UnsubscribeAnimation();
        rotator.rotationType.Value = PlayerRotator.RotationType.None;
        move.moveType.Value = PlayerController3d.MoveType.None;
    }
}

public class ControlState : SummonStateBase
{
    private int controlLayer;

    public ControlState(AnimationController animationController,
                        PlayerRotator rotator,
                        PlayerController3d move,
                        Material controlMaterial,
                        SkinnedMeshRenderer renderer,
                        int controlLayer) : base(animationController,
                                                 rotator,
                                                 move,
                                                 controlMaterial,
                                                 renderer)
    {
        this.controlLayer = controlLayer;
    }

    public override void OnStateActions()
    {
        animationController.SubscribeAnimation();
        rotator.rotationType.Value = PlayerRotator.RotationType.Summon;
        move.moveType.Value = PlayerController3d.MoveType.Summon;

        var materials = renderer.materials;

        materials[1] = controlMaterial;

        renderer.materials = materials;

        var gameObj = move.gameObject;

        gameObj.layer = controlLayer;
    }
}

public class DeadState : SummonStateBase
{
    [Inject]
    private AstronautInventory astronautInventory;

    public DeadState(AnimationController animationController,
                     PlayerRotator rotator,
                     PlayerController3d move,
                     Material controlMaterial,
                     SkinnedMeshRenderer renderer) : base(animationController,
                                                          rotator,
                                                          move,
                                                          controlMaterial,
                                                          renderer)
    {

    }

    public override void OnStateActions()
    {
        animationController.Dead();

        var summon = move.gameObject;

        astronautInventory.Delete(summon);

        UnityEngine.Object.Destroy(summon);

        move.moveType.Value = PlayerController3d.MoveType.None;
        rotator.rotationType.Value = PlayerRotator.RotationType.None;
    }
}

[Serializable]
public class SummonStates
{
    public Dictionary<SummonState, SummonStateBase> states;

    public ReactiveProperty<SummonState> state;

    private IDisposable _update;

    public SummonStates(SummonState defaultState)
    {
        states = new Dictionary<SummonState, SummonStateBase>();

        state = new ReactiveProperty<SummonState>();

        state.Value = defaultState;
    }

    ~SummonStates()
    {
        _update.Dispose();
    }


    private void StateChanged(SummonState state)
    {
        var newState = states[state];

        newState.OnStateActions();
    }

    public void CreateStates(AnimationController animationController,
                             PlayerRotator rotator,
                             PlayerController3d move,
                             Material controlMaterial,
                             SkinnedMeshRenderer renderer,
                             int controlLayer)
    {
        var freeState = new FreeState(animationController,
                                      rotator,
                                      move,
                                      controlMaterial,
                                      renderer);

        var controlState = new ControlState(animationController,
                                            rotator,
                                            move,
                                            controlMaterial,
                                            renderer,
                                            controlLayer);

        var deadState = new DeadState(animationController,
                                      rotator,
                                      move,
                                      controlMaterial,
                                      renderer);

        states.Add(SummonState.Free, freeState);

        states.Add(SummonState.Controll, controlState);

        states.Add(SummonState.Dead, deadState);


        _update = state.Subscribe(x =>
        {
            StateChanged(x);
        });
    }
}

public class SummonCollider : PersonCollider
{
    #region Assign in editor
    public PlayerRotator playerRotator;

    public PlayerController3d playerController;
    #endregion

    [Inject]
    private ThatCanPick thatCanPick;

    private SummonStateController stateController;

    private void Start()
    {
        stateController = GetComponent<SummonStateController>();
    }

    public void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        var gameObj = other.gameObject;

        var layer = gameObj.layer;

        if (thatCanPick.enumLayerList.Contains(layer))
        {
            stateController.ChangeState(SummonState.Controll);
        }

    }
}
