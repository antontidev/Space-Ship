using LeoLuz.PlugAndPlayJoystick;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

/// <summary>
/// Non-error prone NoneRotator
/// </summary>
public class NoneRotator : PlayerMove
{
    public NoneRotator(Transform transform, 
                       float rotateSpeed) : base(transform, 
                                                 rotateSpeed)
    {
    }

    public override string MoveName
    {
        get
        {
            return "None";
        }
    }
    public override void Move(Vector2 movementDelta)
    {
    }
}

/// <summary>
/// Rotates player with transform.Rotate(...)
/// </summary>
public class TransformRotateRotator : PlayerMove
{
    public TransformRotateRotator(Transform transform,
                                  float rotateSpeed,
                                  Space space = Space.Self) : base(transform,
                                                                   rotateSpeed)
    {
    }

    public override string MoveName
    {
        get
        {
            return "TransformRotate";
        }
    }
    public override void Move(Vector2 movementDelta)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Class which rotates player with atan
/// </summary>
public class MathRotator : PlayerMove
{
    public MathRotator(Transform transform, 
                       float moveSpeed) : base(transform, 
                                               moveSpeed) 
    {
    }

    public override string MoveName
    {
        get
        {
            return "Math";
        }
    }
    public override void Move(Vector2 movementDelta)
    {
        if (movementDelta != Vector2.zero)
        {
            var angle = Mathf.Atan2(movementDelta.x, movementDelta.y) * Mathf.Rad2Deg;

            if (lastValue > 0.001f && angle > 0.001f)
            {
                var rotateDelta = angle - lastValue;

                transform.Rotate(0, rotateDelta * moveSpeed, 0, Space.Self);
            }
            lastValue = angle;
        }
    }

#if UNITY_EDITOR_WIN || UNITY_EDITOR
    public override void _Move(Vector3 lookRotation)
    {
        transform.Rotate(lookRotation);
    }
#endif
}

/// <summary>
/// Class which rotates player with LookRotation and Slerp
/// </summary>
public class QuaternionRotator : PlayerMove
{
    public QuaternionRotator(Transform transform, 
                             float rotateSpeed) : base(transform, 
                                                       rotateSpeed) 
    {
#if UNITY_EDITOR
        playerMoveDebug = new QuaternionRotateDebug();
#endif
    }

    public override string MoveName 
    {
        get
        {
            return "Quaternion";
        }
    }

    // TODO it rotates player in world space not in self space
    /// <summary>
    /// Rotates player with LookRotation
    /// </summary>
    /// <remarks>
    /// Works perfectly but rotates player in world space not in local
    /// </remarks>
    /// <param name="lookRotation">Any delta from any input source</param>
    public override void Move(Vector2 lookRotation)
    {
        var worldSpaceRotateTo = CalculateSpeed(lookRotation);

        var vectorizedRotation = transform.InverseTransformDirection(worldSpaceRotateTo);

        if (vectorizedRotation != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(vectorizedRotation, transform.root.up);

            //var lerpedRotation = Quaternion.Slerp(transform.rotation,
            //                                      rotation,
            //                                      Time.deltaTime * moveSpeed);

            //transform.rotation = lerpedRotation;
        }
    }

#if UNITY_EDITOR_WIN || UNITY_EDITOR
    public override void _Move(Vector3 lookRotation)
    {
        transform.Rotate(lookRotation);
    }

    /// <summary>
    /// Used to debug input
    /// </summary>
    private class QuaternionRotateDebug : PlayerMoveDebug
    {
        public override void DrawGizmos(Transform transform, Vector3 lastForward)
        {
            var forward = GetRay(transform.position, lastForward);
            Gizmos.DrawRay(forward);

            var upwards = GetRay(transform.position, transform.root.up);

            Gizmos.DrawRay(upwards);
        }

        private Ray GetRay(Vector3 origin, Vector3 direction)
        {
            return new Ray(origin, direction);
        }
    }
#endif
}

public class PlayerRotator : MonoBehaviour
{
    /// <summary>
    /// I need two types of input in my DI container
    /// </summary>
    /// <remarks>
    /// Maybe i need to use my old InputManager from Unity code samples
    /// It will be simpier for player to get around with controls
    /// For the future plans i could add to the game multiple source of inputs
    /// </remarks>
    [Inject]
    private IJoystickInput playerInput;

