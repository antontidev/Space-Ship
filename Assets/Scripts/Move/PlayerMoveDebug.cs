using UnityEngine;

/// <summary>
/// Set abstract class for player debug information.
/// You don't need to override this method if you don't
/// need to draw debug information.
/// Works only in debug mode because in basic PlayerMove class placed in #if #endif
/// UNITY_DEBUG constant.
/// </summary>
public abstract class PlayerMoveDebug
{
    public virtual void DrawGizmos(Transform transform, Vector3 lastForward) { }
}
