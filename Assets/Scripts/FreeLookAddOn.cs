using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem.EnhancedTouch;
using InputSamples.Gestures;
using System;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CinemachineFreeLook))]
public class FreeLookAddOn : MonoBehaviour
{
    [Range(0f, 60f)] public float LookSpeed = 1f;
    public bool InvertY = false;
    private CinemachineFreeLook _freeLookComponent;

    [SerializeField]
    public GestureController gestureController;

    void Start()
    {
        _freeLookComponent = GetComponent<CinemachineFreeLook>();
    }

    private void OnEnable()
    {
        //gestureController.Swiped += OnLookSwipe;
        gestureController.Pressed += OnPressed;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove += Touch_onFingerMove;
    }
    private void OnDisable()
    {
        //gestureController.Swiped -= OnLookSwipe;
        gestureController.Pressed -= OnPressed;

        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove -= Touch_onFingerMove;
    }

    private void Touch_onFingerMove(Finger finger)
    {
        var dir = finger.currentTouch.delta;

        _freeLookComponent.m_XAxis.Value += dir.x * Time.deltaTime * LookSpeed;
        _freeLookComponent.m_YAxis.Value += dir.y * Time.deltaTime * LookSpeed;
    }


    private void OnPressed(SwipeInput pressed)
    {
        Debug.Log(pressed.StartPosition.ToString());
    }

    public void OnLookSwipe(SwipeInput swipeInput)
    {
        float duration = Convert.ToSingle(swipeInput.SwipeDuration);

        Vector2 dir = swipeInput.SwipeDirection;

        var dir2 = new Vector3(swipeInput.SwipeDirection.x, swipeInput.SwipeDirection.y, 0);
        var camRotation = Quaternion.Euler(dir2);

        dir.y = InvertY ? -dir.y : dir.y;

        dir.x *= 180f;

        _freeLookComponent.m_XAxis.Value += dir.x * duration * Time.deltaTime * LookSpeed;
        _freeLookComponent.m_YAxis.Value += dir.y * duration * Time.deltaTime * LookSpeed;

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        //Normalize the vector to have an uniform vector in whichever form it came from (I.E Gamepad, mouse, etc)
        Vector2 lookMovement = context.ReadValue<Vector2>().normalized;
        lookMovement.y = InvertY ? -lookMovement.y : lookMovement.y;

        // This is because X axis is only contains between -180 and 180 instead of 0 and 1 like the Y axis
        lookMovement.x *= 180f;

        //Ajust axis values using look speed and Time.deltaTime so the look doesn't go faster if there is more FPS
        _freeLookComponent.m_XAxis.Value += lookMovement.x * LookSpeed * Time.deltaTime;
        _freeLookComponent.m_YAxis.Value += lookMovement.y * LookSpeed * Time.deltaTime;
    }
}
