using System;
using UnityEngine;
using Zenject;

public enum SummonState
{
    Free,
    Controll,
    Dead
}

public class State<T> where T : Enum
{
    private int _currentStateIndex;

    public State(T initialState)
    {
        //initialState.Get
    } 
}

public class SummonStateController
{
    private State<SummonState> state;

    public SummonStateController(SummonState initialState)
    {
        state = new State<SummonState>(initialState);
    }

    public virtual void SetState(SummonState state)
    {
    }
}

public class SummonCollider : MonoBehaviour
{
    #region Assign in editor
    public PlayerRotator playerRotator;

    public PlayerController3d playerController;
    #endregion

    /// <summary>
    /// Layer on which astronauts should be after picking
    /// </summary>
    [Layer]
    public int summonNewLayer;

    /// <summary>
    /// You can set it through editor and choose initial state
    /// Only for test purposes, in real game choose Free state
    /// </summary>
    public SummonState initialSummonState;

    [Inject]
    private RaycastManager raycastManager;

    private SummonStateController stateController;

    private void Start()
    {
        stateController = new SummonStateController(initialSummonState);
    }

    private void OnTriggerEnter(Collider other)
    {
        var gameObj = other.gameObject;

        if (gameObj.tag == "Player")
        {
            playerRotator
            .rotationType
            .Value = PlayerRotator.RotationType.Summon;

            playerController
            .moveType
            .Value = PlayerController3d.MoveType.Summon;

            gameObject.layer = summonNewLayer;
        }

        raycastManager.CollisionWithObject(gameObj);
    }

    public void OnMeteoriteCollision(Collider meteorite) 
    { 
    }
}
