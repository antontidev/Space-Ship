using System;
using UnityEngine;
/// <summary>
/// Moves player via physics engine
/// </summary>
public class PlayerMoveForce : PlayerMove
{
    private Rigidbody rigidbody;

    public PlayerMoveForce(Transform transform,
                           Rigidbody rigidbody,
                           float moveSpeed) : base(transform,
                                                   moveSpeed)
    {
        this.rigidbody = rigidbody;
    }

    public override MoveName Name
    {
        get
        {
            return MoveName.Rigidbody;
        }
    }
    /// <summary>
    /// Moves player by rigidbody RelativeForce which helps a lot in such case
    /// when player moves around sphere. Sometimes player stucks in shape edges
    /// </summary>
    /// <param name="frameMovement"></param>
    [Obsolete("Use MovePlayerTranslate instead")]
    public override void Move(Vector2 movementDelta)
    {
        var movement = CalculateSpeed(movementDelta);

        rigidbody.AddRelativeForce(movement);
    }
}
