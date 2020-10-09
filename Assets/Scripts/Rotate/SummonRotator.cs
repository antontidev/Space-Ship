using UnityEngine;

class SummonRotator : PlayerMove
{
    public SummonRotator(Transform transform,
                         float rotationSpeed) : base(transform,
                                                     rotationSpeed)
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
    }
}