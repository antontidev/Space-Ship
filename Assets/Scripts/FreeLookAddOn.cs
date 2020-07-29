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

    private float _lastX;
    private float _lastY;

    void Start()
    {
        _freeLookComponent = GetComponent<CinemachineFreeLook>();
        CinemachineCore.GetInputAxis = HandleAxisInput;
    }

    private float HandleAxisInput(string axisName)
    {
        switch (axisName)
        {
            case "Mouse X":
                var mov = _lastX / LookSpeed;
                _lastX = 0.0f;
                return mov;
            case "Mouse Y":
                var mov1 = _lastY / LookSpeed;
                _lastY = 0.0f;
                return mov1;
            default:
                Debug.LogError("Input <" + axisName + "> not recognyzed.", this);
                break;
        }

        return 0f;
    }

    private void OnEnable()
    {
        gestureController.Dragged += GestureController_Dragged;
    }
    private void OnDisable()
    {
        gestureController.Dragged -= GestureController_Dragged;
    }

    public void GestureController_Dragged(SwipeInput input)
    {
        var delta = input.EndPosition - input.PreviousPosition;

        _lastX = delta.x;
        _lastY = delta.y;
    }
}
