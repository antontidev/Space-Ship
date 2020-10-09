using UnityEngine;
/// <summary>
/// Non-error prone NoneRotator
/// </summary>
public class NoneRotator : PlayerMove
{
    public NoneRotator(Transform transform,
                       float rotateSpeed) : base(transform,
                                                 rotateSpeed)
    {
    }

    public override MoveName Name
    {
        get
        {
            return MoveName.None;
        }
    }
    public override void Move(Vector2 movementDelta)
    {
    }
}
