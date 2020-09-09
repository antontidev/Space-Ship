using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public abstract class IFader : MonoBehaviour
{
    public abstract void FadeScene();
}

public class FadeManager : IFader
{
    private Animator fadeAnimator;

    private void Awake()
    {
        fadeAnimator = GetComponent<Animator>();
    }

    public override void FadeScene()
    {
        fadeAnimator.SetTrigger("Fade");
    }
}
