using LeoLuz.PlugAndPlayJoystick;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;

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
            animator.SetFloat("MovementX", movement.x);
            animator.SetFloat("MovemntY", movement.y);
        });
    }
}
