using LeoLuz.PlugAndPlayJoystick;
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
            switch (x)
            {
                case MoveType.Translate:
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

        playerInput.onPress.Subscribe(_ =>
        {
            var movement = playerInput.screenInput.Value;

            moveTypeDelegate.Move(movement);
        });
    }
}
