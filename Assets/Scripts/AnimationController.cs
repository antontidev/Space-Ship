using LeoLuz.PlugAndPlayJoystick;
using System;
using UniRx;
using UnityEngine;
using Zenject;

public class AnimationController : MonoBehaviour
{
    public Animator animator;

    [Inject]
    private IJoystickInput joystickInput;

    private IDisposable _update;

    /// <summary>
    /// Subscribes animation to the actual player movement
    /// </summary>
    public void SubscribeAnimation()
    {
        _update = joystickInput.screenInput.Subscribe(movement =>
        {
            var magnitude = movement.magnitude;
            animator.SetFloat("Magnitude", magnitude);
        });
    }

    /// <summary>
    /// Unsubscribes animation from actual player movement
    /// </summary>
    /// <remarks>
    /// Don't think it's a good idea to just Dispose animation
    /// </remarks>
    public void UnsubscribeAnimation()
    {
        _update?.Dispose();
    }

    public void Dead()
    {
        animator.SetTrigger("Dead");
    }

    private void OnDestroy()
    {
        _update?.Dispose();
    }
}
