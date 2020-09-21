﻿using LeoLuz.PlugAndPlayJoystick;
using UniRx;
using UnityEngine;
using Zenject;

public class AnimationController : MonoBehaviour
{
    [Inject]
    private IJoystickInput joystickInput;

    public Animator animator;

    // Maybe i may simplify this blend tree logic by using 1d blend parameters
    // Start is called before the first frame update
    void Start()
    {
        joystickInput.screenInput.Subscribe(movement =>
        {
            var magnitude = movement.magnitude;
            animator.SetFloat("Magnitude", magnitude);
        });
    }
}
