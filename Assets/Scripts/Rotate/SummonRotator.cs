using DG.Tweening;
using UnityEngine;
using Zenject;

public struct PlayerTransform
{
    public Transform playerTransform;
}

class SummonRotator : PlayerMove
{
    [Inject]
    private PlayerTransform playerTransform;

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
        transform.LookAt(playerTransform.playerTransform);
    }
}