    #region Rotation type
    public enum RotationType
    {
        None,
        Math,
        Quaternion
    }

    [SerializeField]
    private float rotationSpeed = 1f;
    
    public ReactiveProperty<RotationType> rotationType;

    private PlayerMove rotatorDelegate;
    #endregion

    private void Start()
    {
        rotationType.Subscribe(x =>
        {
            ChangeRotator(x);
        });

        var stream = Observable.EveryUpdate().Where(_ => !playerInput.released.Value);

        stream.Subscribe(_ =>
        {
            var movement = playerInput.screenInput.Value;

            rotatorDelegate.Move(movement);
        });
    }

    /// <summary>
    /// Draws move debug information
    /// </summary>
    private void OnDrawGizmos()
    {
        rotatorDelegate?.DrawMoveDebug();
    }

    /// <summary>
    /// Callback for changing rotator
    /// </summary>
    /// <param name="x">Rotation type</param>
    public void ChangeRotator(RotationType x)
    {
        switch (rotationType.Value)
        {
            case RotationType.Quaternion:
                rotatorDelegate = new QuaternionRotator(transform,
                                                        rotationSpeed);
                break;
            case RotationType.Math:
                rotatorDelegate = new MathRotator(transform,
                                                  rotationSpeed);
                break;
            default:
                Debug.LogWarning("Rotator set to None");
                rotatorDelegate = new NoneRotator(transform, rotationSpeed);
                break;
        }
    }

#if UNITY_EDITOR_WIN || UNITY_EDITOR
    public void _RotateEditor(Vector3 lookRotation)
    {
        rotatorDelegate.Move(lookRotation);
    }
#endif

    #region Obsolete
    /// <summary>
    /// Rotates player in joystick forward direction
    /// </summary>
    /// <param name="frameMovement"></param>
    [Obsolete("Use rotation delegate instead")]
    public void Rotate(Vector3 lookRotation)
    {
        var rotation = Quaternion.LookRotation(lookRotation);

        var lerpedRotation = Quaternion.Slerp(transform.rotation,
                                              rotation,
                                              Time.deltaTime * rotationSpeed);

        transform.rotation = lerpedRotation;
    }

    /// <summary>
    /// Just rotates player by certain angle around Y-axis
    /// </summary>
    /// <param name="angle"></param>
    [Obsolete("Use rotation delegate instead")]
    public void Rotate(float angle)
    {
        transform.Rotate(0, angle, 0, Space.Self);
    }

    /// <summary>
    /// Rotates player based on tangens between horizontal and vertical parts
    /// of input Vector2
    /// </summary>
    /// <param name="frameMovement"></param>
    [Obsolete("Use RotatePlayer instead")]
    private void RotatePlayerTan(Vector2 frameMovement)
    {
        var xRotation = transform.localEulerAngles.x;
        var zRotation = transform.localEulerAngles.z;
        var yRotation = Mathf.Atan2(frameMovement.y, frameMovement.x) * 180 / Mathf.PI;

        var rotation = new Vector3(xRotation, yRotation, zRotation);


        transform.localEulerAngles = rotation;
    }

    /// <summary>
    /// Rotates player based on Quaternion Euler angles
    /// </summary>
    /// <remarks>
    /// Very interesting way to rotate character
    /// I will use it instead current way of rotatings
    /// </remarks>
    /// <param name="frameMovement">
    /// Player movement delta that you will get from any source of input
    /// </param>
    [Obsolete("Use RotatePlayer instead")]
    private void RotatePlayerQuaternion(Vector3 frameMovement)
    {
        var xRotation = transform.localEulerAngles.x;
        var zRotation = transform.localEulerAngles.z;
        var yRotation = Mathf.Atan2(frameMovement.y, frameMovement.x) * 180 / Mathf.PI;

        var lookVec = new Vector3(xRotation, yRotation, zRotation);

        Quaternion targetRotation = Quaternion.LookRotation(lookVec, Vector3.back);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime);
    }
    #endregion
}
