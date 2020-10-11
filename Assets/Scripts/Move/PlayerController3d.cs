using LeoLuz.PlugAndPlayJoystick;
using System;
using UniRx;
using UnityEngine;
using Zenject;

public class PlayerController3d : MonoBehaviour
{
    /// <summary>
    /// I need two types of input in my DI container
    /// </summary>
    [Inject]
    private IJoystickInput playerInput;

    /// <summary>
    /// Used to pass player Transform to summons
    /// for LookAt operations
    /// </summary>
    [Inject]
    private PlayerTransform playerTransform;

    [SerializeField]
    private AnimationController animationController;

    #region Move type
    public enum MoveType
    {
        Translate,
        Summon,
        None
    }

    [Header("Movement")]
    public ReactiveProperty<MoveType> moveType;

    private PlayerMove moveTypeDelegate;
    #endregion

    public float moveSpeed;

    private Rigidbody rb;

    private IDisposable _update;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        playerTransform.playerTransform = transform;
    }

    private void Start()
    {
        moveType.Subscribe(x =>
        {
            switch (x)
            {
                case MoveType.Translate:
                    animationController.SubscribeAnimation();
                    moveTypeDelegate = new PlayerMoveTranslate(transform,
                                                               moveSpeed,
                                                               Space.Self);
                    break;
                case MoveType.Summon:
                    moveTypeDelegate = new PlayerSummonMove(transform, 
                                                            moveSpeed);
                    break;
                case MoveType.None:
                    moveTypeDelegate = new PlayerMoveNone(transform,
                                                          moveSpeed);
                    break;
            }
        });

        _update = playerInput.OnPress.Subscribe(_ =>
        {
            var movement = playerInput.screenInput.Value;

            moveTypeDelegate.Move(movement);
        });
    }

    private void OnDestroy()
    {
        _update.Dispose();
    }
}
