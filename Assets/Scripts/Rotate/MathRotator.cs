using UnityEngine;
/// <summary>
/// Class which rotates player with atan
/// </summary>
public class MathRotator : PlayerMove
{
    public MathRotator(Transform transform,
                       float moveSpeed) : base(transform,
                                               moveSpeed)
    {
    }

    public override MoveName Name
    {
        get
        {
            return MoveName.Math;
        }
    }
    public override void Move(Vector2 movementDelta)
    {
        if (movementDelta != Vector2.zero)
        {
            var angle = Mathf.Atan2(movementDelta.x, movementDelta.y) * Mathf.Rad2Deg;

            if (lastValue > 0.001f && angle > 0.001f)
            {
                var rotateDelta = lastValue - angle;

                transform.Rotate(0, rotateDelta * moveSpeed, 0, Space.Self);
            }

            lastValue = angle;
        }
    }

#if UNITY_EDITOR_WIN || UNITY_EDITOR
    public override void _Move(Vector3 lookRotation)
    {
        transform.Rotate(lookRotation);
    }
#endif
}
