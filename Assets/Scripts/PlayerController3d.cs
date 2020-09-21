using LeoLuz.PlugAndPlayJoystick;
using System;
using UniRx;
using UnityEngine;
using Zenject;

/// <summary>
/// Set abstract class for player debug information.
/// You don't need to override this method if you don't
/// need to draw debug information.
/// Works only in debug mode because in basic PlayerMove class placed in #if #endif
/// UNITY_DEBUG constant.
/// </summary>
public abstract class PlayerMoveDebug
{
    public virtual void DrawGizmos(Transform transform, Vector3 lastForward) { }
}

/// <summary>
/// Abstract class for player movement
/// </summary>
public abstract class PlayerMove
{
    public abstract string MoveName
    {
        get;
    }

    protected float moveSpeed;

    protected float lastValue;

    protected Vector3 lastForward;

    protected Transform transform;

    public PlayerMove(Transform transform, float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
        this.transform = transform;

        lastForward = Vector3.zero;
        lastValue = 0.0f;
    }

    public abstract void Move(Vector2 movementDelta);

#if UNITY_EDITOR_WIN || UNITY_EDITOR
    protected PlayerMoveDebug playerMoveDebug;

    public virtual void _Move(Vector3 lookRotation)
    {
    }

    public void DrawMoveDebug()
    {
        playerMoveDebug?.DrawGizmos(transform, lastForward);
    }
#endif

    public virtual Vector3 CalculateSpeed(Vector2 frameMovement)
    {
        var speedMovement = lastForward = frameMovement * Time.fixedDeltaTime * moveSpeed;

        return new Vector3(speedMovement.x, 0.0f, speedMovement.y);
    }
}

/// <summary>
/// Player Movement via transform.Translate method
/// </summary>
public class PlayerMoveTranslate : PlayerMove
{
    private Space space;

    public PlayerMoveTranslate(Transform transform,
                               float moveSpeed,
                               Space space = Space.World) : base(transform,
                                                                 moveSpeed)
    {
        this.space = space;
    }

    public override string MoveName
    {
        get
        {
            return "Translate";
        }
    }
    /// <summary>
    /// Ignores physics and  shape of ground
    /// </summary>
    /// <param name="movementDelta"></param>
    public override void Move(Vector2 movementDelta)
    {
        var movement = CalculateSpeed(movementDelta);

        transform.Translate(movement, space);
    }
}

/// <summary>
/// Moves player via physics engine
/// </summary>
public class PlayerMoveForce : PlayerMove
{
    private Rigidbody rigidbody;

    public PlayerMoveForce(Transform transform,
                           Rigidbody rigidbody,
                           float moveSpeed) : base(transform,
                                                   moveSpeed)
    {
        this.rigidbody = rigidbody;
    }

    public override string MoveName
    {
        get
        {
            return "Rigidbody";
        }
    }
    /// <summary>
    /// Moves player by rigidbody RelativeForce which helps a lot in such case
    /// when player moves around sphere. Sometimes player stucks in shape edges
    /// </summary>
    /// <param name="frameMovement"></param>
    [Obsolete("Use MovePlayerTranslate instead")]
    public override void Move(Vector2 movementDelta)
    {
        var movement = CalculateSpeed(movementDelta);

        rigidbody.AddRelativeForce(movement);
    }
}

public class PlayerController3d : MonoBehaviour
{
    /// <summary>
    /// I need two types of input in my DI container
    /// </summary>
    [Inject]
    private IJoystickInput playerInput;

    #region Move type
    public enum MoveType
    {
        Rigidbody,
        Translate
    }

    [Header("Movement")]
    [SerializeField]
    private ReactiveProperty<MoveType> moveType;

    private PlayerMove moveTypeDelegate;
    #endregion

    public float moveSpeed = 500f;

    public AnimationCurve speedByMagnitude;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        moveType.Subscribe(x =>
        {
            switch (moveType.Value)
            {
                case MoveType.Rigidbody:
                    moveTypeDelegate = new PlayerMoveForce(transform,
                                                           rb,
                                                           moveSpeed);
                    break;
                case MoveType.Translate:
                    moveTypeDelegate = new PlayerMoveTranslate(transform,
                                                               moveSpeed,
                                                               Space.Self);
                    break;
            }
        });

        // I need to place code below to the Zenject input container
        // because i use this events in several places, so i don't need to copy/paste the code
        var stream = Observable.EveryUpdate().Where(_ => !playerInput.released.Value);

        stream.Subscribe(_ =>
        {
            var movement = playerInput.screenInput.Value;

            moveTypeDelegate.Move(movement);
        });
    }
}
