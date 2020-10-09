using System;
using UnityEngine;

class PlayerMoveNone : PlayerMove
{
    public PlayerMoveNone(Transform transform,
                          float moveSpeed) : base(transform,
                                                  moveSpeed)
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