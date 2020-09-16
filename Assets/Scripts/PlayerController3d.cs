using InputSamples.Gestures;
using LeoLuz.PlugAndPlayJoystick;
using System;
using UniRx;
using UnityEngine;
using Zenject;

/// <summary>
/// Abstract class for player movement
/// </summary>
public abstract class PlayerMove
{
    private float moveSpeed;

    public PlayerMove(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    public abstract void Move(Vector2 movementDelta); 
    
    public Vector3 CalculateSpeed(Vector2 frameMovement)
    {
        var speedMovement = frameMovement * Time.fixedDeltaTime * moveSpeed;

        return new Vector3(speedMovement.x, 0.0f, speedMovement.y);
    }
}

/// <summary>
/// Player Movement via transform.Translate method
/// </summary>
public class PlayerMoveTranslate : PlayerMove
{
    private Transform transform;

    private Space space;

    public PlayerMoveTranslate(Transform transform, 
                               float moveSpeed, 
                               Space space = Space.World) : base(moveSpeed)
    {
        this.transform = transform;
        this.space = space;
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

    public PlayerMoveForce(Rigidbody rigidbody, 
                           float moveSpeed) : base(moveSpeed)
    {
        this.rigidbody = rigidbody;
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

    public GameObject groundCheck;

    public float moveSpeed = 500f;

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
                    moveTypeDelegate = new PlayerMoveForce(rb, moveSpeed);
                    break;
                case MoveType.Translate:
                    moveTypeDelegate = new PlayerMoveTranslate(transform, moveSpeed, Space.Self);
                    break;
            }
        });

        var stream = Observable.EveryUpdate().Where(_ => !playerInput.released.Value);

        stream.Subscribe(_ =>
        {
            var movement = playerInput.screenInput.Value;

            moveTypeDelegate.Move(movement);
        });
    }
}
