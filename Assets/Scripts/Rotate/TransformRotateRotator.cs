using System;
using UnityEngine;
/// <summary>
/// Rotates player with transform.Rotate(...)
/// </summary>
public class TransformRotateRotator : PlayerMove
{
    public TransformRotateRotator(Transform transform,
                                  float rotateSpeed,
                                  Space space = Space.Self) : base(transform,
                                                                   rotateSpeed)
    {
    }

    public override MoveName Name
    {
        get
        {
            return MoveName.TransformRotate;
        }
    }
    public override void Move(Vector2 movementDelta)
    {
        throw new NotImplementedException();
    }
}
