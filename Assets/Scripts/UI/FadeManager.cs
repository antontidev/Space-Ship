using UnityEngine;

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
