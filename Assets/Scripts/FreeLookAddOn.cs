using Cinemachine;
using InputSamples.Gestures;
using UnityEngine;

public class FreeLookAddOn : MonoBehaviour
{
    [SerializeField]
    private GestureController gestureController;

    [Range(0.1f, 1f)]
    public float LookSpeed = 1f;

    [Range(1f, 2f)]
    public float TouchIdle = 2f;

    public bool InvertY = false;

    [SerializeField]
    private CinemachineFreeLook _freeLookComponent;

    private float _lastX;
    private float _lastY;

    void Awake()
    {
        CinemachineCore.GetInputAxis = HandleAxisInput;

        LookSpeed = GetSensivity();
    }

    private float GetSensivity()
    {
        var sensivity = PlayerPrefs.GetFloat("sensivity");

        return sensivity;
    }

    private void OnEnable()
    {
        gestureController.Dragged += GestureController_Dragged;
        gestureController.PotentiallySwiped += GestureController_PotentiallySwiped;
    }

    private void OnDisable()
    {
        gestureController.Dragged -= GestureController_Dragged;
        gestureController.PotentiallySwiped -= GestureController_PotentiallySwiped;
    }
    private void GestureController_PotentiallySwiped(SwipeInput input)
    {
    }

    // Handle input if not just tupped
    private float HandleAxisInput(string axisName)
    {
        switch (axisName)
        {
            case "Mouse X":
                var mov = _lastX / LookSpeed * Time.deltaTime;
                _lastX = 0.0f;
                return mov;
            case "Mouse Y":
                var mov1 = _lastY / LookSpeed * Time.deltaTime;
                _lastY = 0.0f;
                return mov1;
            default:
                Debug.LogError("Input <" + axisName + "> not recognyzed.", this);
                break;
        }

        return 0f;
    }

    public void SetTargetObject(Transform planetTransform)
    {
        _freeLookComponent.LookAt = _freeLookComponent.Follow = planetTransform;
        _freeLookComponent.m_BindingMode = CinemachineTransposer.BindingMode.WorldSpace;
    }

    public void GestureController_Dragged(SwipeInput input)
    {
        var delta = input.EndPosition - input.PreviousPosition;

        if (input.SwipeDuration > gestureController.maxTapDuration / TouchIdle)
        {
            _lastX = delta.x;
            _lastY = delta.y;
        }
    }
}
