using UnityEngine;
/// <summary>
/// Player Movement via transform.Translate method
/// </summary>
public class PlayerMoveTranslate : PlayerMove
{
    private Space space;

    public PlayerMoveTranslate(Transform transform,
                               float moveSpeed,
                               Space space = Space.World) : base(transform,
                                                                 moveSpeed)
    {
        this.space = space;
    }

    public override MoveName Name
    {
        get
        {
            return MoveName.Translate;
        }
    }
    /// <summary>
    /// Ignores physics and  shape of ground
    /// </summary>
    /// <param name="movementDelta"></param>
    public override void Move(Vector2 movementDelta)
    {
        var movement = CalculateSpeed(movementDelta);

        transform.Translate(movement, space);
    }
}
