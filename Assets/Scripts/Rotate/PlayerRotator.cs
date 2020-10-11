using LeoLuz.PlugAndPlayJoystick;
using System;
using UniRx;
using UnityEngine;
using Zenject;

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

    private IDisposable _update;

    #region Rotation type
    public enum RotationType
    {
        None,
        Math,
        Quaternion,
        Summon
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

        
        _update = playerInput.OnPress.Subscribe(_ =>
        {
            var movement = playerInput.screenInput.Value;

            rotatorDelegate.Move(movement);
        });
    }

#if UNITY_EDITOR_WIN || UNITY_EDITOR
    /// <summary>
    /// Draws move debug information
    /// </summary>
    private void OnDrawGizmos()
    {
        rotatorDelegate?.DrawMoveDebug();
    }
#endif

    /// <summary>
    /// Callback for changing rotator
    /// </summary>
    /// <param name="x">Rotation type</param>
    private void ChangeRotator(RotationType x)
    {
        switch (x)
        {
            case RotationType.Quaternion:
                rotatorDelegate = new QuaternionRotator(transform,
                                                        rotationSpeed);
                break;
            case RotationType.Math:
                rotatorDelegate = new MathRotator(transform,
                                                  rotationSpeed);
                break;
            case RotationType.Summon:
                rotatorDelegate = new SummonRotator(transform,
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

    private void OnDestroy()
    {
        _update.Dispose();
    }

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
