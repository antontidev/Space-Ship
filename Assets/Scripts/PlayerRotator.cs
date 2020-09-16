using LeoLuz.PlugAndPlayJoystick;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public abstract class PlayerRotate
{
    public abstract void Rotate(Vector3 movement);
}

public class PlayerRotator : MonoBehaviour
{
    /// <summary>
    /// I need two types of input in my DI container
    /// </summary>
    [Inject]
    private IJoystickInput playerInput;

    public enum RotationType
    {
        Tan,
        Quaternion,
        LookRotation
    }

    [Header("Rotation")]
    [SerializeField]
    private float rotationSpeed = 1f;

    [HideInInspector]
    [Header("Rotation")]
    [SerializeField]
    private RotationType rotationType;

    private void Start()
    {
        var stream = Observable.EveryUpdate().Where(_ => !playerInput.released.Value);

        stream.Subscribe(_ =>
        {
            var movement = playerInput.screenInput.Value;

            var rotation = new Vector3(movement.x, 0.0f, movement.y);

            Rotate(rotation);
        });
    }

    /// <summary>
    /// Rotates player in joystick forward direction
    /// </summary>
    /// <param name="frameMovement"></param>
    public void Rotate(Vector3 lookRotation)
    {
        if (lookRotation != Vector3.zero)
        {

            float angle = Mathf.Atan2(lookRotation.x, lookRotation.z) * Mathf.Rad2Deg;

            Rotate(angle);
        }
    }

    public void Rotate(float angle)
    {
        transform.Rotate(0, angle, 0, Space.Self);
    }

    #region Obsolete
    // Switch this by RotationType
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
    /// <param name="frameMovement"></param>
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
