using UnityEngine;

/// <summary>
/// Abstract class for player movement
/// </summary>
public abstract class PlayerMove
{
    public abstract MoveName Name
    {
        get;
    }

    protected float moveSpeed;

    protected float lastValue;

    protected Vector3 lastForward;

    protected Transform transform;

    public PlayerMove(Transform transform, float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
        this.transform = transform;

        lastForward = Vector3.zero;
        lastValue = 0.0f;
    }

    public abstract void Move(Vector2 movementDelta);

#if UNITY_EDITOR_WIN || UNITY_EDITOR
    protected PlayerMoveDebug playerMoveDebug;

    public virtual void _Move(Vector3 lookRotation)
    {
    }

    public void DrawMoveDebug()
    {
        playerMoveDebug?.DrawGizmos(transform, lastForward);
    }
#endif

    public virtual Vector3 CalculateSpeed(Vector2 frameMovement)
    {
        var speedMovement = lastForward = frameMovement * Time.fixedDeltaTime * moveSpeed;

        return new Vector3(speedMovement.x, 0.0f, speedMovement.y);
    }
}
