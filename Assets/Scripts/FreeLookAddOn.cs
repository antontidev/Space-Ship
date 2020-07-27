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
        gestureController.Dragged += Touch_onFingerMove;
    }
    private void OnDisable()
    {
        gestureController.Dragged -= Touch_onFingerMove;
    }

    private void Touch_onFingerMove(SwipeInput input)
    {
        var delta = input.EndPosition - input.PreviousPosition;

        Debug.Log(delta);
        //_freeLookComponent.m_XAxis.Value += dir.x * Time.deltaTime * LookSpeed;
        //_freeLookComponent.m_YAxis.Value += dir.y * Time.deltaTime * LookSpeed;
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
}
