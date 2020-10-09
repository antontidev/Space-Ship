using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class PlayerSummonMove : PlayerMove
{
    public PlayerSummonMove(Transform transform, 
                            float moveSpeed) : base(transform,
                                                    moveSpeed)
    {
    }

    public override MoveName Name 
    {
        get
        {
            return MoveName.Summon;
        }
    }
    public override void Move(Vector2 movementDelta)
    {
        var movement = CalculateSpeed(movementDelta);

        transform.Translate(movement, Space.Self);
    }
}